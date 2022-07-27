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

    private int maxOrderPool = 3;
    float timer;
    int nextOrderTime;
    int orderAddingInterval;

    
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        TotalCash = 0;
        Day = 1;
        UpdateGameState(GameState.Play);
        for (int i = 0; i < maxOrderPool; i++) {
            orderManager.AddOrderPool();
        }
    }
    public void UpdateGameState(GameState newState) {
        State = newState;
        switch (newState) {
            case GameState.Menu:
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
        if (State == GameState.Play) {
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
}

