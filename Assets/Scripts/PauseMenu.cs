using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenuUI; // Ссылка на панель меню паузы

    private bool isPaused = false;

    void Update()
    {
        // Нажатие клавиши Escape для активации/деактивации паузы
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Возобновляем игру
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Восстанавливаем нормальное течение времени
        isPaused = false;
    }

    public void Pause()
    {
        // Ставим игру на паузу
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Останавливаем время
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        // Возвращаем время в норму и загружаем главное меню
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
