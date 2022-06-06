using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public Text _tWinSide;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnMaintMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void WinSide()
    {
        gameObject.SetActive(true);

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            _tWinSide.text = "Поремога бежевих";
        }

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            _tWinSide.text = "Поремога червоних";
        }
    }
}
