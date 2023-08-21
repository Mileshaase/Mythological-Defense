using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartanMovement : MonoBehaviour
{
    public GodlyAbilities GA;
    public GameObject player;
    public float movementSpeed = 2f;
    public Animator animator;

    private void Start() 
    {
        player = GameObject.Find("MainPlayer");
        GA = player.GetComponent<GodlyAbilities>();
    }
    private void Update()
    {
        if(!GA.usingAbilityL)
        {
            // Calculate movement direction
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Update position based on movement direction and speed
            transform.position += direction * movementSpeed * Time.deltaTime;

            // Update animation based on movement direction
            SetAnimation(direction);
        }
        else
        {
            animator.Play("Soldier_STRUCK");
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
}
