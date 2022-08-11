using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuildingScene : MonoBehaviour
{
    public void gotowhere()
    {
        SceneManager.LoadScene("Pre_Start");
    }

    public List<GameObject> images;
    public List<GameObject> buildings;
    public List<Text> texts;

    public void build1()
    {
        images[0].SetActive(true);
        buildings[0].SetActive(true);
        texts[0].GetComponent<Text>().text = "200$";
        texts[1].GetComponent<Text>().text = "";
    }
}
