using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = "OnNameChange")]
    string playerName;
    [SyncVar(hook = "OnColorChange")]
    Color playerColor;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        CmdChangeName("Raúl T.");
        CmdChangeColor(Color.cyan);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        this.name = playerName;
        this.GetComponentInChildren<Renderer>().material.color = playerColor;
    }

    void OnNameChange(string name)
    {
        this.name = name;
    }
    
    void OnColorChange(Color color)
    {
        this.GetComponentInChildren<Renderer>().material.color = color;
    }

    [Command]
    void CmdChangeName(string newName)
    {
        this.gameObject.name = newName;
        // RpcChangeName(newName);
        playerName = newName;
    }

    [Command]
    void CmdChangeColor(Color newColor)
    {
        this.GetComponentInChildren<Renderer>().material.color = newColor;
        // RpcChangeColor(newColor);
        playerColor = newColor;
    }

    [ClientRpc]
    void RpcChangeName(string newName)
    {
        this.gameObject.name = newName;
    }

    [ClientRpc]
    void RpcChangeColor(Color newColor)
    {
        this.GetComponentInChildren<Renderer>().material.color = newColor;
    }
}
