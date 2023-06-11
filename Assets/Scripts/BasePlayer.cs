using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerController : MonoBehaviour
{
    public int Health { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Health = 999;
    }

    // Update is called once per frame
    void Update()
    {
        Health -= 1;
    }
}
