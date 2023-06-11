using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CycleThroughSFX
{
    public static int clipCounter = 0;

    public static void playSFX(AudioSource[] clips)
    {
        //if (clips.Length == 0)
        //{
        //    return;
        //}
        //if (clipCounter >= clips.Length - 1)
        //{
        //    clipCounter = 0;
        //}
        //clips[clipCounter++].Play();
        clips[Random.Range(0, clips.Length)].Play();
    }
}
