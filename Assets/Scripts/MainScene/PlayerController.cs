using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float reverseSpeed;
    public float driftFactor;
    public float driveTurnSmoothTime;
    public float roadDrag;
    public float dragTime;
    public float breakForce;
    public float velMag;

    float accelerationInput;
    float deccelerationInput;
    float steeringInput;
    float turnSmoothVelocity;
    Vector2 mousePos;
    Vector2 lookDir;
    bool stopControl = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    displayObject disp;
    MapManager mapManager;
    UIController uIController;
    OrderManager orderManager;
    public Camera cam;
    public TrailRenderer[] tireMarks;
    public GameObject hitEffect;
    public Sprite Shoes, Motorcycle, Smallcar, Truck, Policecar, Sportscar, Tank, Airplane, Dinosaur;
    public int[] speedValues;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        disp = GetComponent<displayObject>();
        mapManager = FindObjectOfType<MapManager>();
        uIController = FindObjectOfType<UIController>();
        orderManager = FindObjectOfType<OrderManager>();
    }
    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        transform.position = new Vector3 (0.5f, 4f, 0);
        transform.rotation = Quaternion.Euler(0,0,-90f);
    }

    void Update() {
        if (GameManager.Instance.State == GameState.Play) {
            //입력 제어
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            accelerationInput = Input.GetAxisRaw("accelerate");
            deccelerationInput = Input.GetAxisRaw("deccelerate");
            //회전 애니메이션 설정
            disp.rotation = new Vector3(0, 0, (transform.localEulerAngles.z + 90) % 360);
            //바퀴자국
            CheckTrail();
        } else {
            accelerationInput = 0;
            deccelerationInput = 0;
        }
    }

    void FixedUpdate() {
        if (!stopControl) {
            ApplySteering();
            KillOrthogonalVelocity();
            ApplyForce();
        }
    }
    //플레이어 회전
    void ApplySteering() {
        //움직이는 속도에 따라 마우스 따라가는 속도 조정
        float turnSmoothTime;
        velMag = rb.velocity.magnitude;
        if (velMag < 0.2f)
            return;
        else if (velMag < maxSpeed/3)
            turnSmoothTime = driveTurnSmoothTime * 6;
        else if (velMag < maxSpeed/2)
            turnSmoothTime = driveTurnSmoothTime * 3;
        else
            turnSmoothTime = driveTurnSmoothTime;
        //마우스위치 방향벡터로 각 구함
        lookDir = mousePos - rb.position;
        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        
        //자연스럽게 회전하도록 중간값 설정
        float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.z, targetAngle, ref turnSmoothVelocity, Time.deltaTime * turnSmoothTime);
        rb.rotation = angle;
    }
    //가속
    void ApplyForce() {
        //환경 저항
        float envDrag = mapManager.GetTileDrag(transform.position);
        //감속
        if(deccelerationInput == 1) {
            rb.drag = envDrag + Mathf.Lerp(rb.drag, breakForce, Time.deltaTime * dragTime);
        } else if(accelerationInput == 0) {
            rb.drag = envDrag + Mathf.Lerp(rb.drag, roadDrag, Time.deltaTime * dragTime);
        } else {
            rb.drag = envDrag;
        }
        Vector2 forceVector;
        if (deccelerationInput == 1) {
            //후진
            forceVector = -transform.up * deccelerationInput * reverseSpeed;
        } else {
            //전진
            forceVector = transform.up * accelerationInput * acceleration;
        }
        rb.AddForce(forceVector, ForceMode2D.Force);
        //최대속도 넘지 않도록
        if(rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    //옆으로 가는 힘 줄임
    void KillOrthogonalVelocity() {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);
        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
    //충격 vfx 생성
    bool collideable = true;
    private void OnCollisionEnter2D(Collision2D col) {
        ContactPoint2D c = col.contacts[0];
        if (/*c.normalImpulse > 1.5f && */collideable) {
            GameObject effect = Instantiate(hitEffect, c.point, Quaternion.identity);
            Destroy(effect, 0.3f);
            int damage = 0;
            if (col.gameObject.CompareTag("NPC")) {
                if (c.normalImpulse > 10)
                    damage = 5;
                else
                    damage = 3;
            } else {
                if (c.normalImpulse > 10)
                    damage = 3;
                else if (c.normalImpulse > 5)
                    damage = 2;
                else
                    damage = 1;
            }
            orderManager.AddDamage(damage);
            collideable = false;
            StartCoroutine(ColWait());
        }
    }
    IEnumerator ColWait() {
        yield return new WaitForSeconds(0.5f);
        collideable = true;
    }
    //바퀴자국 생성
    void CheckTrail() {
        if (getMovementDiff() > 0.2f && velMag > maxSpeed * 0.6f) {
            foreach(TrailRenderer tr in tireMarks) {
                tr.emitting = true;
            }
        } else {
            foreach(TrailRenderer tr in tireMarks) {
                tr.emitting = false;
            }
        }
    }
    //마우스 방향과 이동방향의 차이를 리턴한다.
    float getMovementDiff() {
        float degree = (transform.eulerAngles.z + 90) % 360;
        Vector2 playerDir = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
        return Mathf.Abs((lookDir.normalized - playerDir).magnitude);
    }
    public void Change(int n) {
        maxSpeed = speedValues[n];
        if (n == 0) {
            sr.sprite = Shoes;
            disp.Enable();
        }
        if (n == 1) {
            sr.sprite = Motorcycle;
            disp.Enable();
            disp.ChangeBlueCar();
        }
        if (n == 2)
        {
            sr.sprite = Smallcar;
            disp.Enable();
            disp.ChangeBlueCar();
        }
        if (n == 3)
        {
            sr.sprite = Truck;
            disp.Enable();
            disp.ChangeBlueCar();
        }
        if (n == 4)
        {
            sr.sprite = Policecar;
            disp.Enable();
            disp.ChangeBlueCar();
        }
        if (n == 5)
        {
            sr.sprite = Sportscar;
            disp.Enable();
            disp.ChangeBlueCar();
        }
        if (n == 6)
        {
            sr.sprite = Tank;
            disp.Enable();
            disp.ChangeBlueCar();
        }
        if (n == 7)
        {
            sr.sprite = Airplane;
            disp.Enable();
            disp.ChangeBlueCar();
        }
        if (n == 8)
        {
            sr.sprite = Dinosaur;
            disp.Enable();
            disp.ChangeBlueCar();
        }

    }
}
