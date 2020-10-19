using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovingPlatformStressTest
    {
        private bool sceneLoaded;
        private int stressValue = 2000;
        int actualValue = 0;


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
        public IEnumerator MovingPlatformNumberStressTest()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);
            GameObject testplatform = GameObject.Find("MovingRockPlatform_0");
            
            for(int i = 0; i<stressValue; i++)
            {
            GameObject platform = UnityEngine.Object.Instantiate(testplatform, Vector3.zero, Quaternion.identity) as GameObject;

            platform.transform.position = new Vector3(Random.Range(0,10f), Random.Range(5f,20f), 0f);

            actualValue ++;

            yield return new WaitForSeconds(.01f);

            }
            

            Debug.Log("Expected: " + stressValue + ", Actual; " + actualValue);
            Assert.AreEqual(stressValue, actualValue);


        }
    }
}
