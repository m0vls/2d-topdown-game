using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        SaveSystem.Instance.LoadGameOnce();
        Application.targetFrameRate = 60;
        Screen.SetResolution(SaveSystem.Instance.width, SaveSystem.Instance.height, SaveSystem.Instance.fullScreen);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("game");
    }

    public void QuitButton()
    {
        SaveSystem.Instance.SaveGame();
        Application.Quit();
    }
}
