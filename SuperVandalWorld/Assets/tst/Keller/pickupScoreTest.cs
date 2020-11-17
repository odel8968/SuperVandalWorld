using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class pickupPointTest
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

        public static int updateScore(string type, int value, int numItems, int currentScore)
        {

            for(int i=0; i < numItems; i++)
            {
                switch(type)
                {
                    case "Item":
                    {
                        currentScore += value;
                    }
                    break;
                    
                    case "PowerUp":
                    {
                        currentScore += value;
                    }
                    break;
                    default:
                    {
                        return currentScore;
                    }
                    
                }   

                //if score would be negative, make it 0
                if(currentScore < 0)
                {
                    currentScore = 0;
                }
            }
            return currentScore;
        }


        [UnityTest]
        public IEnumerator PickUpItemAddsPointsToScore()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            //arrange
            int score = 0;
            var sapphire = GameObject.Find("Sapphire");
            var emerald = GameObject.Find("Emerald");
            var ruby = GameObject.Find("Ruby");
            var diamond = GameObject.Find("Diamond");

            int[] numItems = {5,3,4,2};

            //act
            score = updateScore("Item", 100, numItems[0], score); //collect sapphires
            score = updateScore("Item", 200, numItems[1], score); //collect emeralds
            score = updateScore("Item", 400, numItems[2], score); //collect rubies
            score = updateScore("Item", 2500, numItems[3], score); //collect diamonds

            yield return null;

            //assert
            Assert.AreEqual(7700, score);
        }


        [UnityTest]
        public IEnumerator PickUpPowerUpsAddsPointsToScore()
        {
            //arrange
            int score = 0;
            var multiJump = GameObject.Find("MultiJump");
            var powerAxe = GameObject.Find("PowerAxe");
            var badApple = GameObject.Find("BadApple");
            int[] numPowerUps = {3,2,7};

            //act
            score = updateScore("PowerUp", 1000, numPowerUps[0], score); //collect multiJump
            score = updateScore("PowerUp", 1000, numPowerUps[1], score); //collect powerAxe
            score = updateScore("PowerUp", -1000, numPowerUps[2], score); //collect badApple

            yield return null;

            //assert
            Assert.AreEqual(0, score);
        }

        [UnityTest]
        public IEnumerator AddPointsForItemsBeforePowerUps()
        {

            //arrange
            int score = 0;
            var multiJump = GameObject.Find("MultiJump");
            var powerAxe = GameObject.Find("PowerAxe");
            var badApple = GameObject.Find("BadApple");
            int[] numPowerUps = {3,2,7};

            var sapphire = GameObject.Find("Sapphire");
            var emerald = GameObject.Find("Emerald");
            var ruby = GameObject.Find("Ruby");
            var diamond = GameObject.Find("Diamond");
            int[] numItems = {5,3,4,2};

            //act
            score = updateScore("Item", 100, numItems[0], score); //collect sapphires
            score = updateScore("Item", 200, numItems[1], score); //collect emeralds
            score = updateScore("Item", 400, numItems[2], score); //collect rubies
            score = updateScore("Item", 2500, numItems[3], score); //collect diamonds

            score = updateScore("PowerUp", 1000, numPowerUps[0], score); //collect multiJump
            score = updateScore("PowerUp", 1000, numPowerUps[1], score); //collect powerAxe
            score = updateScore("PowerUp", -1000, numPowerUps[2], score); //collect badApple
            
            yield return null;

            //asset
            Assert.AreEqual(5700, score);
        }

        [UnityTest]
        public IEnumerator AddPointsForPowerupsBeforeItems()
        {

            //arrange
            int score = 0;
            var multiJump = GameObject.Find("MultiJump");
            var powerAxe = GameObject.Find("PowerAxe");
            var badApple = GameObject.Find("BadApple");
            int[] numPowerUps = {3,2,7};

            var sapphire = GameObject.Find("Sapphire");
            var emerald = GameObject.Find("Emerald");
            var ruby = GameObject.Find("Ruby");
            var diamond = GameObject.Find("Diamond");
            int[] numItems = {5,3,4,2};

            //act
            score = updateScore("PowerUp", 1000, numPowerUps[0], score); //collect multiJump
            score = updateScore("PowerUp", 1000, numPowerUps[1], score); //collect powerAxe
            score = updateScore("PowerUp", -1000, numPowerUps[2], score); //collect badApple

            score = updateScore("Item", 100, numItems[0], score); //collect sapphires
            score = updateScore("Item", 200, numItems[1], score); //collect emeralds
            score = updateScore("Item", 400, numItems[2], score); //collect rubies
            score = updateScore("Item", 2500, numItems[3], score); //collect diamonds

            yield return null;

            //asset
            Assert.AreEqual(7700, score);
        }
    }
}