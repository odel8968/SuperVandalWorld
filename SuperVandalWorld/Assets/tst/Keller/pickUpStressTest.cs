using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class pickUpsStressTest
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

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator stressSpawnDiamonds()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);
            //GameObject diamond = GameObject.Find("Diamond");
           // GameObject axe = UnityEngine.Object.Instantiate(axeProj, Vector3.zero, Quaternion.identity);

            int MaxTested = 1000;
            int actualNum = 0;
            GameObject proj = null;
            float currentFPS = 1.0f / Time.deltaTime;
            float minFPS = 60;
            bool breakLoop = false;
            GameObject diamond = GameObject.Find("Diamond");

            //nested for loops for doubling spawn amount
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < MaxTested; j++)
                {    
                    //instantiate game object and change name for testing purposes. Spawn in a random pattern. 
                    proj = GameObject.Instantiate(diamond, Vector3.zero, Quaternion.identity);
                    proj.name = "testDiamond";
                    proj.transform.position = new Vector3(Random.Range(-15f,15f), Random.Range(5f,25f), Random.Range(-15f,15f));
                    actualNum++;                    //increase test value

                    currentFPS = 1.0f / Time.deltaTime;         //calculate fps for last frame
                    
                    /*check if current fps is less than minimum acceptable fps value and that
                      the scene has been active for longer than 1 second. */
                    if(currentFPS < minFPS && Time.time > 1)
                    {
                        Debug.Log("<color=red>Failed</color> at " + MaxTested + " diamonds");
                        Debug.Log("Current FPS = <color=red> " + currentFPS + "</color>");
                        breakLoop = true;
                        break;
                    }

                    //Pause for 0.2 seconds every time 200 diamonds are instantiated
                    if(actualNum % 200 == 0)
                    {
                        yield return new WaitForSeconds(.2f);
                    }
                }

                //If fps dropped below acceptable values, break out of both for loops
                if(breakLoop)
                {
                    break;
                }

                //Wait for 2 seconds to read Debug Logs
                yield return new WaitForSeconds(2.0f);
                Debug.Log("<color=green>Passed</color> at " + MaxTested + " diamonds");
                Debug.Log("Current FPS = <color=green> " + currentFPS + "</color>");

                //delete all diamonds spawned during last test
                var DiamondObjs = GameObject.FindGameObjectsWithTag("Item");
                foreach(var item in DiamondObjs)
                {
                    if(item.name.Contains("testDiamond"))
                    {
                        UnityEngine.Object.Destroy(item);
                    }   
                }

                actualNum = 0;                                          //reset test number to 0
                MaxTested *= 2;                                         //double max test value
                yield return new WaitForSeconds(2.0f);
            }
            
            yield return null;
            Assert.AreEqual(MaxTested, actualNum);
        }
    }
}
