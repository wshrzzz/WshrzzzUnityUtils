using UnityEngine;
using System.Collections;
using Wshrzzz.UnityUtils;

public class SpawnerTest : MonoBehaviour {

    public Transform testCube;


	// Use this for initialization
	void Start () {
        StartCoroutine(KeepSpawn());
	}

    IEnumerator KeepSpawn()
    {
        Spawner testSpawn = Spawner.InitSpawner();
        Spawner testSpawn2 = Spawner.InitSpawner();
        testSpawn.SetupSpawner(new Vector3(0f, 0f, 30f), Quaternion.identity, 10f, 5f, 30f, true);
        testSpawn2.SetupSpawner(new Vector3(0f, 0f, 80f), 15f, true);
        while (true)
        {
            testSpawn.Spawn(testCube, Quaternion.identity);
            testSpawn2.Spawn(testCube, Quaternion.identity);
            yield return new WaitForEndOfFrame();
        }
        testSpawn.DeinitSpawner();
    }
}
