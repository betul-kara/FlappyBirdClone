using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Restart()
    {
        PlayerController.isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        PlayerController.isGameOver = false;
        SceneManager.LoadScene(1);
    }
}
