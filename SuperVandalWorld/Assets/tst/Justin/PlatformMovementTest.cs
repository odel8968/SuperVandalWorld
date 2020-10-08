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
            var platform = GameObject.Find("MovingRockPlatform_0").GetComponent<MovingPlatform>();
            var pointB = GameObject.Find("PointB").GetComponent<MovingPlatform>();
            yield return new WaitForSeconds((20));

            Assert.That(platform, Is.EqualTo(pointB));
        }
    } 
}
