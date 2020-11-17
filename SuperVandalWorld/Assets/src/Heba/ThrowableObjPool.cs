using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjPool : MonoBehaviour
{
    public static ThrowableObjPool Instance { get; private set; }

    public GameObject throwableObject; // throwable object that we're going to create
    List<GameObject> poolingList = new List<GameObject>(); // list of all the throwable objects that can be reused
    //List<bool> poolingListAvail = new List<bool>();
    public int numObjsInPool = 20; // there are 20 throwable objects that can be reused when we start

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Make sure we have an object to create
        if (throwableObject != null)
        {

            // create the default number of objects for our pool
            for (int i = 0; i < numObjsInPool; i++)
            {
                // creates the throwable object
                GameObject obj = Instantiate(throwableObject);
                // set the object to inactive
                obj.SetActive(false);
                // save the object pool index in the object so that when it disappears it can signal
                // the object pool that it can be reused again
                obj.GetComponentInChildren<ThrowableObject>().PoolIndex = i;
                //obj.GetComponentInChildren<ThrowableObject>().Enemy = this;

                // add object to our list
                poolingList.Add(obj);
                //poolingListAvail.Add(true);
            }
        }
    }

    // Called when an enemy needs a throwable object
    public GameObject GetThrowableObjectFromPool()
    {
        if (throwableObject == null)
            return null;

        // go through our object pool list to try to find one that's available
        for (int i = 0; i < poolingList.Count; i++)
        {
            //if (poolingListAvail[i])
            // if the object is inactive it means that it's free to be used
            if(!poolingList[i].activeInHierarchy)
            {
                // Make object visible
                poolingList[i].SetActive(true);

                // return the object to the enemy
                //poolingListAvail[i] = false;
                print("Requesting object " + i);
                return poolingList[i];
            }
        }

        print("Couldn't find available object");

        // let's create a new throwable object for our object pool
        GameObject obj = Instantiate(throwableObject); //0-19
        obj.SetActive(true);
        //poolingListAvail.Add(false);
        obj.GetComponentInChildren<ThrowableObject>().PoolIndex = poolingList.Count;
        //obj.GetComponentInChildren<ThrowableObject>().Enemy = this;
        poolingList.Add(obj); //21 ..etc



        return obj;
    }

    // Called from throwable object to indicate that the object is free to be used again
    public void ReleasePoolingObj(int index)
    {
        poolingList[index].SetActive(false);
        //poolingListAvail[index] = true;
    }


}
