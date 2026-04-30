using GhostProject.Core;
using GhostProject.Data;
using NUnit.Framework;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.TestTools;

public class LevelLoaderTests
{
    [Test]
    public void LevelLoader_JSONParsing_ReturnsValidWorld()
    {
        string mockJson = "{\"world\": {\"title\": \"Test Project\", \"sectors\": []}}";

        WorldWrapper wrapper = JsonUtility.FromJson<WorldWrapper>(mockJson);

        Assert.IsNotNull(wrapper);
        Assert.AreEqual("Test Project", wrapper.world.title);
    }

    [Test]
    public void Core_WorldInitialization_HasEmptySectorList()
    {
        World world = new World();
        Assert.IsNotNull(world.sectors);
        Assert.AreEqual(0, world.sectors.Count);
    }
}
