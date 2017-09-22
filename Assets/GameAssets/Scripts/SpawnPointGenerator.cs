using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnPointGenerator : NetworkBehaviour {

    [SerializeField] int nSpawnPoints = 20;
    [SerializeField] Rect limits = new Rect(-100, -75, 200, 150);

    void Start () {

        if(isServer)
        {
            for (int i = 0; i < nSpawnPoints; i++)
            {
                GameObject newSpawnPoint = new GameObject("Spawn Point " + i);
                newSpawnPoint.AddComponent<NetworkStartPosition>();
                newSpawnPoint.transform.position = new Vector3(
                    Random.Range(limits.xMin, limits.xMax),
                    Random.Range(limits.yMin, limits.yMax),
                    0);
            }
        }
	}
}
