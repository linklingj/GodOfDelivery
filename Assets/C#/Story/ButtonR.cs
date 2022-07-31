using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonR : MonoBehaviour
{
    public int num = 0;
    public int number = 1;
    public static int Num;

    public List<GameObject> images;
    public List<Button> buttons;
    public Text text;

    public void numberchecking2()
    {
        num = ButtonL.Num1;
        number = num + 1;
    }

    public void Click1()
    {
        images[num].SetActive(false);
        images[number].SetActive(true);

        if (number == 5)
        {
            buttons[1].interactable = false;
            buttons[2].interactable = true;
            text.GetComponent<Text>().text = "Start";
        }

        buttons[0].interactable = true;

        Num = num;

        num++;
        number++;
    }
}
