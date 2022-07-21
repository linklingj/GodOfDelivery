using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject panel, phone, deliveringOrderParent, deliveringOrderPrefab;
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
    //핸드폰 열기
    public void OpenUI() {
        LeanTween.alpha(panelRT, 0.6f, 0.3f).setIgnoreTimeScale(true);
        LeanTween.moveLocal(phone, new Vector3(-490f, -54.275f, 0), 0.4f).setEase(LeanTweenType.easeOutBack).setOnComplete(transitionFinish).setIgnoreTimeScale(true);
        LeanTween.rotateLocal(phone, new Vector3(0, 0, 3f), 0.4f).setEase(LeanTweenType.easeOutBack).setOnComplete(OpenFirstApp).setIgnoreTimeScale(true);
    }
    //핸드폰 닫기
    public void CloseUI() {
        LeanTween.alpha(panelRT, 0, 0.3f);
        LeanTween.moveLocal(phone, new Vector3(-544f, -952.7f, 0), 0.3f).setEase(LeanTweenType.easeOutQuint).setOnComplete(transitionFinish);
        LeanTween.rotateLocal(phone, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutElastic);
    }
    //앱 클릭
    public void AppIconClicked(int id) {
        if (id != currentApp && !onTransition) {
            CloseApp(currentApp);
            currentApp = id;
            OpenApp(currentApp);
        }
    }
    //핸드폰 처음 열었을 때 앱 열기
    public void OpenFirstApp() {
        currentApp = 0;
        pastApp = 0;
        OpenApp(0);
    }
    //앱 열기
    public void OpenApp(int id) {
        onTransition = true;
        GameObject app = apps[id];
        app.SetActive(true);
        if (id == 0)
            DeliveryApp();
        app.transform.position = appIcons[id].transform.position;
        app.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
        LeanTween.moveLocal(app, new Vector3(400f, 10f, 0f), 0.3f).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);
        LeanTween.scale(app, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(transitionFinish).setIgnoreTimeScale(true);
    }
    //앱 닫기
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
        apps[pastApp].transform.localScale = new Vector3(1f,1f,1f);
        pastApp = currentApp;
    }
    //게임 중지
    void Pause() {
        Time.timeScale = 0f;
    }
    //게임 재개
    void Resume() {
        Time.timeScale = 1f;
    }
    void DeliveryApp() {
        foreach (Transform child in deliveringOrderParent.transform) {
            GameObject.Destroy(child.gameObject);
        }
        List<string[]> orders = orderManager.PassOrders();
        if(orders.Count == 0)
            return;
        foreach (string[] order in orders) {
            GameObject item = Instantiate(deliveringOrderPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(deliveringOrderParent.transform);
            order[2] += "초 남음";
            order[3] = CashToString(int.Parse(order[3])) + "원";
            for (int i=0; i<4; i++) {
                TextMeshProUGUI text = item.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                text.text = order[i];
            }
        }
    }
    //시간 업데이트
    void UpdateTimerDisplay() {
        float timer = orderManager.timer;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        string currentTime = string.Format("{00:00}:{1:00}",minutes,seconds);
        timeText.text = currentTime;
    }
    //돈 업데이트
    void UpdateMoneyDisplay() {
        int cash = GameManager.Instance.Cash;
        cashText.text = CashToString(cash) + "원";
    }
    //한국식 표현으로 전환
    string CashToString(int cash) {
        string st = "0";
        if(cash >= 100000000)
            st = (cash/100000000).ToString() + "억" + ((cash%100000000)/10000).ToString() + "만";
        else if(cash >= 10000)
            st = (cash/10000).ToString() + "만" + (cash%10000).ToString();
        else
            st = cash.ToString();
        return st;
    }
}
