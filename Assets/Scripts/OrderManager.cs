using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public PickupPoint[] pickupPoints;
    public DeliveryPoint[] deliveryPoints;
    public List<Order> orders = new List<Order>();
    public List<AvailableOrder> orderPool = new List<AvailableOrder>();
    public float timer;
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
    public class AvailableOrder {
        public int index;
        //0 받기 전, 1 받은 상태
        public int state;
        public PickupPoint pickupPoint;
        public DeliveryPoint deliveryPoint;
        public float targetTime;
        public int maxReward;
        public AvailableOrder(int i, PickupPoint pp, DeliveryPoint dp, float tT, int mR) {
            index = i;
            state = 0;
            pickupPoint = pp;
            deliveryPoint = dp;
            targetTime = tT;
            maxReward = mR;
        }
        public Order AddToOrderList(float startTime) {
            return new Order(0, pickupPoint, deliveryPoint, targetTime, maxReward, startTime);
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
    public void AddOrderPool() {
        //수정 필요
        int poolSize = orderPool.Count;
        int pPNum = Random.Range(0, pickupPoints.Length);
        int dPNum = Random.Range(0, deliveryPoints.Length);
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 60f, 1000));
        pPNum = Random.Range(0, pickupPoints.Length);
        dPNum = Random.Range(0, deliveryPoints.Length);
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 60f, 1000));
        pPNum = Random.Range(0, pickupPoints.Length);
        dPNum = Random.Range(0, deliveryPoints.Length);
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 60f, 1000));
    }
    public void MakeOrder(int index) {
        Debug.Log("order in");
        orders.Add(orderPool[index].AddToOrderList(timer));
        orderPool[index].state = 1;
    }
    public void Pickup(int index) {
        Debug.Log("pickup");
        orders[index].state = 1;
        orders[index].pickupPoint.pointOff();
    }
    int test = 1;
    public void FinishOrder(int index) {
        Order order = orders[index];
        order.deliveryPoint.pointOff();
        Debug.Log("order completed");
        Debug.Log("Saftey : " + ReviewSaftey().ToString() + " stars");
        Debug.Log("Speed : " + ReviewSpeed(order).ToString() + " stars");
        GameManager.Instance.Cash += orders[index].maxReward;
        orders.RemoveAt(index);
        MakeOrder(test++);
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
