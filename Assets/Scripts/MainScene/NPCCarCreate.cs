using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation{
    public class NPCCarCreate : MonoBehaviour
    {
        public GameObject carPrefab;
        public PathCreator pathA1, pathB1;
        public stackobject blueCar, redMotorcycle;
        Vector3 spawnPos = new Vector3 (-1000, -1000, 0);
        void Start() {
            MakeCar(carPrefab, blueCar, pathA1, 0);
            MakeCar(carPrefab, redMotorcycle, pathA1, 0.25f);
            MakeCar(carPrefab, blueCar, pathA1, 0.5f);
            MakeCar(carPrefab, redMotorcycle, pathA1, 0.75f);
        }

        void MakeCar(GameObject obj, stackobject so, PathCreator p, float sP) {
            GameObject car = Instantiate(obj, spawnPos, Quaternion.identity);
            car.GetComponent<PathFollow>().pathCreator = p;
            car.GetComponent<PathFollow>().startPoint = sP;
            car.GetComponent<displayObject>().stackObject = so;
        }
    }
}
