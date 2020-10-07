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

        public static int updateScore(string[] items, int[] numItems, int currentScore){

            for(int i = 0; i < items.Length; i++){
                switch(items[i]){
                    case("sapphire"):
                        currentScore += numItems[i] * 100;
                        break;
                    case("emerald"):
                        currentScore += numItems[i] * 200;
                        break;
                    case("ruby"):
                        currentScore += numItems[i] * 400;                        
                        break;
                    case("diamond"):
                        currentScore += numItems[i] * 2500;                        
                        break;
                    case("multiJump"):
                        currentScore += numItems[i] * 1000;
                        break;
                    case("powerAxe"):
                        currentScore += numItems[i] * 1000;
                        break;
                    case("badApple"):
                        currentScore -= numItems[i] * 1000;
                        break;
                    default:
                        currentScore += 0;
                        break;
                }
            }

            //if score would be negative, make it 0
            if(currentScore < 0){
                currentScore = 0;
            }
            
            return currentScore;
            
        }


        [UnityTest]
        public IEnumerator PickUpItemAddsPointsToScore()
        {
            //arrange
            int score = 0;

            string[] items = {"sapphire", "emerald", "ruby", "diamond"};
            int[] numItems = {5,3,4,2};

            //act
            score = updateScore(items, numItems, score);

            yield return null;

            //assert
            Assert.AreEqual(7700, score);
        }


        [UnityTest]
        public IEnumerator PickUpPowerUpsAddsPointsToScore(){
            //arrange
            int score = 0;
            string[] powerUps = {"multiJump", "powerAxe", "badApple"};
            int[] numPowerUps = {3,2,7};

            //act
            score = updateScore(powerUps, numPowerUps, score);

            yield return null;

            //assert
            Assert.AreEqual(0, score);
        }

        [UnityTest]
        public IEnumerator AddPointsForItemsBeforePowerUps(){

            //arrange
            int score = 0;
            string[] powerUps = {"multiJump", "powerAxe", "badApple"};
            int[] numPowerUps = {3,2,7};
            string[] items = {"sapphire", "emerald", "ruby", "diamond"};
            int[] numItems = {5,3,4,2};

            //act
            score = updateScore(items, numItems, score);
            score = updateScore(powerUps, numPowerUps, score);
            yield return null;

            //asset
            Assert.AreEqual(5700, score);
        }

        [UnityTest]
        public IEnumerator AddPointsForPowerupsBeforeItems(){

            //arrange
            int score = 0;
            string[] powerUps = {"multiJump", "powerAxe", "badApple"};
            int[] numPowerUps = {3,2,7};
            string[] items = {"sapphire", "emerald", "ruby", "diamond"};
            int[] numItems = {5,3,4,2};

            //act
            score = updateScore(powerUps, numPowerUps, score);
            score = updateScore(items, numItems, score);


            yield return null;

            //asset
            Assert.AreEqual(7700, score);
        }
    }
}