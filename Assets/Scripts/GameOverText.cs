using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverText : MonoBehaviour
{
    public static GameOverText Instance { get; private set; }

    private TMP_Text gameOverText;

    void Awake()
    {
        Debug.Log("Awake");

        gameOverText = GetComponent<TMP_Text>();
        gameOverText.gameObject.SetActive(false);

        Instance = this;
    }

    private void SetText(string message)
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = message;
    }

    public void GameWin()
    {
        SetText("You Win!\nThanks for playing!\nBy Andre Powell\nPress R to Restart");
        EndMusic.Instance.PlayWinMusic();
    }

    public void GameLose()
    {
        SetText("You Lose!\nPress R to Restart!");
        EndMusic.Instance.PlayLoseMusic();


    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelWin()
    {
        SetText("You Win!\nTalk to Jambi to Go to Level 2!");
    }

}
