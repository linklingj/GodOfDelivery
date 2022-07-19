using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public OrderManager orderManager;
    public bool belowPlayer = false;
    public int orderIndex = -1;
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
        if (col.CompareTag("Player")) {
            belowPlayer = true;
            if (orderManager.orders[orderIndex].state == 1)
            orderManager.FinishOrder(orderIndex);
        }
    }
    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            belowPlayer = false;
        }
    }
}
