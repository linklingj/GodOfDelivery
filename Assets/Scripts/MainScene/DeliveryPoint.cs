using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public OrderManager orderManager;
    public ProgressBar progressBar;
    public int orderIndex = -1;
    bool belowPlayer = false;
    float enterTime;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void pointOn() {
        gameObject.SetActive(true);
    }
    public void pointOff() {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && orderManager.orders[orderIndex].state == 1) {
            enterTime = orderManager.timer;
            belowPlayer = true;
            progressBar.ShowBar();
        }
    }
    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player") && belowPlayer) {
            belowPlayer = false;
            progressBar.HideBar();
        }
    }
    private void Update() {
        if (!belowPlayer)
            return;
        float time = orderManager.timer - enterTime;
        if(time >= 2f) {
            belowPlayer = false;
            orderManager.FinishOrder(orderIndex);
            progressBar.FullBar();
        }
        progressBar.SetValue(time/2);
    }
}
