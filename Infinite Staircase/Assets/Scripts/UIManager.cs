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

    private ScoreManager GM_ScoreManager;
    private Player GM_Player;


    //
    private void Start()
    {
        GM_ScoreManager = gameManager.scoreManager;
        GM_Player = gameManager.player;
    }


    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = GM_ScoreManager.CurrentScore.ToString();
        gracePeriodSlider.value = (GM_Player.GracePeriodTimer / GM_Player.PLAYER_GRACE_PERIOD);

        highScoreText.text = GM_ScoreManager.HighScore.ToString();
        finalScoreText.text = GM_ScoreManager.CurrentScore.ToString();
    }

}
