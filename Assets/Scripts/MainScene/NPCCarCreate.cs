using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation{
    public class NPCCarCreate : MonoBehaviour
    {
        public Transform NPCs;
        public GameObject carPrefab;
        public PathCreator pathA1, pathA2, pathB1, pathB2, pathC1, pathD1, pathE1, pathF1;
        public stackobject blueCar, redMotorcycle, bigGreenCar, yellowCar, greenCar, whiteMotorcycle, purpleCar, brownMotorcycle, redCar;
        Vector3 spawnPos = new Vector3 (-1000, -1000, 0);
        void Start() {
            //a
            MakeCar(carPrefab, redCar, pathA1, 0);
            MakeCar(carPrefab, redMotorcycle, pathA1, 0.25f);
            MakeCar(carPrefab, yellowCar, pathA1, 0.5f);
            MakeCar(carPrefab, bigGreenCar, pathA1, 0.75f);
            MakeCar(carPrefab, redCar, pathA2, 0.15f);
            MakeCar(carPrefab, brownMotorcycle, pathA2, 0.4f);
            MakeCar(carPrefab, purpleCar, pathA2, 0.65f);
            MakeCar(carPrefab, whiteMotorcycle, pathA2, 0.9f);
            //b
            MakeCar(carPrefab, greenCar, pathB1, 0);
            MakeCar(carPrefab, redMotorcycle, pathB1, 0.25f);
            MakeCar(carPrefab, bigGreenCar, pathB1, 0.5f);
            MakeCar(carPrefab, yellowCar, pathB1, 0.75f);
            MakeCar(carPrefab, blueCar, pathB2, 0);
            MakeCar(carPrefab, yellowCar, pathB2, 0.25f);
            MakeCar(carPrefab, redCar, pathB2, 0.5f);
            MakeCar(carPrefab, redMotorcycle, pathB2, 0.75f);
            //c
            MakeCar(carPrefab, greenCar, pathC1, 0);
            MakeCar(carPrefab, purpleCar, pathC1, 0.5f);
            //d
            MakeCar(carPrefab, bigGreenCar, pathD1, 0);
            MakeCar(carPrefab, whiteMotorcycle, pathD1, 0.5f);
            //e
            MakeCar(carPrefab, greenCar, pathE1, 0);
            MakeCar(carPrefab, blueCar, pathE1, 0.2f);
            MakeCar(carPrefab, yellowCar, pathE1, 0.4f);
            MakeCar(carPrefab, brownMotorcycle, pathE1, 0.5f);
            MakeCar(carPrefab, redCar, pathE1, 0.7f);
            MakeCar(carPrefab, brownMotorcycle, pathE1, 0.85f);
            //f
            MakeCar(carPrefab, redCar, pathF1, 0);
            MakeCar(carPrefab, purpleCar, pathF1, 0.2f);
            MakeCar(carPrefab, yellowCar, pathF1, 0.4f);
            MakeCar(carPrefab, redMotorcycle, pathF1, 0.5f);
            MakeCar(carPrefab, bigGreenCar, pathF1, 0.7f);
            MakeCar(carPrefab, purpleCar, pathF1, 0.85f);
        }

        void MakeCar(GameObject obj, stackobject so, PathCreator p, float sP) {
            GameObject car = Instantiate(obj, spawnPos, Quaternion.identity);
            car.GetComponent<PathFollow>().pathCreator = p;
            car.GetComponent<PathFollow>().startPoint = sP;
            car.GetComponent<displayObject>().stackObject = so;
            car.transform.SetParent(NPCs);
        }
    }
}
