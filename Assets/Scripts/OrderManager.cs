using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public PickupPoint[] pickupPoints;
    public DeliveryPoint[] deliveryPoints;
    public List<Order> orders = new List<Order>();
    private float timer;
    //오더 클래스
    //index: 리스트에서의 인덱스, state: 상태(0: 픽업 전, 1: 픽업후 배달전), 픽업.배달 포인트, 목표 시간, 최대 보상, 시작시간
    public class Order {
        public int index;
        public int state;
        public PickupPoint pickupPoint;
        public DeliveryPoint deliveryPoint;
        public float targetTime;
        public int maxReward;
        public float startTime;
        //생성자
        public Order(int i, PickupPoint pp, DeliveryPoint dp, float tT, int mR, float sT) {
            index = i;
            state = 0;
            pickupPoint = pp;
            deliveryPoint = dp;
            targetTime = tT;
            maxReward = mR;
            startTime = sT;

            pickupPoint.orderIndex = i;
            deliveryPoint.orderIndex = i;
            pickupPoint.pointOn();
            deliveryPoint.pointOn();
        }
    }
    private void Start() {
        //스타트가 아니라 게임스테이트 바뀔때 타이머 리셋되도록 수정 필요
        ResetTimer();
    }
    private void Update() {
        timer += Time.deltaTime;
    }
    void ResetTimer() {
        timer = 0;
    }
    public void MakeOrder() {
        //수정필요
        orders.Add(new Order(0, pickupPoints[0], deliveryPoints[0], 60f, 1000, timer));
    }
    public void Pickup(int index) {
        Debug.Log("pickup");
        orders[index].state = 1;
        orders[index].pickupPoint.pointOff();
    }
    public void FinishOrder(int index) {
        Order order = orders[index];
        order.deliveryPoint.pointOff();
        Debug.Log("order completed");
        Debug.Log("Saftey : " + ReviewSaftey().ToString() + " starts");
        Debug.Log("Speed : " + ReviewSpeed(order).ToString() + " starts");
        orders.RemoveAt(index);
    }
    //안정성 평가 (0~5의 정수 리턴)
    int ReviewSaftey() {
        //수정 필요
        return 5;
    }
    //속도 평가 (0~5의 정수 리턴)
    int ReviewSpeed(Order order) {
        int point = 0;
        int clearTime = Mathf.FloorToInt(timer - order.startTime);
        //수정 필요
        Debug.Log("clear time: "+ clearTime.ToString());
        if (clearTime <= order.targetTime) {
            point = 5;
        } else {
            point = 1;
        }
        return point;
    }
}
