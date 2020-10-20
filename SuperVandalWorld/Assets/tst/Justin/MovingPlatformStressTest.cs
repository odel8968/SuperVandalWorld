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

        bool sceneLoaded;
       
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

            int stressValue = 1;
            int actualValue = 0;
            float curFPS = 1.0f / Time.deltaTime;
            float tFPS = 30;
            bool breaklp = false;


            GameObject testplatform = GameObject.Find("MovingRockPlatform_0");
            
            //Outer loop to double number of platforms spawned
            for(int outlp = 0; outlp < 20; outlp++)
            {
                //Inner loop spawn platforms and increment # to stress
                for(int innerlp = 0; innerlp < stressValue; innerlp ++)
                {
                    //Instantiate platform objects that move
                    GameObject platform = UnityEngine.Object.Instantiate(testplatform, Vector3.zero, Quaternion.identity) as GameObject;
                    platform.name = "testPlatform";
                    platform.transform.position = new Vector3(Random.Range(0,10f), Random.Range(5f,20f), 0f);
                    actualValue++;

                    //Get current framerate
                    curFPS = 1.0f / Time.deltaTime;

                    //if current framerate drops below target frame rate, break out of loop
                    if(curFPS < tFPS && Time.time > 1)
                    {
                        Debug.Log("Failed at " + actualValue + " Moving Rock Platforms");
                        Debug.Log("Current FPS = " + curFPS);
                        breaklp = true;
                        break;
                    }

                    yield return new WaitForSeconds(.01f);


                }

                //break out of outer loop
                if(breaklp)
                {
                    break;
                }

                //Delete objects created for test
                var PlatformObj = GameObject.FindGameObjectsWithTag("MovingPlatform");
                foreach (var obj in PlatformObj)
                {
                    if(obj.name.Contains("testPlatform") )
                    {
                        UnityEngine.Object.Destroy(obj);
                    }
                }

                //mulitply stress value
                actualValue = 0;
                stressValue *=2;
                yield return new WaitForSeconds(1.0f);
            

            }

            yield return null;
            Assert.AreEqual(stressValue, actualValue);


        }
    }
}
