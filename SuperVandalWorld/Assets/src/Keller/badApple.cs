using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badApple : MonoBehaviour
{
    public int healthChange = -1;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        var apple = col.gameObject.GetComponent<badApple>();
        GameObject.Find("Player").GetComponent<powerAxe>().enabled = false;
        GameObject.Find("Player").GetComponent<multiJump>().enabled = false;

        Destroy(apple);
    }
}
