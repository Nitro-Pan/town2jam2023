using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonDefinitions;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField]
    private AudioClip _attackSound;

    // Weapon collider hitbox
    [SerializeField]
    private Collider _collider;

    // Determines how fast the attack is
    [SerializeField]
    private float _attackSpeed = 1.0f;

    // May not be able to attack: out of ammo, various reasons
    [field: SerializeField] public bool CanAttack { get; set; }

    // What operator is active: what will occur on hit
    [field: SerializeField] public Operators ActiveOperator { get; set; } = Operators.Subtraction;


    // Start is called before the first frame update
    public void Start()
    {
        _attackSound = GetComponent<AudioClip>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    public void Update()
    {
        // do nothing
    }

    /**
     * Exists for side-effects (e.g. spawning projectiles): doesn't do anything right now for melee attacks
     */
    public void Attack()
    {
        // do nothing
    }

    /**
     * What occurs on hit
     */
    public void OnTriggerEnter(Collider other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        // BasePlayer player = ??? how to get player ref
        if (!enemy) return; // no enemy collision: do nothing


        switch(ActiveOperator)
        {
            case Operators.Addition:
                break;
            case Operators.Subtraction:
                break;
            case Operators.Multiplication:
                break;
            case Operators.Division:
                break;
        }
    }

    public bool TryAttack()
    {
        if(!CanAttack)
        {
            return false;
        }
        Attack();
        return true;
    }

    public void SetColliderEnabled(bool colliderEnabled)
    {
        _collider.enabled = colliderEnabled;
    }

    // OnEquip?
    // OnUnqeuip?
}
