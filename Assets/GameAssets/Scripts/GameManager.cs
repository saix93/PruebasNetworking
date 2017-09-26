using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    bool gameStarted = false;
    bool gameOver = false;

	void Update () {
        if(isServer)
        {
            if(!gameStarted)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                if(players.Length > 1)
                {
                    gameStarted = true;
                }
            }
            if (gameStarted && !gameOver)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                if (players.Length == 1)
                {
                    players[0].GetComponent<Player>().Victory();
                    gameOver = true;
                }
            }            
        }
    }
}
