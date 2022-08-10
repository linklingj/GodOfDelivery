using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation{
    public class NPCCarCreate : MonoBehaviour
    {
        public Transform NPCs;
        public GameObject carPrefab;
        public PathCreator[] paths;
        public stackobject blueCar, redMotorcycle, bigGreenCar, yellowCar, greenCar, whiteMotorcycle, purpleCar, brownMotorcycle, redCar;
        Vector3 spawnPos = new Vector3 (-1000, -1000, 0);
        List<PathFollow>[] cars = new List<PathFollow>[8];
        void Start() {
            for (int i = 0; i < 8; i++) {
                cars[i] = new List<PathFollow>();
            }
            //a
            MakeCar(redCar, 0, 0);
            MakeCar(redMotorcycle, 0, 0.25f);
            MakeCar(yellowCar, 0, 0.5f);
            MakeCar(bigGreenCar, 0, 0.75f);
            MakeCar(redCar, 1, 0.15f);
            MakeCar(brownMotorcycle, 1, 0.4f);
            MakeCar(purpleCar, 1, 0.65f);
            MakeCar(whiteMotorcycle, 1, 0.9f);
            //b
            MakeCar(greenCar, 2, 0);
            MakeCar(redMotorcycle, 2, 0.25f);
            MakeCar(bigGreenCar, 2, 0.5f);
            MakeCar(yellowCar, 2, 0.75f);
            MakeCar(blueCar, 3, 0);
            MakeCar(yellowCar, 3, 0.25f);
            MakeCar(redCar, 3, 0.5f);
            MakeCar(redMotorcycle, 3, 0.75f);
            //c
            MakeCar(greenCar, 4, 0);
            MakeCar(purpleCar, 4, 0.5f);
            //d
            MakeCar(bigGreenCar, 5, 0);
            MakeCar(whiteMotorcycle, 5, 0.5f);
            //e
            MakeCar(greenCar, 6, 0);
            MakeCar(blueCar, 6, 0.2f);
            MakeCar(yellowCar, 6, 0.4f);
            MakeCar(brownMotorcycle, 6, 0.5f);
            MakeCar(redCar, 6, 0.7f);
            MakeCar(brownMotorcycle, 6, 0.85f);
            //f
            MakeCar(redCar, 7, 0);
            MakeCar(purpleCar, 7, 0.2f);
            MakeCar(yellowCar, 7, 0.4f);
            MakeCar(redMotorcycle, 7, 0.5f);
            MakeCar(bigGreenCar, 7, 0.7f);
            MakeCar(purpleCar, 7, 0.85f);
        }

        void MakeCar(stackobject so, int num, float sP) {
            GameObject car = Instantiate(carPrefab, spawnPos, Quaternion.identity);
            PathFollow pf = car.GetComponent<PathFollow>();
            pf.pathCreator = paths[num];
            pf.startPoint = sP;
            pf.pathNum = num;
            car.GetComponent<displayObject>().stackObject = so;
            car.transform.SetParent(NPCs);
            cars[num].Add(pf);
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.R)) {
                //c1[0].GetComponent<PathFollow>().StopCar();
            }
            if (Input.GetKeyDown(KeyCode.T)) {
                //c1[0].GetComponent<PathFollow>().MoveCar();
            }
        }
        public void Stop(int n) {
            foreach (PathFollow car in cars[n]) {
                car.StopCar();
            }
        }
        public void Move(int n) {
            foreach (PathFollow car in cars[n]) {
                car.MoveCar();
            }
        }
    }
}
