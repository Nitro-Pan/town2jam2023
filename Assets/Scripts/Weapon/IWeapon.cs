using System;
using UnityEngine;

public interface IWeapon
{
	public bool TryAttack();

	public void Attack();

	public void OnTriggerEnter(Collider other);

	public void SetColliderEnabled(bool colliderEnabled);
}
