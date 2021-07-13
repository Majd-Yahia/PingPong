using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI whom;
    [SerializeField] private TextMeshProUGUI scoreToWin;
    [SerializeField] private GameData gameData;
    private void Start()
    {
        whom.text = "Bot";
        scoreToWin.text = gameData.scoreToWin.ToString();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Toggle()
    {
        gameData.againstPlayer = !gameData.againstPlayer;

        if (gameData.againstPlayer)
        {
            whom.text = "Player 2";
        }
        else
        {
            whom.text = "Bot";
        }
    }

    private void SetValue()
    {
        scoreToWin.text = gameData.scoreToWin.ToString();
    }

    public void Increase()
    {
        if (gameData.scoreToWin < 100)
        {
            gameData.scoreToWin++;
            SetValue();
        }
    }

    public void Decrease()
    {
        if (gameData.scoreToWin > 1)
        {
            gameData.scoreToWin--;
            SetValue();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
