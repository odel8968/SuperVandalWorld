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
        public void loadedLevel(){
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1){
            sceneLoaded = true;
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator stressSpawnDiamonds()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);
            GameObject axeProj = GameObject.Find("Diamond");
           // GameObject axe = UnityEngine.Object.Instantiate(axeProj, Vector3.zero, Quaternion.identity);

            int MaxTested = 120000;
            int actualNum = 0;
            GameObject proj = null;
            

            for(int i = 0; i < MaxTested; i++){

                proj = GameObject.Instantiate(axeProj, Vector3.zero, Quaternion.identity);
                proj.transform.position = new Vector3(Random.Range(-15f,15f), Random.Range(5f,25f), Random.Range(-15f,15f));
                actualNum++;
                if(actualNum % 2000 == 0){
                    yield return new WaitForSeconds(0.4f);
                }
                
            }
            
            yield return null;
            Debug.Log("Expected: " + MaxTested + ", Actual: " + actualNum);
            Assert.AreEqual(MaxTested, actualNum);
        }
    }
}
