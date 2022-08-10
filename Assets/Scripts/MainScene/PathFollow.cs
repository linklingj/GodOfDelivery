using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation {
    public class PathFollow : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public displayObject disp;
        public Transform player;
        public float speed = 5;
        [Range(0f, 1f)]
        public float startPoint;
        float distanceTravelled;
        bool active;

        void Start() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            distanceTravelled = pathCreator.path.length * startPoint;
        }

        void Update()
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            Quaternion angle = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            angle.x = 0f;
            angle.y = 0f;
            transform.rotation = angle;
            transform.Rotate(0,0,90);
            float dist = Vector2.Distance(player.position, transform.position);
            if (dist < 25f && !active)
                disp.enabled = true;
            if (dist >= 25f && active)
                disp.enabled = false;
            if (dist < 25f)
                disp.rotation = new Vector3(0, 0, (transform.localEulerAngles.z + 90) % 360);
        }
    }
}
