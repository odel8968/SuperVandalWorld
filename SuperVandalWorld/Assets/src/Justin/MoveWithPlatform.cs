using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : EnvObject
{
        Transform platform;
        Transform player;

        void Start()
        {
                platform = GetComponent<Transform>();
                player = GameObject.Find("Player").GetComponent<Transform>();
        }


        public override void OnCollisionEnter2D(Collision2D collision)
        {
                //collision.collider.transform.SetParent(transform); 
                player.transform.localPosition = platform.transform.localPosition;
                //GameObject.Find("Main Camera").transform.SetParent(transform);           
        }

        public override void OnCollisionExit2D(Collision2D collision)
        {
                collision.collider.transform.SetParent(null);
                //GameObject.Find("Main Camera").transform.SetParent(null);
        }
}
