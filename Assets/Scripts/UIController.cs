using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject panel, phone;
    [SerializeField]
    RectTransform panelRT;
    public bool phoneOpen = false;
    bool onTransition = false;
    public void OpenUI() {
        LeanTween.alpha(panelRT, 0.6f, 0.3f);
        LeanTween.moveLocal(phone, new Vector3(-490f, -54.275f, 0), 0.4f).setEase(LeanTweenType.easeOutBack).setOnComplete(transitionFinish);
        LeanTween.rotateLocal(phone, new Vector3(0, 0, 3f), 0.4f).setEase(LeanTweenType.easeOutBack);
    }
    public void CloseUI() {
        LeanTween.alpha(panelRT, 0, 0.3f);
        LeanTween.moveLocal(phone, new Vector3(-544f, -952.7f, 0), 0.3f).setEase(LeanTweenType.easeOutQuint).setOnComplete(transitionFinish);
        LeanTween.rotateLocal(phone, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutElastic);
    }
    private void Update() {
        if(Input.GetButtonDown("Menu") && !onTransition) {
            onTransition = true;
            if(phoneOpen) {
                Resume();
                CloseUI();
            }
            else {
                OpenUI();
            }
            phoneOpen = !phoneOpen;
        }
    }
    void transitionFinish() {
        onTransition = false;
        if (phoneOpen) {
            Pause();
        }
    }
    void Pause() {
        Time.timeScale = 0f;
    }
    void Resume() {
        Time.timeScale = 1f;
    }
}
