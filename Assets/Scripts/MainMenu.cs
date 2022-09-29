using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void AppPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void AppQuit()
    {
        Application.Quit();
    }
}
