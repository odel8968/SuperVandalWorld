using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class CameraTest
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
        public IEnumerator Camera_Set_To_Follow_Player()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);

            //check to see if Player object is set to camera follow object
            var mainCamera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
            var player = GameObject.Find("Player");

            Debug.Log(mainCamera.followObject);
            Debug.Log(player);

            Assert.AreSame(mainCamera.followObject, player);

        }

        [UnityTest]
        public IEnumerator Camera_X_Offset_Set()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);

            //Check if code reading x-offset is set correctly
            var mainCamera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
            var xOffset = mainCamera.followOffset.x;

            Debug.Log(mainCamera.followOffset.x);

            Assert.AreEqual(xOffset, 30);
        }

        [UnityTest]
        public IEnumerator Camera_Y_Offset_Set()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);

            //Check if code reading y-offset is set correctly
            var mainCamera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
            var yOffset = mainCamera.followOffset.y;

            Debug.Log(mainCamera.followOffset.y);
            
            Assert.AreEqual(yOffset, 20);
        }

        [UnityTest]
        public IEnumerator Camera_Speed_Set()
        {
            yield return new WaitWhile(()=>sceneLoaded == false);

            //Check if code reading y-offset is set correctly
            var mainCamera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
            var speed = mainCamera.speed;

            Debug.Log(speed);
            
            Assert.AreEqual(speed, 7);
        }
    }

}
