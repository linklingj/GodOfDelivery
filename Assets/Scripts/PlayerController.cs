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
    displayObject disp;
    public Camera cam;
    public TrailRenderer[] tireMarks;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        disp = GetComponent<displayObject>();
    }

    void Update() {
        //입력 제어
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        accelerationInput = Input.GetAxisRaw("accelerate");
        deccelerationInput = Input.GetAxisRaw("deccelerate");
        //회전 애니메이션 설정
        disp.rotation = new Vector3(0, 0, (transform.localEulerAngles.z + 90) % 360);
        //바퀴자국
        CheckTrail();
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
        //감속
        if(accelerationInput == 0) {
            rb.drag = Mathf.Lerp(rb.drag, roadDrag, Time.deltaTime * dragTime);
        } else if(deccelerationInput == 1) {
            rb.drag = Mathf.Lerp(rb.drag, breakForce, Time.deltaTime * dragTime);
            return;
        } else {
            rb.drag = 0;
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
    private void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("hit");
    }
    private void OnCollisionStay2D(Collision2D col) {
        if (col.transform.CompareTag("Grass")) {
            Debug.Log("grass");
        }
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
}
