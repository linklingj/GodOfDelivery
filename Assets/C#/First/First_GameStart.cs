using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class First_GameStart : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Story");
    }
}
