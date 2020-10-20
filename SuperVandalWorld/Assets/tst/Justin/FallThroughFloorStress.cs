using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class FallThroughFloorStress
    {
       bool sceneLoaded;
       
        [OneTimeSetUp]
        public void loadedLevel()
        {

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene("Level 3", LoadSceneMode.Single);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {

            sceneLoaded = true;
        }      

        [UnityTest]
        public IEnumerator FallThroughFloorStressTest()
        {
            float maxSpeed = 5;
            bool breaklp = false;

            //Rigidbody2D platformRB;

            GameObject testplatform = GameObject.Find("StatSnowPlatform");
            GameObject player = GameObject.Find("Player");
            GameObject platform = null;


            for(int outlp = 0; outlp < 20; outlp++)
            {
                for(int inlp = 0; inlp < 3; inlp ++)
                {
                    platform = UnityEngine.Object.Instantiate(testplatform, Vector3.zero, Quaternion.identity);
                    platform.name = "testPlatform";
                    platform.transform.position = new Vector3(0f, 10f, 0f);
                    platform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -maxSpeed);

                   
                    if(player.transform.position.y <= -1f)
                    {
                        Debug.Log("Failed at " + maxSpeed + " Platform speed");
                        breaklp = true;
                        break;
                    }

                    yield return new WaitForSeconds(5.0f);

                    var PlatformObj = GameObject.FindGameObjectsWithTag("Platform");
                    foreach (var obj in PlatformObj)
                    {
                        if(obj.name.Contains("testPlatform") )
                        {
                            UnityEngine.Object.Destroy(obj);
                        }
                    }
                    
                }

                //break out of outer loop
                if(breaklp)
                    {
                        break;
                    }

                //mulitply stress value
                maxSpeed *=2;
                
            }

            Debug.Log("Player falls through Ground at " + maxSpeed + " Platform speed");

            //fails if breaks out of test
            Assert.AreEqual(breaklp, false);
            
        }
    }
}
