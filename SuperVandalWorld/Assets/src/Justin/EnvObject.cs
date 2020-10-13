using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvObject : MonoBehaviour
{
   public virtual void OnCollisionEnter2D(Collision2D collision)
    {
            Debug.Log("Collision Entered");                  
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
            Debug.Log("Collision Exited");           
    }

    

}
