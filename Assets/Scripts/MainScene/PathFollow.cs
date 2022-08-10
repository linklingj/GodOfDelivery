using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation {
    public class PathFollow : MonoBehaviour
    {
        NPCCarCreate nPCCarCreate;
        public PathCreator pathCreator;
        public int pathNum;
        public EndOfPathInstruction endOfPathInstruction;
        public displayObject disp;
        public Transform player;
        public float targetSpeed;
        float speed;
        [Range(0f, 1f)]
        public float startPoint;
        float distanceTravelled;
        bool active;

        void Start() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            nPCCarCreate = FindObjectOfType<NPCCarCreate>();
            distanceTravelled = pathCreator.path.length * startPoint;
            speed = targetSpeed;
        }

        void Update() {
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
        public void StopCar() {
            speed = 0;
        }
        public void MoveCar() {
            LeanTween.value(gameObject, speed, targetSpeed, 2f).setOnUpdate((x) => speed = x);
        }
        private void OnCollisionEnter2D(Collision2D col) {
            if (col.gameObject.CompareTag("Player")) {
                nPCCarCreate.Stop(pathNum);
            }
        }
        private void OnCollisionExit2D(Collision2D col) {
            if (col.gameObject.CompareTag("Player")) {
                StartCoroutine("Wait");
            }
        }
        IEnumerator Wait() {
            yield return new WaitForSeconds(1.5f);
            nPCCarCreate.Move(pathNum);
        }
    }
}
