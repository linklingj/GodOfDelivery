using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    GameManager gameManager;
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartGame() {
        gameManager.UpdateGameState(GameState.Story);
    }
}
