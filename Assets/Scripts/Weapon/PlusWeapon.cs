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

        /* Wait until Player health is implemented
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if(!enemy)
        {
            return;
        }

        BasePlayer player = this.GetComponent<BasePlayer>();
        if(enemy.BaseHealth < power)
        {
            // consume enemy into self
            player.BaseHealth += enemy.BaseHealth;
            enemy.BaseHealth = -1;
        } else {
            // Add one to both
            player.BaseHealth += 1;
            enemy.BaseHealth += 1;
        }
        */
    }
}
