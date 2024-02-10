using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlBTT : MonoBehaviour
{
    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restart");
    }
    public void QuitTheGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
