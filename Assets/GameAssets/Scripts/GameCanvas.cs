using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {

    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject losePanel;

    [SerializeField] Text victoryMessage;
    [SerializeField] Text loseMessage;

    public void ActivateVictoryPanel()
    {
        victoryPanel.gameObject.SetActive(true);
    }

    public void ActivateLosePanel()
    {
        losePanel.gameObject.SetActive(true);
    }

    public void SetVictoryMessage(string text)
    {
        victoryMessage.text = text;
    }

    public void SetLoseMessage(string text)
    {
        loseMessage.text = text;
    }

}
