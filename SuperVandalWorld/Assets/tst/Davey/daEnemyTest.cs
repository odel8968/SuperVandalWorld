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
        private bool enemyIsAtPlayerPosition;

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

            var enemy = GameObject.Find("Enemy_Square").GetComponent<enemyController>();
            var speed = enemy.enemySpeed;

            yield return new WaitForSeconds(1f);

            Assert.That(speed, Is.EqualTo(2.5f));   
        }

        // TODO: Send Heba what my test do and why
        //       What i will be working on next week
        //       Check to see if i can change the size of the Gantt Chart

        // Test to see if the enemy is moving
        [UnityTest]
        public IEnumerator isEnemyMoving() {

            yield return new WaitWhile(() => sceneLoaded == false);

            var enemy = GameObject.Find("Enemy_Square").GetComponent<enemyController>();
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
        public IEnumerator movesTowardsPlayer() {

            yield return new WaitWhile(() => sceneLoaded == false);

            var enemy = GameObject.Find("Enemy_Square").GetComponent<enemyController>();
            var player = GameObject.Find("Player").GetComponent<Character_Movement>();

            var playerPos = player.transform.position;
            var enemyPos = enemy.transform.position;

            yield return new WaitForSeconds(2f);

            if (playerPos == enemyPos) {
                enemyIsAtPlayerPosition = true;
            }
            Assert.That(enemyIsAtPlayerPosition, Is.EqualTo(true));
        }

        // Test to see if the enemy is visible
    }
}
