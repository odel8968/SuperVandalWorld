using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvObject : MonoBehaviour
{
        protected bool playerAlive = false;

   void FixedUpdate()
   {
           if(Input.GetKey("n"))
           {
                   LoadNextLevel();
           }
   }

   public virtual void OnCollisionEnter2D(Collision2D collision)
    {
            Debug.Log("Collision Entered");                  
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
            Debug.Log("Collision Exited");           
    }

   public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerAlive = true;
    }

    void LoadNextLevel()
    {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    

}
