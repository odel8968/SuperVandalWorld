using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests {
    public class daEnemyTest {

        private bool sceneLoaded;
        private bool enemyIsMoving;
        private bool enemyMovingToPlayer;



        [OneTimeSetUp]
        public void OneTimeSetup() {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
            sceneLoaded = true;
        }

        // Test to check if the enemy speed is set
        [UnityTest]
        public IEnumerator enemySpeedIsSet() {
            
            yield return new WaitWhile(() => sceneLoaded == false);

            var enemy = GameObject.Find("Test_Enemy_Square").GetComponent<enemyController>();
            var speed = enemy.enemySpeed;

            yield return new WaitForSeconds(1f);

            Assert.That(speed, Is.EqualTo(2.5f));   
        }

        // Test to see if the enemy is moving
        [UnityTest]
        public IEnumerator isEnemyMoving() {

        yield return new WaitWhile(() => sceneLoaded == false);

            var enemy = GameObject.Find("Test_Enemy_Square").GetComponent<enemyController>();
            Vector2 enemySPos = enemy.initPos;

            yield return new WaitForSeconds(1f);
            
            Vector2 enemyFPos = enemy.transform.position;

            if (enemySPos.x != enemyFPos.x) {
                enemyIsMoving = true;
            }

            Assert.That(enemyIsMoving, Is.EqualTo(true));
        }

        // Test to check if the enemy was able to locate the player
        [UnityTest]
        public IEnumerator movingTowardsPlayer() {

            yield return new WaitWhile(() => sceneLoaded == false);

            var enemy = GameObject.Find("Test_Enemy_Square").GetComponent<enemyController>();
            var player = GameObject.Find("Player").GetComponent<Character_Movement>();

            float initDist = Vector2.Distance(player.transform.position, enemy.transform.position);

            yield return new WaitForSeconds(2f);

            float distFinal = Vector2.Distance(player.transform.position, enemy.transform.position);            

            if (distFinal < initDist) {
                enemyMovingToPlayer = true;
            } else {
                enemyMovingToPlayer = false;
            }

            Assert.That(enemyMovingToPlayer, Is.EqualTo(true));
        }
    }
}
