using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Now : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Now");
    }
}
