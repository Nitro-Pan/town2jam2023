using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusWeapon : BaseWeapon
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
        enemy.BaseHealth -= player.BaseHealth;
        */
    }
}
