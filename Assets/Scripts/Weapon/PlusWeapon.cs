using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlusWeapon : BaseWeapon
{
    public override void Attack()
    {
        // Do nothing: all effects are in OnTriggerEnter
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(!TryAttack())
        {
            // Back out of attack
            return;
        }

        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if(!enemy)
        {
            return;
        }

        BasePlayerController player = GetComponent<BasePlayerController>();
        if(enemy.BaseHealth < player.Health)
        {
            // consume enemy into self
            player.Health += enemy.CurrentHealth;
            enemy.BaseHealth = -1;
        } else {
            // Add one to both
            player.Health += 1;
            enemy.BaseHealth += 1;
        }
    }
}
