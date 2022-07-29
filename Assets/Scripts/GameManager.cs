using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
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

    
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        Cash = 0;
        TotalCash = 0;
        Day = 1;
        DeliveryCount = 0;
        UpdateGameState(GameState.Play);

        //test
        orderManager.AddOrderPool();
        //StartCoroutine("Test");
        

    }
    public void UpdateGameState(GameState newState) {
        State = newState;
        switch (newState) {
            case GameState.Menu:
                break;
            case GameState.Play:
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
    private void Update() {
        if (Cash >= targetCashPerDay[Day-1] && State == GameState.Play) {
            ClearDay();
            Debug.Log("Success");
        }
        if (orderManager.timer >= targetTimePerDay[Day-1] * 60 && State == GameState.Play) {
            GameOver();
            Debug.Log("game over!");
        }
    }
    void ClearDay() {
        UpdateGameState(GameState.Clear);
    }
    void GameOver() {
        UpdateGameState(GameState.GameOver);
    }
    // IEnumerator Test() {
    //     yield return new WaitForSeconds(1f);
    //     orderManager.MakeOrder(0);
    // }
}

