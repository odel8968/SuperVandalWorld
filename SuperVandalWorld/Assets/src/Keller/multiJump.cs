using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class multiJump : PowerUp
{
    public KeyCode abilityKey = KeyCode.J;

    void OnCollisionEnter(Collision col)
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(abilityKey))
        {
            //Character_Movement.Jump(true);
        }
    }
}

