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

        BaseEnemy enemy = other.GetComponent<BaseEnemy>();
        if(!enemy)
        {
            return;
        }

        BasePlayerController player = GetComponent<BasePlayerController>();
        enemy.BaseHealth -= player.Health;
    }
}
