using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    private int _currentHP;
    // Start is called before the first frame update
    void Start()
    {
        _currentHP = 999;
    }

    // Update is called once per frame
    void Update()
    {
        _currentHP -= 1;
    }

    public int GetCurrentHP() {
        return _currentHP;
    }
}
