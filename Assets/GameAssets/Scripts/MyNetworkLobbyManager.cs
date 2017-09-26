using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkLobbyManager : NetworkLobbyManager {

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayerGO, GameObject gamePlayerGO)
    {
        Debug.LogWarning("OnLobbyServerSceneLoadedForPlayer");

        LobbyPlayer lobbyPlayer = lobbyPlayerGO.GetComponent<LobbyPlayer>();
        Player player = gamePlayerGO.GetComponent<Player>();
        player.SetPlayerColor(lobbyPlayer.GetPlayerColor());
        player.SetPlayerName(lobbyPlayer.GetPlayerName());

        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayerGO, gamePlayerGO);
    }
}
