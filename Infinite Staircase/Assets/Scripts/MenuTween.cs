using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTween : MonoBehaviour
{
    public GameObject playButton;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(playButton, new Vector3(0.9f, 0.9f, 0.9f), 0.5f).setEaseInOutSine().setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
