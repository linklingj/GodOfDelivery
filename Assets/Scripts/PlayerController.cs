using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float driftFactor;
    public float driveTurnSmoothTime;
    public float roadDrag;
    public float dragTime;
    public float breakForce;

    float accelerationInput;
    float deccelerationInput;
    float steeringInput;
    float rotationAngle;
    float turnSmoothVelocity;
    Vector2 mousePos;
    bool stopControl = false;

    Rigidbody2D rb;
    displayObject disp;
    public Camera cam;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        disp = GetComponent<displayObject>();
    }

    void Update() {
        //입력 제어
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        accelerationInput = Input.GetAxisRaw("accelerate");
        deccelerationInput = Input.GetAxisRaw("deccelerate");
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
        float velMag = rb.velocity.magnitude;
        if (velMag < 0.2f)
            return;
        else if (velMag < maxSpeed/3)
            turnSmoothTime = driveTurnSmoothTime * 6;
        else if (velMag < maxSpeed/2)
            turnSmoothTime = driveTurnSmoothTime * 3;
        else
            turnSmoothTime = driveTurnSmoothTime;
        //마우스위치 방향벡터로 각 구함
        Vector2 lookDir = mousePos - rb.position;
        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        
        //자연스럽게 회전하도록 중간값 설정
        float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.z, targetAngle, ref turnSmoothVelocity, Time.deltaTime * turnSmoothTime);
        rb.rotation = angle;
        //회전 애니메이션 설정
        disp.rotation = new Vector3(0, 0, (transform.localEulerAngles.z + 90) % 360);
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
        Vector2 forceVector = transform.up * accelerationInput * acceleration;
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
    void StopMovement() {
        rb.velocity = Vector2.zero;
    }
    
}
