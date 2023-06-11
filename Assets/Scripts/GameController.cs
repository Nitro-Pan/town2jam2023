using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CommonDefinitions;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private List<Operators> _playerArsenal;
    [SerializeField] private Operators _activeOperator;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject _playerHotBar;

    private void Start()
    {
        _healthBar.transform.Find("MaximumPower").gameObject.GetComponent<TMP_Text>().text = SumEnemyHP().ToString();
        _healthBar.transform.Find("CurrentPower").gameObject.GetComponent<TMP_Text>().text = SumEnemyHP().ToString();
        _playerArsenal.Add(Operators.Addition);
        _playerArsenal.Add(Operators.Subtraction);
        _activeOperator = Operators.Subtraction;
        GameObject.Find("WeaponUI2").transform.Find("WeaponImage").GetComponent<Image>().color = Color.yellow;
    }
        
    private void Update() 
    {
        UpdateHPBar();
        UpdatePlayerHP();
    }

    private void UpdatePlayerHP() {
        if (_player == null) return;
        _playerHotBar.transform.Find("Power").gameObject.GetComponent<TMP_Text>().text = _player.GetComponent<BasePlayerController>().Health.ToString();
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
            maxHP += enemy.GetComponent<BaseEnemy>().CurrentHealth;
        }
        return maxHP;
    }
    public void SwapWeapon()
    {
        _activeOperator += 1;
        if ((int) _activeOperator >= _playerArsenal.Count) {
            _activeOperator = 0;
        }
        _weapon.GetComponent<BaseWeapon>().ActiveOperator = _activeOperator;
        if (_activeOperator == Operators.Addition) 
        {
            GameObject.Find("WeaponUI1").transform.Find("WeaponImage").GetComponent<Image>().color = Color.yellow;
            GameObject.Find("WeaponUI2").transform.Find("WeaponImage").GetComponent<Image>().color = Color.white;
            GameObject.Find("WeaponUI3").transform.Find("WeaponImage").GetComponent<Image>().color = Color.white;
            GameObject.Find("WeaponUI4").transform.Find("WeaponImage").GetComponent<Image>().color = Color.white;
        }
        else if (_activeOperator == Operators.Subtraction)
        {
            GameObject.Find("WeaponUI2").transform.Find("WeaponImage").GetComponent<Image>().color = Color.yellow;
            GameObject.Find("WeaponUI1").transform.Find("WeaponImage").GetComponent<Image>().color = Color.white;
            GameObject.Find("WeaponUI3").transform.Find("WeaponImage").GetComponent<Image>().color = Color.white;
            GameObject.Find("WeaponUI4").transform.Find("WeaponImage").GetComponent<Image>().color = Color.white;
        }
    }
}
