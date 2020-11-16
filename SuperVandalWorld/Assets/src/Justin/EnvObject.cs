using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvObject : MonoBehaviour
{
        //track if player is alive
        public bool playerAlive = true;

        //array to hold players
        private GameObject[] players;

        //array to hold cameras
        private GameObject[] mainCamera;

        //player prefab to instantiate
        public GameObject playerPrefab;

        //camera prefab to instantiate
        public GameObject camPrefab;

        //Vector to hold starting position
        private Vector3 tempPos;

   void Start()
   {
           //Find the player and camera objects
        players = GameObject.FindGameObjectsWithTag("Player");
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera");

        //Instantiate player at the start position if if doesn't exist in the scene
        if(players.Length < 1)
        {
                tempPos = GameObject.FindWithTag("StartPos").transform.position;
                GameObject player = Instantiate(playerPrefab, tempPos, Quaternion.identity);
                player.gameObject.name = "Player";
        }

        //Instantiate Camera at the start position if it doesn't exist in the scene
        if(mainCamera.Length < 1)
        {
                tempPos = GameObject.FindWithTag("StartPos").transform.position;
                
                //adjust z axis so Camera sees the Scene
                tempPos.z -= 1;
                GameObject cam = Instantiate(camPrefab, tempPos, Quaternion.identity);
                cam.gameObject.name = "Main Camera";
        }
   }

   void FixedUpdate()
   {

           /*load next level if n is pressed
           used for testing - remove from final product*/
           if(Input.GetKey("n"))
           {
                   LoadNextLevel();
           }

           /*load next level if n is pressed
           used for testing - remove from final product*/
           if(Input.GetKey("b"))
           {
                   LoadPreviousLevel();
           }
   }

        /*Static bound method for environment collisions. This function will be called if 
        the method is not over-ridden in other functions*/
   public virtual void OnCollisionEnter2D(Collision2D collision)
    {     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerAlive = true;
    }

        /*Static bound method for environment exiting collisions. This function will be called if 
        the method is not over-ridden in other functions*/
    public virtual void OnCollisionExit2D(Collision2D collision)
    {
            collision.collider.transform.SetParent(null);
    }

       //public function to load the next level 
   public void LoadNextLevel()
    {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

        //public function to load a previous level - probably removed for final product
    public void LoadPreviousLevel()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    

}