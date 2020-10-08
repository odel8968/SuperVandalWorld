using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class CharacterMovementTests
    {
        private bool sceneLoaded;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            sceneLoaded = true;
        }
        
        [UnityTest]
        public IEnumerator jump_test()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            var Player = GameObject.Find("Player").GetComponent<Character_Movement>();
            float initialpos = Player.transform.position.y;
            Player.Jump(true);
            yield return null;
            Assert.AreNotEqual(Player.transform.position.y, initialpos);
        }

        [UnityTest]
        public IEnumerator movement_test()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            var Player = GameObject.Find("Player").GetComponent<Character_Movement>();
            float initialpos = Player.transform.position.x;
            Player.speed = 10;
            Player.rb.velocity = new Vector2(Player.speed, Player.rb.velocity.y);
            yield return null;
            Assert.AreNotEqual(Player.transform.position.x, initialpos);
        }
    }
}
