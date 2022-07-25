using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject bg, panel, phone, deliveringOrderParent, deliveringOrderPrefab, availableOrderParent, availableOrderPrefab, reviewMessage;
    [SerializeField]
    GameObject[] apps, appIcons;
    [SerializeField]
    TextMeshProUGUI timeText, cashText;
    
    [SerializeField]
    RectTransform panelRT;
    [SerializeField]
    Sprite[] stars;
    [SerializeField]
    OrderManager orderManager;
    public AnimationCurve vibrateCurve;
    public ProgressBar progressBar;
    public bool phoneOpen = false;
    bool onTransition = false;
    int currentApp = 0, pastApp = 0;
    public string[][] reviewComment = {
        //속도도 빠르고 안정성도 높은 경우
        new string[] {"완벽하네요!","최고에요:]",},
        //속도가 빠른 경우
        new string[] {"굉장히 빠르네요!"},
        //안정성이 높은 경우
        new string[] {"음식 상태가 좋아요"},
        //보통인 경우
        new string[] {"뭐 나쁘지 않네요"},
        //속도가 느린 경우
        new string[] {"배달중에 사고라도 났나요. 왜이렇게 오래걸리죠!"},
        //안정성이 낮은 경우
        new string[] {"포장이 다 뜯겼네요! 오다 태풍이라도 만났나봐요"},
        //둘다 안좋은 경우
        new string[] {"정말 속도와 상태가 최악입니다! 다시는 안시킬게요"}
    };
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
        if(Input.GetKeyDown(KeyCode.Space)) {
            progressBar.ShowBar();
        }
        if(Input.GetKeyDown(KeyCode.A)) {
            progressBar.HideBar();
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            progressBar.FullBar();
        }
        if(Input.GetButtonDown("Menu") && !onTransition) {
            onTransition = true;
            if(phoneOpen) {
                Resume();
                CloseUI();
                CloseApp(currentApp);
                progressBar.Show();
            }
            else {
                Pause();
                OpenUI();
                progressBar.Hide();
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
    //리뷰 메세지 띄우기
    public void ReviewMessage(int speedStar, int safteyStar, int time, int reward) {
        GameObject message = Instantiate(reviewMessage, new Vector3(900f, -150f, 0f), Quaternion.identity);
        message.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
        message.transform.SetParent(bg.transform);

        string reviewText = "";
        if (speedStar <= 1 && safteyStar <= 1)
            reviewText = reviewComment[6][Random.Range(0,reviewComment[6].Length)];
        else if (safteyStar <= 1)
            reviewText = reviewComment[5][Random.Range(0,reviewComment[5].Length)];
        else if (speedStar <= 1)
            reviewText = reviewComment[4][Random.Range(0,reviewComment[4].Length)];
        else if (speedStar == 5 && safteyStar == 5)
            reviewText = reviewComment[0][Random.Range(0,reviewComment[0].Length)];
        else if (speedStar == 5)
            reviewText = reviewComment[1][Random.Range(0,reviewComment[1].Length)];
        else if (safteyStar == 5)
            reviewText = reviewComment[2][Random.Range(0,reviewComment[2].Length)];
        else
            reviewText = reviewComment[3][Random.Range(0,reviewComment[3].Length)];
        
        string r = CashToString(reward);
        string t = $"배달 완료!\n시간: {time}초  보상: {r}원\n리뷰:\n{reviewText}";
        message.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = t;

        int starCount = Mathf.RoundToInt((speedStar + safteyStar) / 2);
        message.transform.GetChild(1).GetComponent<Image>().sprite = stars[starCount];

        ViberatePhone();
        LeanTween.moveLocal(message, new Vector3(0f, -370f, 0f), 0.3f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);
        LeanTween.scale(message, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);
        LeanTween.moveLocal(message, new Vector3(0f, -100f, 0f), 0.5f).setDelay(4f).setEase(LeanTweenType.easeOutCubic).setIgnoreTimeScale(true);
        LeanTween.alpha(message.GetComponent<RectTransform>(), 0f, 0.5f).setDelay(4f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(message.transform.GetChild(0).GetComponent<CanvasGroup>(), 0f, 0.5f).setDelay(4f).setIgnoreTimeScale(true);
        Destroy(message, 4.5f);
    }
    void ViberatePhone() {
        LeanTween.moveX(phone, 403f, 0.3f).setEase(vibrateCurve);
        LeanTween.moveX(phone, 400f, 0.1f).setDelay(0.3f);
    }
    //배달앱 열었을때 텍스트 처리
    void DeliveryApp() {
        foreach (Transform child in deliveringOrderParent.transform) {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in availableOrderParent.transform) {
            GameObject.Destroy(child.gameObject);
        }
        List<string[]> orders = orderManager.PassOrders();
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
        List<string[]> aOrders = orderManager.PassOrders_A();
        foreach (string[] order in aOrders) {
            GameObject item = Instantiate(availableOrderPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(availableOrderParent.transform);
            order[2] += "초";
            order[3] = CashToString(int.Parse(order[3])) + "원";
            for (int i=0; i<4; i++) {
                TextMeshProUGUI text = item.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                text.text = order[i];
            }
            item.transform.GetChild(4).name = order[4];
        }
    }
    GameObject tmpObj;
    //오더 확인했을 때 풀에서 제거
    public void AcceptOrder(GameObject obj) {
        int index = int.Parse(obj.transform.GetChild(4).name);
        orderManager.MakeOrder(index);
        LeanTween.scale(obj,new Vector2(1f,0.05f),0.3f).setIgnoreTimeScale(true);
        tmpObj = obj;
        LeanTween.alphaCanvas(obj.GetComponent<CanvasGroup>(),0,0.3f).setIgnoreTimeScale(true).setOnComplete(DestroyObj);
    }
    void DestroyObj() {
        Destroy(tmpObj);
        tmpObj = null;
    }
    public void AddOrderToUI(string[] order) {
        GameObject item = Instantiate(deliveringOrderPrefab, Vector3.zero, Quaternion.identity);
        item.transform.SetParent(deliveringOrderParent.transform);
        order[2] += "초 남음";
        order[3] = CashToString(int.Parse(order[3])) + "원";
        for (int i=0; i<4; i++) {
            TextMeshProUGUI text = item.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            text.text = order[i];
        }
        item.transform.localScale = new Vector2(1f,0);
        item.GetComponent<CanvasGroup>().alpha = 0;
        LeanTween.scale(item,new Vector2(1f,1f),0.3f).setIgnoreTimeScale(true);
        LeanTween.alphaCanvas(item.GetComponent<CanvasGroup>(),1f,0.3f).setIgnoreTimeScale(true);
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
    public void CashAnim() {
        cashText.color = Color.green;
        LeanTween.scale(cashText.rectTransform,new Vector2(2f,2f), 1f).setEase(LeanTweenType.easeOutCirc).setIgnoreTimeScale(true);
        LeanTween.scale(cashText.rectTransform,new Vector2(1f,1f), 1f).setEase(LeanTweenType.easeInBack).setDelay(1f).setOnComplete(FinishCashAnim).setIgnoreTimeScale(true);
    }
    void FinishCashAnim() {
        cashText.color = Color.white;
    }
    //한국식 표현으로 전환
    string CashToString(int cash) {
        string st = "0";
        if(cash >= 100000000) {
            st = (cash/100000000).ToString() + "억";
            if((cash%100000000)/10000 > 0)
                st += ((cash%100000000)/10000).ToString() + "만";
        }
        else if(cash >= 10000) {
            st = (cash/10000).ToString() + "만";
            if((cash%10000) > 0)
                st +=(cash%10000).ToString();
        }
        else
            st = cash.ToString();
        return st;
    }
}
