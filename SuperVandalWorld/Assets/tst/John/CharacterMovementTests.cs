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
            yield return new WaitForSeconds(2f);
            Assert.AreNotEqual(Player.transform.position.y, initialpos);
        }

        [UnityTest]
        public IEnumerator movement_test_right()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            var Player = GameObject.Find("Player").GetComponent<Character_Movement>();
            float initialpos = Player.transform.position.x;
            float speed = 10f;
            for (int i = 0; i < 750; i++)
            {
                Player.rb.velocity = new Vector2(speed, 0);
                yield return null;
            }
            Assert.AreNotEqual(Player.transform.position.x, initialpos);
        }

        [UnityTest]
        public IEnumerator movement_test_left()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            var Player = GameObject.Find("Player").GetComponent<Character_Movement>();
            float initialpos = Player.transform.position.x;
            float speed = -10f;
            for (int i = 0; i < 750; i++)
            {
                Player.rb.velocity = new Vector2(speed, 0);
                yield return null;
            }
            Assert.AreNotEqual(Player.transform.position.x, initialpos);
        }

        [UnityTest]
        public IEnumerator jump_stress_test()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            var Player = GameObject.Find("Player").GetComponent<Character_Movement>();
            var testPlatform = GameObject.Find("MovingRockPlatform_0");
            var testShiny = GameObject.Find("Sapphire");
            bool shinyCollected = true;
            
            GameObject platform = UnityEngine.Object.Instantiate(testPlatform, Vector3.zero, Quaternion.identity) as GameObject;
            platform.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 10f, 0f);
            platform.GetComponent<MovingPlatform>().setSpeed = 0f;

            for (int i = 0; i < 4; i++)
            {
                GameObject anotherPlatform = UnityEngine.Object.Instantiate(testPlatform, Vector3.zero, Quaternion.identity) as GameObject;
                anotherPlatform.transform.position = new Vector3(Player.transform.position.x + (5f * i), Player.transform.position.y + 10f, 0f);
                anotherPlatform.GetComponent<MovingPlatform>().setSpeed = 0f;

                GameObject yetAnotherPlatform = UnityEngine.Object.Instantiate(testPlatform, Vector3.zero, Quaternion.identity) as GameObject;
                yetAnotherPlatform.transform.position = new Vector3(Player.transform.position.x + (5f * i), Player.transform.position.y + 10f, 0f);
                yetAnotherPlatform.GetComponent<MovingPlatform>().setSpeed = 0f;
            }
     
            Player.jumpForce = 15f;
            for (int i = 0; i < 25; i++)
            {
                GameObject shiny = UnityEngine.Object.Instantiate(testShiny, Vector3.zero, Quaternion.identity) as GameObject;
                shiny.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5f, 0f);
                Player.Jump(true);
                Debug.Log("Jump force = " + Player.jumpForce);
                yield return new WaitForSeconds(1.5f);
                if (Player.transform.position.y > platform.transform.position.y) break; //if player goes through the platform, exit the loop
                if (shiny != null) //if the collectable was not collected, exit the loop
                {
                    shinyCollected = false;
                    break;
                }
                Player.jumpForce *= 10;
            }
            Assert.IsTrue(Player.transform.position.y < platform.transform.position.y, "Player has broken through the platform with a speed of " + Player.jumpForce);
            Assert.IsTrue(shinyCollected, "Player has failed to collect a gem, even with a jump speed of " + Player.jumpForce);
        }
    }
}
