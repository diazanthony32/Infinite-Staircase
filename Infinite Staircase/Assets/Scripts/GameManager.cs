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
    public Parallax parallax;

    [Space(10)]

    public CinemachineVirtualCamera VCamera;


    [Space(10)]

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.IsGrounded)
            StartCoroutine(GameOver());
    }

    // On player "death", this coroutine gets enabled
    IEnumerator GameOver()
    {
        // disable player input
        uiManager.viewPlayerInput.SetActive(false);

        yield return new WaitForSeconds(3.0f);

        uiManager.viewGameUI.SetActive(false);
        uiManager.viewGameOver.SetActive(true);

        yield return null;
    }
}
