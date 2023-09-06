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

    [HideInInspector] public int currentScore;
    public TMP_Text currentScoreText;

    [Space(10)]

    public Canvas inputCanvas;

    [Space(10)]

    public Canvas gameOverCanvas;

    [HideInInspector] public int highScore;
    public TMP_Text highScoreText;
    public TMP_Text finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score");
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = currentScore.ToString();
        gracePeriodSlider.value = (gameManager.player.gracePeriodTimer / gameManager.player.PLAYER_GRACE_PERIOD);
    }

    public void EndScreen()
    {

    }
}
