using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject satelliteObject;
    public GameObject pauseMenuObject;
    bool pauseMenu = false;
    private PlayerShooting shooting;
    private PlayerMovement movement;

    private void Start()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        shooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
    }

    public void CloseSatellite()
    {
        satelliteObject.SetActive(false);
        movement.enabled = true;
        PlayerShooting.canShoot = true;
    }

    public void DMGUp()
    {
        shooting.damageModifiers.Value++;
    }

    public void HPUp()
    {
        movement.maxHealth.Value++;
        movement.currentHealth = movement.maxHealth.Value;
        movement.playerHealthbar.SetMaxHealth(movement.maxHealth.Value);
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
