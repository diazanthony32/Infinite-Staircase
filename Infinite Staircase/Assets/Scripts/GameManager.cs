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
    public ScoreManager scoreManager;

    [Space(10)]

    public CinemachineVirtualCamera VCamera;

    [Space(10)]

    public Player player;

    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead && !isGameOver)
            StartCoroutine(GameOver());
    }

    // On player "death", this coroutine gets enabled
    IEnumerator GameOver()
    {
        // makes sure this runs only once
        isGameOver = true;

        // disable player input
        uiManager.viewPlayerInput.SetActive(false);

        yield return new WaitForSeconds(3.0f);

        uiManager.viewGameUI.SetActive(false);

        uiManager.viewGameOver.SetActive(true);
        uiManager.UITween.EndScreen();
    }
}
