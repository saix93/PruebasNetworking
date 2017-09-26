using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyPlayerGUI : MonoBehaviour {

    [SerializeField] Image playerImage;
    [SerializeField] InputField nameText;
    [SerializeField] Text readyButtonText;

    LobbyPlayer _lobbyPlayer;

    private void Start()
    {
        nameText.interactable = _lobbyPlayer.isLocalPlayer;
    }

    public void SetLobbyPlayer(LobbyPlayer player)
    {
        _lobbyPlayer = player;
    }

    public void OnInputFieldEndEdit()
    {
        Debug.Log("Name changed to " + nameText.text);
        _lobbyPlayer.NameChanged(nameText.text);
    }

    public void ChangePlayerImageColor()
    {
        if(_lobbyPlayer.isLocalPlayer)
        { 
            Color randomColor = Random.ColorHSV();
            randomColor.a = 1;
            playerImage.color = randomColor;
            _lobbyPlayer.ColorChanged(randomColor);
        }
    }

    public void SetPlayerName(string name)
    {
        nameText.text = name;
    }

    public void SetPlayerImageColor(Color color)
    {
        playerImage.color = color;
    }

    public void TogglePlayerReady()
    {
        bool readyToBegin = _lobbyPlayer.TooglePlayerReady();
        UpdatePlayerReady(readyToBegin);
    }

    public void UpdatePlayerReady(bool ready)
    {
        if (ready)
        {
            readyButtonText.text = "CANCELAR";
        }
        else
        {
            readyButtonText.text = "LISTO";
        }
    }

}
