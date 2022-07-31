using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform followPoint;
    GameObject player;
    public int offset;
    int minX,minY,maxX,maxY;
    Vector2 arrowPos;
    UIController uIController;
    RectTransform rT;
    Animator anim;
    private void Start() {
        uIController = FindObjectOfType<UIController>();
        rT = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        minX = offset;
        minY = offset;
        maxX = Screen.width - offset;
        maxY = Screen.height - offset;
    }

    void Update() {
        if(followPoint == null || uIController.phoneOpen) {
            rT.anchoredPosition = new Vector3(-1000f, -1000f, 0f);
        } else {
            Vector3 targetPos = RectTransformUtility.WorldToScreenPoint(Camera.main, followPoint.position);
            arrowPos = targetPos;
            if (minX < targetPos.x && targetPos.x < maxX && targetPos.y > minY && targetPos.y < maxY) {
                arrowPos += Vector2.up * 100f;
                transform.eulerAngles = Vector3.zero;
            } else {
                if (targetPos.x > maxX)
                    arrowPos.x = maxX;
                else if (targetPos.x < minX)
                    arrowPos.x = minX;
                if (targetPos.y > maxY)
                    arrowPos.y = maxY;
                else if (targetPos.y < minY)
                    arrowPos.y = minY;
                Vector2 lookDir = followPoint.position - player.transform.position;
                float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90;
                transform.eulerAngles = new Vector3 (0,0,targetAngle);
            }
            rT.anchoredPosition = arrowPos;
        }
    }
    public void ChangeColor() {
        anim.SetInteger("color", 2);
    }
}
