using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuildingScene : MonoBehaviour
{

    public List<GameObject> images;
    public List<GameObject> buildings;
    public List<Text> texts;
    public List<Image> btns;
    public Text money;

    public void gotowhere() {
        SceneManager.LoadScene("PreStart");
    }

    //초기 세팅
    private void Start() {
        money.GetComponent<Text>().text = CashToString(GameManager.Instance.TotalCash) + "원";
        int[] bP = (GameManager.Instance.buildingNum == 0)? GameManager.Instance.buildPrice : GameManager.Instance.buildPrice2;
        int bs = GameManager.Instance.buildState;
        for (int i = 0; i < 6; i++) {
            texts[i].text = CashToString(bP[i]) + "원";
            if (bs > i) {
                images[i].SetActive(true);
                if (i == 0 || i == bs-1)
                    buildings[i].SetActive(true);
                btns[i].gameObject.SetActive(false);
                texts[i].GetComponent<Text>().text = "";
            }
            if (bs == i) {
                btns[i].color = Color.white;
            } else {
                btns[i].color = new Color32(106, 90, 90, 255);
            }
        }
    }
    //버튼 눌렀을 때
    public void Build(int n) {
        int p = (GameManager.Instance.buildingNum == 0)? GameManager.Instance.buildPrice[n] : GameManager.Instance.buildPrice2[n];
        if (GameManager.Instance.TotalCash < p || GameManager.Instance.buildState != n) {
            return;
        }
        GameManager.Instance.TotalCash -= p;
        images[n].SetActive(true);
        if (n > 1)
            buildings[n-1].SetActive(false);
        buildings[n].SetActive(true);
        btns[n].gameObject.SetActive(false);
        money.GetComponent<Text>().text = CashToString(GameManager.Instance.TotalCash) + "원";
        texts[n].GetComponent<Text>().text = "";
        if (n <= 4)
            btns[n+1].color = Color.white;
        GameManager.Instance.buildState++;
    }
    //돈을 한국식 표현으로 전환하는 함수
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
    //테스트 용 나중에 지울 것
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            GameManager.Instance.TotalCash = 1500000000;
        }
    }
}
