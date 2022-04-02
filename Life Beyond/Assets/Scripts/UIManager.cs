using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject satelliteObject;
    public GameObject pauseMenuObject;
    bool pauseMenu = false;

    public void CloseSatellite()
    {
        satelliteObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu = !pauseMenu;
        }

        if(pauseMenu)
        {
            OpenPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        pauseMenuObject.SetActive(false);
        pauseMenu = false;
        Time.timeScale = 1;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
