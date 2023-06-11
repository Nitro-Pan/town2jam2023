using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseWeapon : MonoBehaviour, IWeapon
{
    // May not be able to attack: out of ammo, various reasons
    [SerializeField]
    protected bool _canAttack;

    // Determines how fast the attack is
    [SerializeField]
    protected float _attackSpeed;

    // Associated weapon sound
    [SerializeField]
    protected AudioClip _attackSound;

    // Weapon collider hitbox
    [SerializeField]
    protected Collider _collider;


    // Start is called before the first frame update
    public virtual void Start()
    {
        this._attackSound = GetComponent<AudioClip>();
        this._collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public abstract void Attack();

    // On hit
    public abstract void OnTriggerEnter(Collider other);

    public virtual bool TryAttack()
    {
        if(!_canAttack)
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
