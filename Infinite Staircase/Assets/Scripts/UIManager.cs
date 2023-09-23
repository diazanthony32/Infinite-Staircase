using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public GameManager gameManager;

    [Space(10)]

    public Canvas gameUICanvas;

    public Slider gracePeriodSlider;

    public TMP_Text currentScoreText;

    [Space(10)]

    public Canvas inputCanvas;

    [Space(10)]

    public Canvas gameOverCanvas;

    public TMP_Text highScoreText;
    public TMP_Text finalScoreText;

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = gameManager.scoreManager.CurrentScore.ToString();
        gracePeriodSlider.value = (gameManager.player.GracePeriodTimer / gameManager.player.PLAYER_GRACE_PERIOD);

        highScoreText.text = gameManager.scoreManager.HighScore.ToString();
        finalScoreText.text = gameManager.scoreManager.CurrentScore.ToString();
    }

}
