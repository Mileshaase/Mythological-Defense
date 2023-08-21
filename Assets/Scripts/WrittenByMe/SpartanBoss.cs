using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartanBoss : MonoBehaviour
{
    [Header("Enemy Weapon")]
	[Tooltip("This is the current weapon that the enemy is using")]
	public Damager weapon;

	[Header("Parameters")]

	public bool showAttackRadius = false;
	public float attackRadius = 5f;
	[Tooltip("The coolDown before you can attack again")]
	public float coolDown = 0.5f;
    public float chargeCoolDown = 2f;
	private bool canAttack = true;
    private bool canCharge = true;
    public float movementSpeed;
    public GameObject player;
    public float followDistance = 8f;
    public float chargeDuration = 2f;
    public float chargeTime;
    public float chargeSpeed = 20f;

    private Rigidbody2D rb;
    private bool isCharging = false;
    private bool charging = false;
    private Vector3 chargeDirection;
    private Vector2 originalPosition;

    public GodlyAbilities GA;
    public Animator animator;

    private Color startingColor;
    public Color targetColor = Color.red;
    public SpriteRenderer bossSpriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingColor = bossSpriteRenderer.color; // Store the initial color
        originalPosition = rb.position;
        player = GameObject.FindGameObjectWithTag("Player");
        GA = player.GetComponent<GodlyAbilities>();
    }

    private void Update()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 bossPosition = rb.position;
        float distanceToPlayer = Vector2.Distance(bossPosition, playerPosition);
        // Calculate movement direction
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if(!GA.usingAbilityL)
        {
            if (!charging)
            {
                if(!canCharge)
                {
                    // Update position based on movement direction and speed
                    transform.position += direction * movementSpeed * Time.deltaTime;

                    // Update animation based on movement direction
                    SetAnimation(direction);

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
                    foreach (Collider2D other in colliders)
                    {
                        if (canAttack && other.CompareTag("Player"))
                        {
                            Attack(other.transform.position - this.transform.position);
                        }
                    }
                }
                else  
                { 
                    // Follow the player if the player is beyond the follow distance
                    if (distanceToPlayer > followDistance)
                    {
                        // Update position based on movement direction and speed
                        transform.position += direction * movementSpeed * Time.deltaTime;

                        // Update animation based on movement direction
                        SetAnimation(direction);
                    }
                    else
                    {
                        // Charge behavior when within follow distance
                        chargeDirection = (player.transform.position - transform.position).normalized;
                        Debug.Log(chargeDirection);
                        rb.velocity = Vector2.zero;
                        StartCoroutine(ChargeAttack());
                    }
                }
            }

            if(isCharging)
            {
                rb.AddForce(chargeDirection * chargeSpeed, ForceMode2D.Impulse);
                bossSpriteRenderer.color = startingColor;
                StartCoroutine(wait());
            }
        }
        else
        {
            animator.Play("Soldier_STRUCK");
        }
    }

    private IEnumerator ChargeAttack()
    {
        charging = true;
        
        // Gradually change the color to the target color over the chargeDuration
        float elapsedTime = 0f;
        while (elapsedTime < chargeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / chargeDuration;
            bossSpriteRenderer.color = Color.Lerp(startingColor, targetColor, t);
            yield return null;
        }

        isCharging = true;
    }
	private IEnumerator CoolDown()
	{
		canAttack = false;
		yield return new WaitForSeconds(coolDown);
		canAttack = true;
	}

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(chargeTime);
        rb.velocity = Vector2.zero;
        isCharging = false;
        charging = false;
        StartCoroutine(ChargeCoolDown());
    }
    private IEnumerator ChargeCoolDown()
	{
		canCharge = false;
		yield return new WaitForSeconds(chargeCoolDown);
		canCharge = true;
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

    private void SetAnimation(Vector2 direction)
    {
        // Determine the animation based on the movement direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Horizontal movement
            if (direction.x > 0)
            {
                animator.Play("Soldier_RIGHT");
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
            else if (direction.x < 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
                animator.Play("Soldier_RIGHT");
            }
        }
        else
        {
            // Vertical movement
            if (direction.y > 0)
                animator.Play("Soldier_UP");
            else if (direction.y < 0)
                animator.Play("Soldier_DOWN");
        }
    }

    public void StopAttack()
	{
		if (weapon)
		{
			weapon.WeaponFinished();
		}
	}

	private void OnDrawGizmos()
	{
		if (showAttackRadius)
        {
			Gizmos.DrawWireSphere(transform.position, attackRadius);
            // Draw a visual representation of the follow distance
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, followDistance);
        }
	}

    public void ResetBoss()
    {
        // Reset the boss to its original position and state
        rb.position = originalPosition;
        rb.velocity = Vector2.zero;
        isCharging = false;
    }
}
