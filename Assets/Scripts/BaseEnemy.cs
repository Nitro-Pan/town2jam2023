using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private int _currentHP = 1000000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentHP -= 100;
    }

    public int GetCurrentHP() {
        return _currentHP;
    }
}
