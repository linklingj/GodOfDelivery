using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject panel, phone;
    [SerializeField]
    GameObject[] apps, appIcons;
    [SerializeField]
    TextMeshProUGUI timeText, cashText;
    
    [SerializeField]
    RectTransform panelRT;
    [SerializeField]
    OrderManager orderManager;
    public bool phoneOpen = false;
    bool onTransition = false;
    int currentApp = 0, pastApp = 0;
    public void OpenUI() {
        LeanTween.alpha(panelRT, 0.6f, 0.3f).setIgnoreTimeScale(true);
        LeanTween.moveLocal(phone, new Vector3(-490f, -54.275f, 0), 0.4f).setEase(LeanTweenType.easeOutBack).setOnComplete(transitionFinish).setIgnoreTimeScale(true);
        LeanTween.rotateLocal(phone, new Vector3(0, 0, 3f), 0.4f).setEase(LeanTweenType.easeOutBack).setOnComplete(OpenFirstApp).setIgnoreTimeScale(true);
    }
    public void CloseUI() {
        LeanTween.alpha(panelRT, 0, 0.3f);
        LeanTween.moveLocal(phone, new Vector3(-544f, -952.7f, 0), 0.3f).setEase(LeanTweenType.easeOutQuint).setOnComplete(transitionFinish);
        LeanTween.rotateLocal(phone, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutElastic);
    }
    public void AppIconClicked(int id) {
        if (id != currentApp) {
            CloseApp(currentApp);
            currentApp = id;
            OpenApp(currentApp);
        }
    }
    public void OpenFirstApp() {
        currentApp = 0;
        pastApp = 0;
        OpenApp(0);
    }
    public void OpenApp(int id) {
        onTransition = true;
        GameObject app = apps[id];
        app.SetActive(true);
        app.transform.position = appIcons[id].transform.position;
        app.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
        LeanTween.moveLocal(app, new Vector3(400f, 10f, 0f), 0.3f).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);
        LeanTween.scale(app, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(transitionFinish).setIgnoreTimeScale(true);
    }
    public void CloseApp(int id) {
        onTransition = true;
        GameObject app = apps[id];
        LeanTween.moveLocal(app, new Vector3(400f, -1000f, 0f), 0.3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(deactivateApp).setIgnoreTimeScale(true);
        LeanTween.scaleY(app, 0.05f, 0.3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(transitionFinish).setIgnoreTimeScale(true);
    }
    private void Update() {
        if(Input.GetButtonDown("Menu") && !onTransition) {
            onTransition = true;
            if(phoneOpen) {
                Resume();
                CloseUI();
                CloseApp(currentApp);
            }
            else {
                Pause();
                OpenUI();
            }
            phoneOpen = !phoneOpen;
        }
        UpdateTimerDisplay();
        UpdateMoneyDisplay();
    }
    void transitionFinish() {
        onTransition = false;
    }
    void deactivateApp() {
        apps[pastApp].SetActive(false);
        pastApp = currentApp;
    }
    void Pause() {
        Time.timeScale = 0f;
    }
    void Resume() {
        Time.timeScale = 1f;
    }
    void UpdateTimerDisplay() {
        float timer = orderManager.timer;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        string currentTime = string.Format("{00:00}:{1:00}",minutes,seconds);
        timeText.text = currentTime;
    }
    void UpdateMoneyDisplay() {
        int cash = GameManager.Instance.Cash;
        cashText.text = cash.ToString() + "원";
    }
}
