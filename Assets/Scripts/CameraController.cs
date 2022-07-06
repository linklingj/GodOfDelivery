using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public Transform player;
    Vector2 camPos;
    public float minX,maxX,minY,maxY;

    void Start() {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10f);
    }
    void Update()
    {
        camPos = Vector2.Lerp(transform.position, player.position, Time.deltaTime * speed);
        if(camPos.x > maxX)
            camPos.x = maxX;
        if(camPos.x < minX)
            camPos.x = minX;
        if(camPos.y > maxY)
            camPos.y = maxY;
        if(camPos.y < minY)
            camPos.y = minY;
        transform.position = new Vector3(camPos.x,camPos.y,transform.position.z);
    }
}
