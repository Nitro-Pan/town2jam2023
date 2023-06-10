using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private List<BaseWeapon> _playerArsenal;
    [SerializeField] private BaseWeapon _activeWeapon;
    [SerializeField] private GameObject _healthBar;

    private void Start()
    {
        _healthBar.transform.Find("MaxHP").gameObject.GetComponent<TMP_Text>().text = sumEnemyHP().ToString();
    }

    private void Update()
    {
        if (_healthBar != null || _enemies.Count == 0) return;

        int currentHP = sumEnemyHP();
        int maxHP = int.Parse(_healthBar.transform.Find("MaxHP").gameObject.GetComponent<TMP_Text>().text);

        if ( maxHP == currentHP ) return;

        _healthBar.GetComponent<Slider>.Value = currentHP / maxHP;
    }

    private int sumEnemyHP()
    {
        int maxHP = 0;
        foreach (GameObject enemy in _enemies) {
            maxHP += enemy.GetComponent<BaseEnemy>().getCurrentHP();
        }
        return maxHP;
    }
