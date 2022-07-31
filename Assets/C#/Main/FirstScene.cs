using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("First");
    }
}
