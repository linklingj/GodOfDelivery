using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public UIController uIController;
    public PickupPoint[] pickupPoints;
    public DeliveryPoint[] deliveryPoints;
    public List<Order> orders = new List<Order>();
    public List<GameObject> arrows = new List<GameObject>();
    public List<AvailableOrder> orderPool = new List<AvailableOrder>();
    public float timer;
    public GameObject arrowPrefab;
    public Transform canvas;
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
        //테스트 주문 생성
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 60f, 1000));
        poolSize = orderPool.Count;
        pPNum = Random.Range(0, pickupPoints.Length);
        dPNum = Random.Range(0, deliveryPoints.Length);
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 30f, 5000));
        poolSize = orderPool.Count;
        pPNum = Random.Range(0, pickupPoints.Length);
        dPNum = Random.Range(0, deliveryPoints.Length);
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 60f, 20000));
        poolSize = orderPool.Count;
        pPNum = Random.Range(0, pickupPoints.Length);
        dPNum = Random.Range(0, deliveryPoints.Length);
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 20f, 90000));
        poolSize = orderPool.Count;
        pPNum = Random.Range(0, pickupPoints.Length);
        dPNum = Random.Range(0, deliveryPoints.Length);
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 110f, 300000));
        poolSize = orderPool.Count;
        pPNum = Random.Range(0, pickupPoints.Length);
        dPNum = Random.Range(0, deliveryPoints.Length);
        orderPool.Add(new AvailableOrder(poolSize, pickupPoints[pPNum], deliveryPoints[dPNum], 80f, 200000000));
    }
    public void MakeOrder(int index) {
        Debug.Log("order in");
        orders.Add(orderPool[index].AddToOrderList(timer));

        GameObject arrow = Instantiate(arrowPrefab, new Vector3(-100f,-100f,0), Quaternion.identity);
        arrow.GetComponent<Arrow>().followPoint = orderPool[index].pickupPoint.transform;
        arrow.transform.SetParent(canvas);
        arrows.Add(arrow);

        orderPool[index].state = 1;
    }
    public void Pickup(int index) {
        orders[index].state = 1;
        orders[index].pickupPoint.pointOff();

        arrows[index].GetComponent<Arrow>().followPoint = orders[index].deliveryPoint.transform;
        arrows[index].GetComponent<Arrow>().ChangeColor();
    }
    public void FinishOrder(int index) {
        Order order = orders[index];
        order.deliveryPoint.pointOff();

        int clearTime = Mathf.FloorToInt(timer - order.startTime);
        int speedStar = ReviewSaftey();
        int safteyStar = ReviewSpeed(clearTime, order.targetTime);
        int reward = orders[index].maxReward;

        GameManager.Instance.Cash += reward;
        uIController.ReviewMessage(speedStar,safteyStar,clearTime,reward);
        orders.RemoveAt(index);
        Destroy(arrows[index]);
        arrows.RemoveAt(index);

        Debug.Log("speed:"+speedStar);
        Debug.Log("saftey" +safteyStar);
    }
    //안정성 평가 (0~5의 정수 리턴)
    int ReviewSaftey() {
        //수정 필요
        int point = Random.Range(0,6);
        return point;
    }
    //속도 평가 (0~5의 정수 리턴)
    int ReviewSpeed(int clearTime, float targetTime) {
        int point = 0;
        //수정 필요
        if (clearTime >= targetTime) {
            point = 0;
        } else {
            point = Random.Range(0,6);
        }
        return point;
    }
    public List<string[]> PassOrders() {
        if (orderPool.Count == 0)
            return null;
        //string배열의 리스트를 리턴, string 배열은 {출발지,도착지,남은시간,보상}
        List<string[]> res = new List<string[]>();
        foreach(Order order in orders) {
            int timeLeft = Mathf.FloorToInt(order.targetTime - timer + order.startTime);
            res.Add(new string[] {order.pickupPoint.transform.name,order.deliveryPoint.transform.name,timeLeft.ToString(),order.maxReward.ToString()});
        }
        return res;
    }
    public List<string[]> PassOrders_A() {
        if (orderPool.Count == 0)
            return null;
        //string배열의 리스트를 리턴, string 배열은 {출발지,도착지,총 시간,보상,인덱스}
        List<string[]> res = new List<string[]>();
        foreach(AvailableOrder order in orderPool) {
            if(order.state == 1)
                continue;
            res.Add(new string[] {order.pickupPoint.transform.name,order.deliveryPoint.transform.name,order.targetTime.ToString(),order.maxReward.ToString(),order.index.ToString()});
        }
        return res;
    }
}
