using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ride : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Ride");
    }
}