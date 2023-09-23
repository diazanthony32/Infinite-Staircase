using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameManager gameManager;

    [Space(10)]

    public static AudioSource audioSrc;
    public static AudioClip gameMusic, jumpSound, dieSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        jumpSound = Resources.Load<AudioClip>("Jump");
    }

    //
    public static void PlaySound(string clip)
    {
        switch (clip) {
            case "Jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "Die":
                audioSrc.PlayOneShot(dieSound);
                break;
        }
    }
}
