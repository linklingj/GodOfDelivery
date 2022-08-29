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
        int dayLeft = gameManager.buildDayLimits[gameManager.buildingNum] - gameManager.Day + 1;
        if (dayLeft == 1)
            dayText.text = "마지막날";
        else
            dayText.text = "D - " + dayLeft.ToString();
    }
    public void Play() {
        gameManager.StartGamePlay();
    }
    public void ToTitle() {
        gameManager.UpdateGameState(GameState.Title);
    }
    public void Building() {
        if (gameManager.buildingNum == 0)
            SceneManager.LoadScene("Building1");
        if (gameManager.buildingNum == 1)
            SceneManager.LoadScene("Building2");
        if (gameManager.buildingNum == 2)
            SceneManager.LoadScene("Building3");
        if (gameManager.buildingNum == 3)
            SceneManager.LoadScene("Building4");
    }
    public void Ending() {
        SceneManager.LoadScene("Ending");
    }

    public void gotowhere()
    {
        SceneManager.LoadScene("Howtoplay");
    }
}
