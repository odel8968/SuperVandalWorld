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
            var platform = GameObject.Find("MovingRockPlatform_0").GetComponent<MovingPlatform>();
            var speed = platform.setSpeed;
            //Check initial speed value
            Assert.That(speed, Is.EqualTo(5f));

           //Change speed value with function in MovingPlatform script
           //Check that value was updated correctly
            speed = platform.SetSpeed(10f);
            Assert.AreEqual(speed, 10f);

            //Change speed value with function in MovingPlatform script
           //Check that value was updated correctly
            speed = platform.SetSpeed(2f);
            Assert.AreEqual(speed, 2f);
        }

    } 
}
