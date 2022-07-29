using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public Transform player;
    Camera cam;
    PlayerController playerController;
    Vector2 camPos;
    Vector3 targetPos;
    public float minX,maxX,minY,maxY;
    public float maxLookAhead;

    void Start() {
        playerController = player.GetComponent<PlayerController>();
        cam = GetComponent<Camera>();
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10f);
    }
    void Update() {
        if(GameManager.Instance.State != GameState.Play)
            return;
        //카메라가 미리 앞으로 나가는 정도 -> 플레이어 속도에 비례
        float lookAhead = (playerController.velMag / playerController.maxSpeed) * maxLookAhead;
        //플레이어의 방향을 위치벡터로 전환
        float degree = (player.eulerAngles.z + 90) % 360;
        targetPos = new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad) * 0.6f, 0) * lookAhead;
        //카메라의 위치 부드럽게 변환
        camPos = Vector2.Lerp(transform.position, player.position + targetPos, Time.deltaTime * speed);
        //카메라가 맵 경계 벗어나지 않도록 조정
        if(camPos.x > maxX)
            camPos.x = maxX;
        if(camPos.x < minX)
            camPos.x = minX;
        if(camPos.y > maxY)
            camPos.y = maxY;
        if(camPos.y < minY)
            camPos.y = minY;
        //카메라 위치 설정
        transform.position = new Vector3(camPos.x,camPos.y,transform.position.z);
    }
    public void ResultScreen() {
        LeanTween.value(gameObject, cam.orthographicSize, 3f, 3f).setDelay(3f).setEase(LeanTweenType.easeInOutSine).setOnUpdate((float flt) =>  {
            cam.orthographicSize = flt;
        });
        LeanTween.moveLocal(gameObject, transform.position + Vector3.right * 3f, 3f).setDelay(3f).setEase(LeanTweenType.easeInOutSine);
    }
}
