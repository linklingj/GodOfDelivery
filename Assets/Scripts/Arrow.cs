using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform followPoint;
    public int offset;
    int minX,minY,maxX,maxY;
    Vector2 arrowPos;
    RectTransform rT;
    private void Start() {
        rT = GetComponent<RectTransform>();
        minX = offset;
        minY = offset;
        maxX = Screen.width - offset;
        maxY = Screen.height - offset;
    }

    void Update() {
        if(followPoint == null) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            Vector3 targetPos = RectTransformUtility.WorldToScreenPoint(Camera.main, followPoint.position);
            arrowPos = targetPos;
            if (minX < targetPos.x && targetPos.x < maxX && targetPos.y > minY && targetPos.y < maxY) {
                
            } else {
                if (targetPos.x > maxX)
                    arrowPos.x = maxX;
                else if (targetPos.x < minX)
                    arrowPos.x = minX;
                if (targetPos.y > maxY)
                    arrowPos.y = maxY;
                else if (targetPos.y < minY)
                    arrowPos.y = minY;
            }
            rT.anchoredPosition = arrowPos;
        }
    }
}
