using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed;    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveVelocity;
    public float curTime;
    public float moveTime = 3;
    public float direction = 1;

    Character_Movement playerMovement;
    bool playerAlive;
    public float restartDelay = 2f;

    public GameObject throwableObject;
    public float attackDistance = 5;
    public float attackForce = 100;
    public float timeBetweenAttacks = 4;
    private float curAttackTime;

    public bool PlayerAlive { get { return playerAlive;} set { playerAlive = value;}}

    List<GameObject> poolingList = new List<GameObject>();
    List<bool> poolingListAvail = new List<bool>();
    public int numObjsInPool = 5; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
       animator = GetComponent<Animator>();
        playerAlive = true;
        playerMovement = FindObjectOfType<Character_Movement>();

        if(throwableObject != null)
        {
            for(int i = 0; i < numObjsInPool; i++)
            {
                GameObject obj = Instantiate(throwableObject);
                obj.SetActive(false);
                obj.GetComponentInChildren<ThrowableObject>().PoolIndex = i;
                obj.GetComponentInChildren<ThrowableObject>().Enemy = this;
                poolingList.Add(obj);
                poolingListAvail.Add(true);
            }
        }
    }

    private GameObject GetThrowableObjectFromPool()
    {
        if(throwableObject == null)
            return null;

        for(int i = 0; i < poolingList.Count; i++)
        {
            if(poolingListAvail[i])
            {
                poolingListAvail[i] = false;
                print("Requesting object " + i);
                return poolingList[i];
            }
        }

        print("Couldn't find available object");

        GameObject obj = Instantiate(throwableObject);
        poolingList.Add(obj);
        poolingListAvail.Add(false);
        obj.GetComponentInChildren<ThrowableObject>().PoolIndex = poolingList.Count - 1;
        obj.GetComponentInChildren<ThrowableObject>().Enemy = this;
        obj.SetActive(false);



        return obj;
    }

    public void ReleasePoolingObj(int index)
    {
        poolingListAvail[index] = true;
    }

    // Update is called once per frame
    public void Update()
    {
        float dist = Mathf.Abs(playerMovement.transform.position.x - transform.position.x);
        if(dist <= attackDistance)
        {
            curAttackTime += Time.deltaTime;

            if(curAttackTime >= timeBetweenAttacks)
            {
                //GameObject obj = GameObject.Instantiate(throwableObject, transform.position, Quaternion.identity);
                GameObject obj = GetThrowableObjectFromPool();
                if(obj != null)
                {
                    obj.SetActive(true);

                    obj.transform.position = transform.position;
                    obj.transform.rotation = Quaternion.identity;

                    var forceVector = (playerMovement.transform.position - transform.position).normalized * attackForce;
                    obj.GetComponent<Rigidbody2D>().AddForce(forceVector);
                }
                curAttackTime = 0;
            }

            moveVelocity.x = 0;
        }
        else
        {
            curAttackTime = timeBetweenAttacks;
            curTime += Time.deltaTime;
            if(curTime >= moveTime)
            {
                direction *= -1;
                curTime = 0;
            }
            moveVelocity = new Vector2(direction, 0) * speed;
        }

        animator.SetFloat("velocity", moveVelocity.x);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerAlive == true)
        {
            if (collision.collider.tag == "Player")
            {
                playerMovement.enabled = false;

                Debug.Log("You Died");
                playerAlive = false;
                Invoke("ResetLevel", restartDelay);
                Invoke("ReEnablePlayerMovement", restartDelay);
            }
        }
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReEnablePlayerMovement()
    {
        playerMovement.enabled = true;
    }
}