using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonL : MonoBehaviour
{
    public List<GameObject> images;
    public List<Button> buttons;
    public Text text;
    public static int Num1;

    public int num;
    public int number;

    public void numberchecking1()
    {
        num = ButtonR.Num;
        number = num + 1;
    }

    public void Click2()
    { 
        images[num].SetActive(true);
        images[number].SetActive(false);

        if (num == 0)
        {
            buttons[0].interactable = false;
        }

        if (num == 4)
        {
            buttons[2].interactable = false;
            text.GetComponent<Text>().text = "";
        }

        buttons[1].interactable = true;

        Num1 = num;

        num--;
        number--;
    }
}

