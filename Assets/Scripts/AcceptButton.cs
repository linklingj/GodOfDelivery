using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptButton : MonoBehaviour
{
    UIController uIController;
    [SerializeField]
    Button button;
    private void Start() {
        Button btn = button.GetComponent<Button>();
        uIController = FindObjectOfType<UIController>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick() {
        uIController.AcceptOrder(transform.parent.gameObject);
    }
    
}
