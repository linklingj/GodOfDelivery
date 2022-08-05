using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameManager;
    [SerializeField]
    Transform p;
    private void Awake() {
        if (FindObjectOfType<GameManager>() == null) {
            GameObject obj = Instantiate(gameManager, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(p);
        }
    }
}
