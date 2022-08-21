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
            switch (GameManager.Instance.Lvl) {
                case 1:
                    //a
                    MakeCar(greenCar, 0, 0);
                    MakeCar(redMotorcycle, 0, 0.5f);
                    MakeCar(blueCar, 1, 0.25f);
                    MakeCar(bigGreenCar, 1, 0.75f);
                    //b
                    MakeCar(brownMotorcycle, 2, 0);
                    MakeCar(bigGreenCar, 2, 0.5f);
                    MakeCar(greenCar, 3, 0.25f);
                    MakeCar(whiteMotorcycle, 3, 0.75f);
                    //e
                    MakeCar(greenCar, 6, 0);
                    MakeCar(purpleCar, 6, 0.3f);
                    MakeCar(blueCar, 6, 0.6f);
                    //f
                    MakeCar(redCar, 7, 0);
                    MakeCar(redMotorcycle, 7, 0.4f);
                    MakeCar(yellowCar, 7, 0.8f);
                    break;
                case 2:
                    //a
                    MakeCar(bigGreenCar, 0, 0.1f);
                    MakeCar(redMotorcycle, 0, 0.4f);
                    MakeCar(whiteMotorcycle, 0, 0.8f);
                    MakeCar(yellowCar, 1, 0.15f);
                    MakeCar(purpleCar, 1, 0.4f);
                    MakeCar(purpleCar, 1, 0.85f);
                    //b
                    MakeCar(greenCar, 2, 0);
                    MakeCar(redMotorcycle, 2, 0.3f);
                    MakeCar(bigGreenCar, 2, 0.7f);
                    MakeCar(brownMotorcycle, 3, 0);
                    MakeCar(redCar, 3, 0.3f);
                    MakeCar(blueCar, 3, 0.8f);
                    //c
                    MakeCar(whiteMotorcycle, 4, 0);
                    //d
                    MakeCar(bigGreenCar, 5, 0.5f);
                    //e
                    MakeCar(yellowCar, 6, 0);
                    MakeCar(redCar, 6, 0.3f);
                    MakeCar(bigGreenCar, 6, 0.6f);
                    MakeCar(brownMotorcycle, 6, 0.8f);
                    //f
                    MakeCar(blueCar, 7, 0.1f);
                    MakeCar(yellowCar, 7, 0.3f);
                    MakeCar(purpleCar, 7, 0.6f);
                    MakeCar(redMotorcycle, 7, 0.9f);
                    break;
                case 3:
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
                    break;
                case 4:
                    //a
                    MakeCar(greenCar, 0, 0);
                    MakeCar(redMotorcycle, 0, 0.2f);
                    MakeCar(yellowCar, 0, 0.4f);
                    MakeCar(bigGreenCar, 0, 0.6f);
                    MakeCar(blueCar, 0, 0.8f);
                    MakeCar(yellowCar, 1, 0.1f);
                    MakeCar(brownMotorcycle, 1, 0.3f);
                    MakeCar(purpleCar, 1, 0.6f);
                    MakeCar(purpleCar, 1, 0.8f);
                    MakeCar(whiteMotorcycle, 1, 0.9f);
                    //b
                    MakeCar(redCar, 2, 0);
                    MakeCar(redMotorcycle, 2, 0.2f);
                    MakeCar(bigGreenCar, 2, 0.4f);
                    MakeCar(yellowCar, 2, 0.6f);
                    MakeCar(bigGreenCar, 2, 0.8f);
                    MakeCar(blueCar, 3, 0);
                    MakeCar(redCar, 3, 0.2f);
                    MakeCar(whiteMotorcycle, 3, 0.4f);
                    MakeCar(redMotorcycle, 3, 0.6f);
                    MakeCar(purpleCar, 3, 0.8f);
                    //c
                    MakeCar(greenCar, 4, 0);
                    MakeCar(whiteMotorcycle, 4, 0.33f);
                    MakeCar(redCar, 4, 0.66f);
                    //d
                    MakeCar(bigGreenCar, 5, 0);
                    MakeCar(redMotorcycle, 5, 0.33f);
                    MakeCar(whiteMotorcycle, 5, 0.66f);
                    //e
                    MakeCar(yellowCar, 6, 0);
                    MakeCar(blueCar, 6, 0.2f);
                    MakeCar(yellowCar, 6, 0.4f);
                    MakeCar(greenCar, 6, 0.5f);
                    MakeCar(redCar, 6, 0.7f);
                    MakeCar(brownMotorcycle, 6, 0.85f);
                    //f
                    MakeCar(bigGreenCar, 7, 0);
                    MakeCar(purpleCar, 7, 0.2f);
                    MakeCar(purpleCar, 7, 0.4f);
                    MakeCar(redMotorcycle, 7, 0.5f);
                    MakeCar(redCar, 7, 0.7f);
                    MakeCar(purpleCar, 7, 0.85f);
                    break;
                case 5:
                    //a
                    MakeCar(greenCar, 0, 0);
                    MakeCar(redCar, 0, 0.2f);
                    MakeCar(yellowCar, 0, 0.4f);
                    MakeCar(greenCar, 0, 0.6f);
                    MakeCar(blueCar, 0, 0.8f);
                    MakeCar(bigGreenCar, 1, 0.1f);
                    MakeCar(brownMotorcycle, 1, 0.3f);
                    MakeCar(purpleCar, 1, 0.6f);
                    MakeCar(yellowCar, 1, 0.8f);
                    MakeCar(whiteMotorcycle, 1, 0.9f);
                    //b
                    MakeCar(redCar, 2, 0);
                    MakeCar(blueCar, 2, 0.2f);
                    MakeCar(bigGreenCar, 2, 0.4f);
                    MakeCar(whiteMotorcycle, 2, 0.6f);
                    MakeCar(bigGreenCar, 2, 0.8f);
                    MakeCar(blueCar, 3, 0);
                    MakeCar(purpleCar, 3, 0.2f);
                    MakeCar(whiteMotorcycle, 3, 0.4f);
                    MakeCar(redCar, 3, 0.6f);
                    MakeCar(purpleCar, 3, 0.8f);
                    //c
                    MakeCar(bigGreenCar, 4, 0);
                    MakeCar(redMotorcycle, 4, 0.33f);
                    MakeCar(redCar, 4, 0.66f);
                    //d
                    MakeCar(purpleCar, 5, 0);
                    MakeCar(redMotorcycle, 5, 0.33f);
                    MakeCar(brownMotorcycle, 5, 0.66f);
                    //e
                    MakeCar(greenCar, 6, 0);
                    MakeCar(blueCar, 6, 0.1f);
                    MakeCar(brownMotorcycle, 6, 0.3f);
                    MakeCar(greenCar, 6, 0.5f);
                    MakeCar(redCar, 6, 0.7f);
                    MakeCar(brownMotorcycle, 6, 0.8f);
                    MakeCar(bigGreenCar, 6, 0.9f);
                    //f
                    MakeCar(bigGreenCar, 7, 0);
                    MakeCar(redCar, 7, 0.2f);
                    MakeCar(purpleCar, 7, 0.35f);
                    MakeCar(redMotorcycle, 7, 0.5f);
                    MakeCar(redCar, 7, 0.65f);
                    MakeCar(whiteMotorcycle, 7, 0.8f);
                    MakeCar(yellowCar, 7, 0.9f);
                    break;
                case 6:
                    //a
                    MakeCar(redCar, 0, 0);
                    MakeCar(greenCar, 0, 0.2f);
                    MakeCar(purpleCar, 0, 0.4f);
                    MakeCar(yellowCar, 0, 0.6f);
                    MakeCar(greenCar, 0, 0.8f);
                    MakeCar(blueCar, 0, 0.95f);
                    MakeCar(bigGreenCar, 1, 0.1f);
                    MakeCar(brownMotorcycle, 1, 0.3f);
                    MakeCar(brownMotorcycle, 1, 0.5f);
                    MakeCar(yellowCar, 1, 0.7f);
                    MakeCar(whiteMotorcycle, 1, 0.8f);
                    MakeCar(yellowCar, 1, 0.9f);
                    //b
                    MakeCar(redCar, 2, 0);
                    MakeCar(yellowCar, 2, 0.2f);
                    MakeCar(bigGreenCar, 2, 0.4f);
                    MakeCar(whiteMotorcycle, 2, 0.5f);
                    MakeCar(redCar, 2, 0.6f);
                    MakeCar(blueCar, 2, 0.8f);
                    MakeCar(blueCar, 3, 0);
                    MakeCar(purpleCar, 3, 0.1f);
                    MakeCar(whiteMotorcycle, 3, 0.3f);
                    MakeCar(redCar, 3, 0.5f);
                    MakeCar(redMotorcycle, 3, 0.7f);
                    MakeCar(purpleCar, 3, 0.8f);
                    //c
                    MakeCar(bigGreenCar, 4, 0);
                    MakeCar(redMotorcycle, 4, 0.25f);
                    MakeCar(whiteMotorcycle, 4, 0.5f);
                    MakeCar(blueCar, 4, 0.75f);
                    //d
                    MakeCar(purpleCar, 5, 0);
                    MakeCar(redMotorcycle, 5, 0.25f);
                    MakeCar(yellowCar, 5, 0.5f);
                    MakeCar(brownMotorcycle, 5, 0.75f);
                    //e
                    MakeCar(greenCar, 6, 0);
                    MakeCar(blueCar, 6, 0.1f);
                    MakeCar(brownMotorcycle, 6, 0.2f);
                    MakeCar(greenCar, 6, 0.3f);
                    MakeCar(greenCar, 6, 0.4f);
                    MakeCar(brownMotorcycle, 6, 0.6f);
                    MakeCar(bigGreenCar, 6, 0.7f);
                    MakeCar(redMotorcycle, 6, 0.8f);
                    //f
                    MakeCar(bigGreenCar, 7, 0);
                    MakeCar(bigGreenCar, 7, 0.2f);
                    MakeCar(purpleCar, 7, 0.3f);
                    MakeCar(redCar, 7, 0.5f);
                    MakeCar(greenCar, 7, 0.6f);
                    MakeCar(whiteMotorcycle, 7, 0.7f);
                    MakeCar(purpleCar, 7, 0.8f);
                    MakeCar(yellowCar, 7, 0.9f);
                    break;
                default:
                break;
            }
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
