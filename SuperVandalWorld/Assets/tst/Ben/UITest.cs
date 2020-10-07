using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class UITest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UITestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PauseFunctionTest()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            
            var gameSpeed = Time.timeScale;
            Assert.AreEqual(gameSpeed, 1.0f);

            yield return null;

        }

        public IEnumerator ScoreTest()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            var score = GameObject.Find("Score").GetComponent<Text>();
            Assert.AreEqual(score, "0");

            yield return null;

        }
    }
}
