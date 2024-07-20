using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BackToMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame();
        }
        SceneManager.LoadSceneAsync(0);
    }
}
