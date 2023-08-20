using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*

    GOALS:
        - Control the spacings of the platform and player movement
        - Controls score monitoring
        - Controls the end and beginning of each game
        - 
    
     */

    [HideInInspector] public float PLATFORM_X_SPACING = 2.65f;
    [HideInInspector] public float PLATFORM_Y_SPACING = 1.15f;

    public CinemachineVirtualCamera vCamera;

    [Space(10)]

    public Canvas gameUICanvas;
    public Canvas inputCanvas;
    public Canvas gameOverCanvas;

    [Space(10)]

    [HideInInspector] public int currentScore;
    public TMP_Text currentScoreText;
    public TMP_Text finalScoreText;
    [HideInInspector] public int highScore;
    public TMP_Text highScoreText;

    [Space(10)]

    public Slider gracePeriodSlider;

    [Space(10)]

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score");
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = currentScore.ToString();
        gracePeriodSlider.value = (player.gracePeriodTimer / player.PLAYER_GRACE_PERIOD);

        if (player.isOnPlatform == false)
        {
            StartCoroutine(GameOver());
        }

    }

    IEnumerator GameOver()
    {
        inputCanvas.enabled = false;
        vCamera.Follow = null;

        yield return new WaitForSeconds(1.5f);
        player.playerRB.bodyType = RigidbodyType2D.Dynamic;

        // checks if the current score is higher than their best score, if so make it the new highest
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("High Score", currentScore);
        }

        yield return new WaitForSeconds(1.5f);
        highScoreText.text = highScore.ToString();
        finalScoreText.text = currentScore.ToString();
        gameUICanvas.enabled = false;
        gameOverCanvas.enabled = true;

        yield return null;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
