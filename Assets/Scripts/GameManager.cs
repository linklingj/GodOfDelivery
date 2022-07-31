using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Title,
    Story,
    Menu,
    Play,
    Clear,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public OrderManager orderManager;
    public GameState State;
    public int Cash;
    public int TotalCash;
    public int Day;
    public int DeliveryCount;
    public int[] targetCashPerDay;
    public int[] targetTimePerDay;
    public static event Action<GameState> OnGameStateChanged;
    public int maxOrderPool = 3;
    float timer;
    int nextOrderTime;
    int orderAddingInterval;
    bool loading;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        TotalCash = 0;
        Day = 1;
        UpdateGameState(GameState.Title);
    }
    public void Title_GameStart() {
        UpdateGameState(GameState.Story);
    }
    public void EndStory() {
        UpdateGameState(GameState.Menu);
    }
    public void ToTitle() {
        UpdateGameState(GameState.Title);
    }
    public void StartGamePlay() {
        UpdateGameState(GameState.Play);
    }
    public void UpdateGameState(GameState newState) {
        State = newState;
        switch (newState) {
            case GameState.Title:
                SceneManager.LoadScene("First");
                break;
            case GameState.Story:
                SceneManager.LoadScene("Story");
                break;
            case GameState.Menu:
                SceneManager.LoadScene("PreStart");
                break;
            case GameState.Play:
                Cash = 0;
                DeliveryCount = 0;
                if (Day == 1) {
                    maxOrderPool = 3;
                    orderAddingInterval = 20; 
                } else if (Day == 2) {
                    maxOrderPool = 4;
                    orderAddingInterval = 18; 
                } else if (Day == 3) {
                    maxOrderPool = 4;
                    orderAddingInterval = 16; 
                } else if (Day == 4) {
                    maxOrderPool = 5;
                    orderAddingInterval = 15; 
                } else if (Day == 5) {
                    maxOrderPool = 5;
                    orderAddingInterval = 12; 
                }
                nextOrderTime = orderAddingInterval;
                OnGameStateChanged = null;
                StartCoroutine(Loading());
                break;
            case GameState.Clear:
                TotalCash += Cash;
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
    IEnumerator Loading() {
        loading = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");
        while(!asyncLoad.isDone) {
            yield return null;
        }
        orderManager = FindObjectOfType<OrderManager>();
        loading = false;
    }
    private void Update() {
        if (State == GameState.Play && !loading) {
            timer = orderManager.timer;
            if (timer > nextOrderTime) {
                if (orderManager.orderPool.FindAll(x => x.state == 0).Count < maxOrderPool)
                    orderManager.AddOrderPool();
                nextOrderTime += orderAddingInterval;
            }
            if (Cash >= targetCashPerDay[Day-1]) {
                ClearDay();
            }
            if (orderManager.timer >= targetTimePerDay[Day-1] * 60) {
                GameOver();
            }
        }
    }
    void ClearDay() {
        UpdateGameState(GameState.Clear);
    }
    void GameOver() {
        UpdateGameState(GameState.GameOver);
    }
    public void NewDay() {
        Day += 1;
        UpdateGameState(GameState.Menu);
    }
    public void ResetDay() {
        //저장해둔거 로딩
        UpdateGameState(GameState.Menu);
    }
}

