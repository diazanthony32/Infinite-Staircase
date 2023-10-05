using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreManager : MonoBehaviour
{
    public GameManager gameManager;

    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }
    public bool NewHighScore { get; private set; } = false;

    private Player GM_Player;

    // Start is called before the first frame update
    void Start()
    {
        GM_Player = gameManager.player;

        HighScore = PlayerPrefs.GetInt("High Score");
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        if (GM_Player.IsGrounded && !GM_Player.IsDead)
        {
            CurrentScore += 1;
        }

        if (CurrentScore > HighScore)
        {
            NewHighScore = true;

            HighScore = CurrentScore;
            PlayerPrefs.SetInt("High Score", CurrentScore);
        }
    }
}
