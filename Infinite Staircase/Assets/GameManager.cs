using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public AudioManager audioManager;
    public UIManager uiManager;

    [Space(10)]

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isOnPlatform == false)
        {
            StartCoroutine(GameOver());
        }

    }

    // On player "death", this coroutine gets enabled
    IEnumerator GameOver()
    {
        // disable player input
        uiManager.inputCanvas.enabled = false;

        yield return new WaitForSeconds(1.5f);
        player.playerRB.bodyType = RigidbodyType2D.Dynamic;

        // checks if the current score is higher than their best score, if so make it the new highest
        if (uiManager.currentScore > uiManager.highScore)
        {
            uiManager.highScore = uiManager.currentScore;
            PlayerPrefs.SetInt("High Score", uiManager.currentScore);
        }

        yield return new WaitForSeconds(1.5f);
        uiManager.highScoreText.text = uiManager.highScore.ToString();
        uiManager.finalScoreText.text = uiManager.currentScore.ToString();
        uiManager.gameUICanvas.enabled = false;
        uiManager.gameOverCanvas.enabled = true;

        yield return null;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
