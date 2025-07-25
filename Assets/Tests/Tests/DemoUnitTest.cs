using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DemoUnitTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void DemoUnitTestSimplePasses()
    {
        // Use the Assert class to test conditions

        Assert.AreEqual(0, 0);
        Assert.AreEqual(1, 1);

        Assert.IsTrue(true);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DemoUnitTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
