using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonDefinitions;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    // May not be able to attack: out of ammo, various reasons
    [field: SerializeField] public bool CanAttack { get; set; }

    // Determines how fast the attack is
    [SerializeField]
    protected float _attackSpeed;

    // Associated weapon sound
    [SerializeField]
    protected AudioClip _attackSound;

    // Weapon collider hitbox
    [SerializeField]
    protected Collider _collider;

    // What operator is active: what will occur on hit
    [field: SerializeField] public Operators ActiveOperator { get; set; }


    // Start is called before the first frame update
    public void Start()
    {
        this._attackSound = GetComponent<AudioClip>();
        this._collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    public void Update()
    {

    }

    /**
     * Exists for side-effects (spawning projectiles): doesn't do anything right now for melee attacks
     */
    public void Attack()
    {

    }

    // On hit
    public void OnTriggerEnter(Collider other)
    {

    }

    public virtual bool TryAttack()
    {
        if(!CanAttack)
        {
            return false;
        }
        Attack();
        return true;
    }

    public virtual void SetColliderEnabled(bool colliderEnabled)
    {
        _collider.enabled = colliderEnabled;
    }

    // OnEquip?
    // OnUnqeuip?
}
