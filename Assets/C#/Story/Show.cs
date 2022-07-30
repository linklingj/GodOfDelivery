using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour
{
    public List<GameObject> images;
    public Text text1;
    public Text text2;
    public Text text3;
    public List<Button> buttons;


    public void Click()
    {
        images[0].SetActive(false);
        images[1].SetActive(true);

        text1.GetComponent<Text>().text = "";
        text2.GetComponent<Text>().text = "";
        text3.GetComponent<Text>().text = "";

        buttons[0].interactable = false;
        buttons[1].interactable = false;
        buttons[2].interactable = true;
    }
}
