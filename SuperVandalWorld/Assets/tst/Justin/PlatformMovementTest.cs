using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlatformMovementTest
    {
        private bool sceneLoaded;


        [OneTimeSetUp]
        public void loadedLevel()
        {

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {

            sceneLoaded = true;
        }

        [UnityTest]
        public IEnumerator MovingPlatformSpeed()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);
            var platform = GameObject.Find("MovingPlatform1").GetComponent<MovingPlatform>();

            Assert.That(platform.moveSpeed, Is.EqualTo(5f));
        }

        [UnityTest]
        public IEnumerator MovingPlatformToPointB()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);
            var platform = GameObject.Find("MovingPlatform1/MovingRockPlatform1");
            var pointB = GameObject.Find("MovingPlatform1/PointB").transform.position.x;
            var pointA = GameObject.Find("MovingPlatform1/PointA").transform.position.x;
            var platSpeed = GameObject.Find("MovingPlatform1").GetComponent<MovingPlatform>();

            //while(pPos < pointB)
           // {
           //     pPos = platform.transform.position.x;
           //     Debug.Log(pPos); 
           // }
            

            //yield return new WaitForSeconds((20));

            Assert.That(platform, Is.EqualTo(pointB));
        } 

        
    }
}
