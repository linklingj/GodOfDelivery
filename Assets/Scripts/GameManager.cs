using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Menu,
    Play
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public OrderManager orderManager;
    public GameState State;
    public int Cash;
    public static event Action<GameState> OnGameStateChanged;

    
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        Cash = 0;
        UpdateGameState(GameState.Menu);

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
            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
    // IEnumerator Test() {
    //     yield return new WaitForSeconds(1f);
    //     orderManager.MakeOrder(0);
    // }
}

