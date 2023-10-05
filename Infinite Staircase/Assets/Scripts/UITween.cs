using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITween : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject gameOverBox;
    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(restartButton, new Vector3(0.9f, 0.9f, 0.9f), 0.5f).setEaseInOutSine().setLoopPingPong();
    }

    //
    public void EndScreen()
    {
        LeanTween.scale(gameOverText, new Vector3(1.0f, 1.0f, 1.0f), 1.0f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.moveLocal(gameOverText, new Vector3(0.0f, 625.0f, 0.0f), 0.75f).setDelay(1.5f).setEase(LeanTweenType.easeInOutBack);
        
        LeanTween.scale(gameOverBox, new Vector3(1.0f, 1.0f, 1.0f), 0.75f).setDelay(2.25f).setEase(LeanTweenType.easeOutBack);
    }

}
