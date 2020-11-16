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

        public static int updateScore(GameObject other, int numItems, int currentScore)
        {

            string pickUpType = other.gameObject.tag;
            for(int i=0; i < numItems; i++)
            {
                switch(other.gameObject.tag)
                {
                    case "Item":
                    {
                        var obj = other.GetComponent<Item>();
                        currentScore += obj.scoreValue;
                    }
                    break;
                    
                    case "PowerUp":
                    {
                        var obj = other.GetComponent<PowerUp>();
                        currentScore += obj.scoreValue;
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
            score = updateScore(sapphire, numItems[0], score); //collect sapphires
            score = updateScore(emerald, numItems[1], score); //collect emeralds
            score = updateScore(ruby, numItems[2], score); //collect rubies
            score = updateScore(diamond, numItems[3], score); //collect diamonds

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
            score = updateScore(multiJump, numPowerUps[0], score); //collect sapphires
            score = updateScore(powerAxe, numPowerUps[1], score); //collect emeralds
            score = updateScore(badApple, numPowerUps[2], score); //collect rubies

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
            score = updateScore(sapphire, numItems[0], score); //collect sapphires
            score = updateScore(emerald, numItems[1], score); //collect emeralds
            score = updateScore(ruby, numItems[2], score); //collect rubies
            score = updateScore(diamond, numItems[3], score); //collect diamonds

            score = updateScore(multiJump, numPowerUps[0], score); //collect sapphires
            score = updateScore(powerAxe, numPowerUps[1], score); //collect emeralds
            score = updateScore(badApple, numPowerUps[2], score); //collect rubies
            
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
            score = updateScore(multiJump, numPowerUps[0], score); //collect multijump
            score = updateScore(powerAxe, numPowerUps[1], score); //collect powerAxe
            score = updateScore(badApple, numPowerUps[2], score); //collect badApple

            score = updateScore(sapphire, numItems[0], score); //collect sapphires
            score = updateScore(emerald, numItems[1], score); //collect emeralds
            score = updateScore(ruby, numItems[2], score); //collect rubies
            score = updateScore(diamond, numItems[3], score); //collect diamonds

            yield return null;

            //asset
            Assert.AreEqual(7700, score);
        }
    }
}