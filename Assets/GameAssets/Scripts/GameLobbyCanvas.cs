using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyCanvas : MonoBehaviour {

    [SerializeField] ScrollRect scrollPanel;
    [SerializeField] RectTransform entriesPanel;

    public void AddLobbyPlayer(LobbyPlayerGUI playerGUI)
    {
        playerGUI.transform.SetParent(entriesPanel, false);
    }
}
