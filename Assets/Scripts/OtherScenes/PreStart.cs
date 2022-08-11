using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//나중에 없애기
using UnityEngine.SceneManagement;

public class PreStart : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI dayText;
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        dayText.text = "DAY " + gameManager.Day.ToString();
    }
    public void Play() {
        gameManager.StartGamePlay();
    }
    public void ToTitle() {
        gameManager.UpdateGameState(GameState.Title);
    }
    public void Building() {
        SceneManager.LoadScene("Building");
    }
    public void Ending() {
        SceneManager.LoadScene("Ending");
    }
}
