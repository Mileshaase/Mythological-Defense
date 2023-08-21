using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonLiftetime : MonoBehaviour
{
    private GodlyAbilities GA;
    public float skeletonLifetime;
    private GameObject[] enemies;
    public float movementSpeed = 1f;
    private Transform targetEnemy;
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        GA = playerObject.GetComponent<GodlyAbilities>();
        StartCoroutine(SetUsingAbilityAfterDestroy());

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        FindClosestEnemy(); 

        if (targetEnemy != null)
        {
            Vector2 direction = targetEnemy.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * movementSpeed;
            SetAnimation(direction);
        }
    }

    private void FindClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy.transform;
        }
    }

     private IEnumerator SetUsingAbilityAfterDestroy()
    {
        yield return new WaitForSeconds(skeletonLifetime);
        GA.usingAbilityS = false;
        Destroy(gameObject);
    }

    private void SetAnimation(Vector2 direction)
    {
        // Determine the animation based on the movement direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Horizontal movement
            if (direction.x > 0)
            {
                anim.Play("Skeleton_Right");
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
            else if (direction.x < 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
                anim.Play("Skeleton_Right");
            }
        }
        else
        {
            // Vertical movement
            if (direction.y > 0)
                anim.Play("Skeleton_Up");
            else if (direction.y < 0)
                anim.Play("Skeleton_Down");
        }
    }
}
