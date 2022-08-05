using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sotry : MonoBehaviour
{
    GameManager gameManager;
    public int num_before = 0;
    public int num_now = 1;
    public static int Num;

    public List<GameObject> images;
    public Button buttonR, buttnL, skip, show, cont;
    public Text titleText;
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void ShowStory()
    {
        images[0].SetActive(false);
        images[1].SetActive(true);

        skip.gameObject.SetActive(false);
        show.gameObject.SetActive(false);
        buttonR.interactable = true;
        titleText.gameObject.SetActive(false);
    }
    //n이 1일때 오른쪽 버튼, n이 2일때 왼쪽버튼
    public void Click(int n)
    {
        if (n == 1) {
            num_now++;
        } else if (n == 2) {
            num_now--;
        }

        images[num_before].SetActive(false);
        images[num_now].SetActive(true);

        if (num_now == 1) {
            buttnL.interactable = false;
        } else {
            buttnL.interactable = true;
        }
        if (num_now == 6) {
            cont.gameObject.SetActive(true);
            buttonR.interactable = false;
        } else {
            cont.gameObject.SetActive(false);
            buttonR.interactable = true;
        }

        num_before = num_now;
    }
    public void Continue() {
        gameManager.StartTutorial();
    }
}
