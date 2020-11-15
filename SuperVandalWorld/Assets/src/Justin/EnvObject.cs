using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvObject : MonoBehaviour
{
        protected bool playerAlive = true;
        private GameObject[] players;
        private GameObject[] mainCamera;

        public GameObject playerPrefab;
        public GameObject camPrefab;

        private Vector3 tempPos;

   void Start()
   {
        players = GameObject.FindGameObjectsWithTag("Player");
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera");

        //Instantiate player if if doesn't exist in the scene
        if(players.Length < 1)
        {
                tempPos = GameObject.FindWithTag("StartPos").transform.position;
                GameObject player = Instantiate(playerPrefab, tempPos, Quaternion.identity);
                player.gameObject.name = "Player";
        }

        //Instantiate Camera if it doesn't exist in the scene
        if(mainCamera.Length < 1)
        {
                tempPos = GameObject.FindWithTag("StartPos").transform.position;
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

   public virtual void OnCollisionEnter2D(Collision2D collision)
    {     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerAlive = true;
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
            collision.collider.transform.SetParent(null);
    }
        
   public void LoadNextLevel()
    {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousLevel()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    

}