using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private List<BaseWeapon> _playerArsenal;
    [SerializeField] private BaseWeapon _activeWeapon;
    [SerializeField] private GameObject _healthBar;

    private void Start()
    {
        _healthBar.transform.Find("MaximumPower").gameObject.GetComponent<TMP_Text>().text = SumEnemyHP().ToString();
        _healthBar.transform.Find("CurrentPower").gameObject.GetComponent<TMP_Text>().text = SumEnemyHP().ToString();
    }

    private void Update() 
    {
        UpdateHPBar();
        SwapWeapons();
    }

    private void UpdateHPBar() 
    {
        if (_healthBar == null || _enemies.Count == 0) return;

        int currentHP = SumEnemyHP();
        int maxHP = int.Parse(_healthBar.transform.Find("MaximumPower").gameObject.GetComponent<TMP_Text>().text);

        _healthBar.GetComponent<Slider>().value = (float) currentHP / (float) maxHP;
        _healthBar.transform.Find("CurrentPower").gameObject.GetComponent<TMP_Text>().text = currentHP.ToString();
    }

    private int SumEnemyHP()
    {
        int maxHP = 0;
        foreach (GameObject enemy in _enemies) {
            maxHP += enemy.GetComponent<BaseEnemy>().GetCurrentHP();
        }
        return maxHP;
    }
    private void SwapWeapons()
    {
        
    }
}
