using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("OpeningScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
