using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Diagnostics;
//using System.Media;
//using System.Net;

namespace Tests
{
    public class UITest
    {
        private bool sceneLoaded;

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {

            sceneLoaded = true;
        }

        [OneTimeSetUp]
        public void loadedLevel()
        {

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }


        [UnityTest]
        public IEnumerator PauseFunctionTest()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            var gobject = GameObject.Find("UI").GetComponent<PauseMenu>();
            var gameSpeed = Time.timeScale;

            Assert.AreEqual(gameSpeed, 1.0f);


            gobject.pauseControl();

            gameSpeed = Time.timeScale;
            Assert.AreEqual(gameSpeed, 0f);

            gobject.pauseControl();

            gameSpeed = Time.timeScale;
            Assert.AreEqual(gameSpeed, 1f);

            yield return null;

        }

        [UnityTest]
        public IEnumerator ScoreTest()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            var gobject = GameObject.Find("Score").GetComponent<UIManager>();
            var score = gobject.GetScore();
            Assert.AreEqual(score, 0);

            gobject.AddScore(15);

            score = gobject.GetScore();
            Assert.AreEqual(score, 15);


            gobject.ResetScore();
            score = gobject.GetScore();
            Assert.AreEqual(score, 0);

            yield return null;

        }

        [UnityTest]
        public IEnumerator VolumeTest()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            var gobject = GameObject.Find("UI").GetComponent<PauseMenu>();
            var volume = AudioListener.volume;

            Assert.AreEqual(volume, 1f);

            gobject.SetVolume(.5f);
            volume = AudioListener.volume;

            Assert.AreEqual(volume, .5f);

            gobject.SetVolume(0f);
            volume = AudioListener.volume;

            Assert.AreEqual(volume, 0f);

            gobject.SetVolume(1f);
            volume = AudioListener.volume;

            Assert.AreEqual(volume, 1f);

            yield return null;

        }

        /*[UnityTest]
        public IEnumerator SoundStressTest()
        {

            yield return new WaitWhile(() => sceneLoaded == false);

            var sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();

            int count = 1000;
            int i = 0;


            while (i < count)
            {

                if ((i % 35) == 0)
                    sounds.PlaySound("Theme");

                yield return new WaitForEndOfFrame();

                i++;
            }
            //yield return new WaitForSecondsRealtime(2);

            Assert.That(i, Is.EqualTo(count));
        }*/

        [UnityTest]
        public IEnumerator UIStressTest()
        {

            yield return new WaitWhile(() => sceneLoaded == false);

            var menu = GameObject.Find("UI").GetComponent<PauseMenu>();

            int count = 100;
            int i = 0;


            var gobject = GameObject.Find("Score").GetComponent<UIManager>();
            var score = gobject.GetScore();
            int scoreAdd = 0;
            double totalScore = 0;
            Assert.AreEqual(score, 0);

            
            gobject.ResetScore();
            score = gobject.GetScore();
            Assert.AreEqual(score, 0);

            yield return null;

            while (i < count)
            {
                scoreAdd = (int) Math.Pow(2, i);
                gobject.AddScore(scoreAdd);
                totalScore += scoreAdd;

                score = gobject.GetScore();
                Assert.AreEqual(score, (int)totalScore);
                

                i++;
            }

            UnityEngine.Debug.Log("Score : ");
            UnityEngine.Debug.Log(score);

            Assert.That(i, Is.EqualTo(count));
        }

    }
}
