using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translationlayer : MonoBehaviour
{
    [field: SerializeField] private BasePlayerController player { get; set; }

    public void OnRollAnimationComplete()
    {
        player.OnRollAnimationComplete();
    }

    public void OnRollStart()
    {
        player.OnRollAnimStart();
    }

    public void OnRollIFrameEnd()
    {
        player.OnRollIFrameEnd();
    }
}
