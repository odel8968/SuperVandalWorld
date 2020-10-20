using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class SquareTest
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

        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator SquareTestInitialCondition()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            // Use the Assert class to test conditions
            var square = GameObject.Find("Square").GetComponent<Square>();

            Assert.That(square.direction, Is.EqualTo(1));
        }


        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator SquareTestBoundary1()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            // Use the Assert class to test conditions
            var square = GameObject.Find("Square").GetComponent<Square>();

            // boundary is curTime == movetime
            yield return new WaitForSeconds(square.moveTime +square.moveTime* 0.5f);

            Assert.That(square.direction, Is.EqualTo(-1));
        }

        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator SquareTestBoundary2()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            var square = GameObject.Find("Square").GetComponent<Square>();

            // boundary is curTime == movetime
            yield return new WaitForSeconds(square.moveTime * 2 + square.moveTime * 0.5f);

            Assert.That(square.direction, Is.EqualTo(1));
        }


        [UnityTest]
        public IEnumerator SquareTestStress()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            var square = GameObject.Find("Square").GetComponent<Square>();

            var posX = square.transform.position.x;
            const int limit = 10;
            var limitX = posX + square.speed * square.moveTime + limit;


            yield return new WaitWhile(
                () =>
                {
                    Assert.That(square.transform.position.x, Is.LessThan(limitX));

                    if (square.curTime < 0.01f)
                    {
                        square.speed += 1f;
                        //Debug.Log("ASSERT CHECK: " + square.speed + " -- " + square.transform.position.x);
                    }

                    return square.transform.position.x < limitX;
                }
            );

            //Assert.That(square.direction, Is.EqualTo(-1));
            Debug.Log("ASSERT CHECK: " + square.speed + " -- " + square.transform.position.x + " -- " + limitX);
            Assert.That(square.transform.position.x < limitX);
        }

    }
}
