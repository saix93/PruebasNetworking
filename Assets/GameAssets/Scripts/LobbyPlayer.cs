using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkLobbyPlayer {
        
    [SerializeField] LobbyPlayerGUI lobbyPlayerGUIPrefab;
    
    LobbyPlayerGUI _lobbyPlayerGUI;

    [SyncVar(hook ="NameChangedOnServer")]
    string _playerName;

    [SyncVar(hook ="ColorChangedOnServer")]
    Color _playerColor = Color.white;

    public string GetPlayerName()
    {
        return _playerName;
    }

    public Color GetPlayerColor()
    {
        return _playerColor;
    }
        
	public override void OnStartClient()
    {
        base.OnStartClient();

        _lobbyPlayerGUI = Instantiate(lobbyPlayerGUIPrefab);
        _lobbyPlayerGUI.SetLobbyPlayer(this);
        _lobbyPlayerGUI.SetPlayerName(_playerName);
        _lobbyPlayerGUI.SetPlayerImageColor(_playerColor);

        FindObjectOfType<GameLobbyCanvas>().AddLobbyPlayer(_lobbyPlayerGUI);
    }

    public void NameChanged(string name)
    {
        CmdSetName(name);
    }

    public void ColorChanged(Color color)
    {
        CmdSetColor(color);
    }

    public void NameChangedOnServer(string name)
    {
        _lobbyPlayerGUI.SetPlayerName(name);
    }

    public void ColorChangedOnServer(Color color)
    {
        _lobbyPlayerGUI.SetPlayerImageColor(color);
    }
    
    [Command]
    public void CmdSetColor(Color color)
    {
        _playerColor = color;
    }

    [Command]
    public void CmdSetName(string name)
    {
        _playerName = name;
    }

    public override void OnClientReady(bool readyState)
    {
        _lobbyPlayerGUI.UpdatePlayerReady(readyState);
        base.OnClientReady(readyState);        
    }

    public bool TooglePlayerReady()
    {
        Debug.Log(this.connectionToServer);
        readyToBegin = !readyToBegin;        
        if (readyToBegin)
        {
            SendReadyToBeginMessage();        }
        else
        {
            SendNotReadyToBeginMessage();
        }
        
        return readyToBegin;
    }

}
