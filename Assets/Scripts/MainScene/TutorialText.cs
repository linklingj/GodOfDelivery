using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public GameObject textBox;
    public GameObject bg;
    public OrderManager orderManager;
    int idx = 0;
    bool clickAble = true;
    public string[] tutorialText = { };
    public Vector2[] textSize = { };
    private void Start() {
        if (GameManager.Instance.Day == 0) {
            idx = 0;
            MakeText();
        } else {
            Destroy(gameObject);
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && clickAble) {
            if (idx == tutorialText.Length-1) {
                Time.timeScale = 1f;
                GameManager.Instance.EndTutorial();
            } else {
                idx++;
                NewText();
                if (idx == 3 || idx == 5)
                    StartCoroutine("WaitTime");
                if (idx == 7)
                    StartCoroutine("WaitTab");
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
            GameManager.Instance.EndTutorial();
    }
    GameObject message;
    void NewText() {
        clickAble = false;
        LeanTween.move(message, new Vector3(700f, 300f, 0f), 0.5f).setEase(LeanTweenType.easeOutCubic).setIgnoreTimeScale(true);
        LeanTween.alpha(message.GetComponent<RectTransform>(), 0f, 0.5f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(message.GetComponent<CanvasGroup>(), 0f, 0.5f).setOnComplete(MakeText).setIgnoreTimeScale(true);
        Destroy(message, 0.55f);
    }
    void MakeText() {
        message = Instantiate(textBox, new Vector3(600f, -350f, 0f), Quaternion.identity);
        message.transform.SetParent(bg.transform);
        message.GetComponent<RectTransform>().sizeDelta = textSize[idx];
        message.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tutorialText[idx];
        LeanTween.move(message, new Vector3(700f, 60f, 0f), 0.3f).setOnComplete(Complete).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);
    }
    void Complete() {
        if (idx != 3 && idx != 5 && idx != 7 && idx != 8 && idx != 9 && idx != 10)
            clickAble = true;
    }
    IEnumerator WaitTime() {
        yield return new WaitForSeconds(5f);
        idx++;
        NewText();
    }
    IEnumerator WaitTab() {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        idx++;
        NewText();
        StartCoroutine("WaitClick");
    }
    IEnumerator WaitClick() {
        yield return new WaitUntil(() => orderManager.orders.Count == 1);
        idx++;
        NewText();
        StartCoroutine("WaitOrder1");
    }
    IEnumerator WaitOrder1() {
        yield return new WaitUntil(() => orderManager.orders[0].state == 1);
        idx++;
        NewText();
        StartCoroutine("WaitOrder2");
    }
    IEnumerator WaitOrder2() {
        yield return new WaitUntil(() => orderManager.orders.Count == 0);
        idx++;
        StartCoroutine("WaitTime");
    }
}
