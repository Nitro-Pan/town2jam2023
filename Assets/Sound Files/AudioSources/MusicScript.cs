using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    public AudioSource intro;
    public AudioSource loop;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playAudioSequentially());
    }

    IEnumerator playAudioSequentially()
    {
        intro.Play();

        while (intro.isPlaying)
        {
            yield return null;
        }
        loop.Play();
    }
}
