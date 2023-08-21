using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[Header("Enemy Weapon")]
	[Tooltip("This is the current weapon that the enemy is using")]
	public Damager weapon;

	[Header("Parameters")]

	public bool showAttackRadius = false;
	public float attackRadius = 5f;
	[Tooltip("The coolDown before you can attack again")]
	public float coolDown = 0.5f;
	private bool canAttack = true;

	private void Update()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
		foreach (Collider2D other in colliders)
		{
			if (canAttack && (other.CompareTag("Player") || other.CompareTag("Skeleton")))
			{
				Attack(other.transform.position - this.transform.position);
			}
		}
	}

	public void Attack(Vector2 attackDir)
	{
		//This is where the weapon is rotated in the right direction that you are facing
		if (weapon && canAttack)
		{
			if (weapon is ProjectileWeapon)
				weapon.WeaponStart(this.transform, attackDir, Vector2.zero);
			else
				weapon.WeaponStart(this.transform, attackDir);

			StartCoroutine(CoolDown());
		}
	}

	public void StopAttack()
	{
		if (weapon)
		{
			weapon.WeaponFinished();
		}
	}

	private IEnumerator CoolDown()
	{
		canAttack = false;
		yield return new WaitForSeconds(coolDown);
		canAttack = true;
	}

	private void OnDrawGizmos()
	{
		if (showAttackRadius)
			Gizmos.DrawWireSphere(transform.position, attackRadius);
	}
}
