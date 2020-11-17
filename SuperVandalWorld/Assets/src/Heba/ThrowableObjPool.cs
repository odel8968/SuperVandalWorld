using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjPool : MonoBehaviour
{
    public static ThrowableObjPool Instance { get; private set; }

    public GameObject throwableObject;
    List<GameObject> poolingList = new List<GameObject>();
    //List<bool> poolingListAvail = new List<bool>();
    public int numObjsInPool = 20;

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
        if (throwableObject != null)
        {
            for (int i = 0; i < numObjsInPool; i++)
            {
                GameObject obj = Instantiate(throwableObject);
                obj.SetActive(false);
                obj.GetComponentInChildren<ThrowableObject>().PoolIndex = i;
                //obj.GetComponentInChildren<ThrowableObject>().Enemy = this;
                poolingList.Add(obj);
                //poolingListAvail.Add(true);
            }
        }
    }

    public GameObject GetThrowableObjectFromPool()
    {
        if (throwableObject == null)
            return null;

        for (int i = 0; i < poolingList.Count; i++)
        {
            //if (poolingListAvail[i])
            if(!poolingList[i].activeInHierarchy)
            {
                //poolingListAvail[i] = false;
                print("Requesting object " + i);
                return poolingList[i];
            }
        }

        print("Couldn't find available object");

        GameObject obj = Instantiate(throwableObject);
        poolingList.Add(obj);
        //poolingListAvail.Add(false);
        obj.GetComponentInChildren<ThrowableObject>().PoolIndex = poolingList.Count - 1;
        //obj.GetComponentInChildren<ThrowableObject>().Enemy = this;
        obj.SetActive(false);



        return obj;
    }

    public void ReleasePoolingObj(int index)
    {
        poolingList[index].SetActive(false);
        //poolingListAvail[index] = true;
    }


}
