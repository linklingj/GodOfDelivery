using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    public GameObject[] buildings, locks, cards, endings;
    public GameObject bg, cardGet;
    
    int buildState, buildingNum, currentCard;
    bool waitForClick = false;
    Color32 deactive = new Color32(77, 66, 66, 128);
    private void Start() {
        waitForClick = false;
        buildState = GameManager.Instance.buildState;
        buildingNum = GameManager.Instance.buildingNum;
        bg.SetActive(false);
        for (int i = 0; i < 4; i++) {
            if (i >= buildingNum) {
                buildings[i].GetComponent<Image>().color = deactive;
                locks[i].gameObject.SetActive(true);
                if (i == buildingNum && buildState == 6) {
                    locks[i].GetComponent<Image>().color = Color.white;
                } else {
                    locks[i].GetComponent<Image>().color = deactive;
                }
            } else {
                locks[i].gameObject.SetActive(false);
                buildings[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
    public void Unlock(int n) {
        if (buildingNum == n && buildState == 6) {
            GameManager.Instance.buildDay = 1;
            buildState = 0;
            GameManager.Instance.buildState = 0;
            buildingNum += 1;
            GameManager.Instance.buildingNum += 1;
            locks[n].gameObject.SetActive(false);
            buildings[n].GetComponent<Image>().color = Color.white;
        }
    }
    public void ShowEnding(int n) {
        if (buildingNum > n) {
            currentCard = n;
            bg.SetActive(true);
            bg.GetComponent<CanvasGroup>().alpha = 0;
            LeanTween.alphaCanvas(bg.GetComponent<CanvasGroup>(), 1f, 1f);

            endings[n].SetActive(true);
            endings[n].GetComponent<CanvasGroup>().alpha = 0;
            LeanTween.alphaCanvas(endings[n].GetComponent<CanvasGroup>(), 1f, 1f).setOnComplete(() => waitForClick = true);
        }
    }
    public void ShowCards() {

    }
    void GetCard() {
        LeanTween.alphaCanvas(endings[currentCard].GetComponent<CanvasGroup>(), 0f, 1f);
        cards[currentCard].SetActive(true);
        RectTransform rT = cards[currentCard].GetComponent<RectTransform>();
        rT.anchoredPosition = new Vector2(0, -830f);
        LeanTween.moveLocalY(cards[currentCard], -10f, 0.3f).setEase(LeanTweenType.easeOutBack).setDelay(1f).setOnComplete(() => cardGet.SetActive(true));
        cardGet.transform.localScale = new Vector3 (0.01f, 0.01f, 1f);
        LeanTween.scale(cardGet.GetComponent<RectTransform>(), new Vector2(1f,1f), 0.3f).setDelay(1.5f).setEase(LeanTweenType.easeInOutBack);

        LeanTween.moveLocalY(cards[currentCard], -800f, 0.4f).setEase(LeanTweenType.easeOutCirc).setDelay(6f).setOnComplete(() => cards[currentCard].SetActive(false));
        LeanTween.alphaCanvas(bg.GetComponent<CanvasGroup>(), 0, 1f).setDelay(6.5f).setOnComplete(() => bg.SetActive(false));
        LeanTween.scale(cardGet.GetComponent<RectTransform>(), new Vector2(0.01f,0.011f), 0.3f).setDelay(6.5f).setEase(LeanTweenType.easeOutCirc).setOnComplete(() => cardGet.SetActive(false));
    }
    public void Exit() {
        SceneManager.LoadScene("PreStart");
    }
    private void Update() {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && waitForClick == true) {
            waitForClick = false;
            GetCard();
        }
    }
}
