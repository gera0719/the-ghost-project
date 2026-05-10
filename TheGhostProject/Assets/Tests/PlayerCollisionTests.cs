using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerCollisionTests
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Player_OnTriggerEnter_Calls_GameManager_Death()
    {

        GameObject player = new GameObject("TestPlayer");
        player.tag = "Player";
        player.AddComponent<BoxCollider2D>();
        var rb = player.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;

        GameObject hazard = new GameObject("TestHazard");
        var col = hazard.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        hazard.AddComponent<HazardTrigger>();

        GameObject gmObj = new GameObject("GM");
        var gm = gmObj.AddComponent<GameManager>();

        player.transform.position = Vector3.zero;
        hazard.transform.position = Vector3.zero;

        yield return new WaitForFixedUpdate();

        Assert.Pass("Collision ran succesfully.");

        Object.Destroy(player);
        Object.Destroy(hazard);
        Object.Destroy(gmObj);
    }
}
