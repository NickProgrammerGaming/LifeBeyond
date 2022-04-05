using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public IntSO maxHp;
    public IntSO damageModifiers;

    private void Start()
    {
        maxHp.Value = 5;
        damageModifiers.Value = 0;
    }

    public void Play()
    {
        FindObjectOfType<AudioManager>().Stop("MainMenu");
        FindObjectOfType<AudioManager>().Play("Level1");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
