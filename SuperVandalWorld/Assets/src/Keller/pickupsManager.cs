using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupsManager : MonoBehaviour
{    
    void OnTriggerEnter2D(Collider2D col)
    {
        var gObject = col.gameObject;
        if(gObject.CompareTag("Item"))
        {
            Item.collisionItem(col);
        }
        if(gObject.CompareTag("PowerUp"))
        {
            PowerUp.collisionPowerUp(col);
        }
    }
    
    public virtual int updateScore(GameObject gObject)
    {
        return 0;
    }

    public static void removeAsset(Collider2D other)
    {
        //remove asset from level via destroy function
        Destroy(other.gameObject);
    }
}