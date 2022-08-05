using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreStart : MonoBehaviour
{
    public GameManager gameManager;
    public Text dayText;
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        dayText.text = "Day " + gameManager.Day.ToString();
    }
    public void Play() {
        gameManager.StartGamePlay();
    }
    public void ToTitle() {
        gameManager.UpdateGameState(GameState.Title);
    }
}
