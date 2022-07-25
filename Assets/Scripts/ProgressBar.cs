using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public RectTransform rT;
    public Transform player;
    public void HideBar() {
        LeanTween.scale(gameObject, new Vector3(0f,0f,1f), 0.2f).setEase(LeanTweenType.easeOutQuad).setOnComplete(Deactivate);
    }
    public void ShowBar() {
        transform.localScale = new Vector3(0,0,1f);
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, new Vector3(1f,1f,1f), 0.3f).setEase(LeanTweenType.easeOutBack);
    }
    public void SetValue(float progress) {
        slider.value = progress;
    }
    public void FullBar() {
        LeanTween.scale(gameObject, new Vector3(2f,2f,1f), 0.3f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(FullBar2);
    }
    void FullBar2() {
        LeanTween.scale(gameObject, new Vector3(0.1f,0.1f,1f), 0.3f).setEase(LeanTweenType.easeInQuad);
        LeanTween.rotateAround(gameObject,Vector3.forward,360f,0.3f).setEase(LeanTweenType.easeInQuad).setOnComplete(Deactivate);
    }
    bool wasActive = false;
    void Deactivate() {
        gameObject.SetActive(false);
    }
    public void Hide() {
        if (gameObject.activeSelf)
            wasActive = true;
        else
            wasActive = false;
        gameObject.SetActive(false);
    }
    public void Show() {
        if (wasActive)
            gameObject.SetActive(true);
    }
    private void Update() {
        rT.position = RectTransformUtility.WorldToScreenPoint(Camera.main, player.position) + Vector2.up * 70f;
    }
}
