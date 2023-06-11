using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject winScreen;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0f;
        winScreen.SetActive(true);
    }

}
