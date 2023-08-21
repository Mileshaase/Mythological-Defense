using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManMoveScript : MonoBehaviour
{
    public Transform Spawn;
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Point4;
    public Transform Point5;
    public Transform Point6;
    public Transform Point7;


    public Rigidbody2D OldManRB;
    public Transform OldMan;
    public Animator anim;
    public float speed;

    public bool start = false;

    private int pointReached = 0;

    void Update()
    {
        if(start == true)
        {
            switch (pointReached)
            {
                case 0:
                    MoveEm(Spawn);
                    break;
                case 1:
                    MoveEm(Point1);
                    break;
                case 2:
                    MoveEm(Point2);
                    break;
                case 3:
                    MoveEm(Point3);
                    break;
                case 4:
                    MoveEm(Point4);
                    break;
                case 5:
                    anim.SetFloat("MoveHorizontal", -1);
                    MoveEm(Point5);
                    break;
                case 6:
                    anim.SetFloat("MoveHorizontal", 0);
                    MoveEm(Point6);
                    break;
                case 7:
                    MoveEm(Point7);
                    break;
                case 8:
                    anim.SetBool("isMoving", false);
                    break;
                default:
                    break;
            }
        }
    }

    void MoveEm(Transform targetPoint)
    {
        // Calculate the direction from the current position to the target position
        Vector2 direction = targetPoint.position - OldMan.position;
        SetAnimation(direction);

        // Normalize the direction vector to get a unit vector
        direction.Normalize();

        // Calculate the new position to move towards
        Vector2 newPosition;
        if(pointReached != 0)
        {
            newPosition = (Vector2)OldMan.position + direction * speed * Time.deltaTime;
        }
        else
        {
            newPosition = (Vector2)OldMan.position + direction * Time.deltaTime;
        }

        // Move the Rigidbody towards the new position
        OldManRB.MovePosition(newPosition);

        // Check if the old man has reached the target point
        if (Vector3.Distance(OldMan.position, targetPoint.position) < 0.1f)
        {
            pointReached++;
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
                anim.Play("OldMan_RIGHT");
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
            else if (direction.x < 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
                anim.Play("OldMan_RIGHT");
            }
        }
        else
        {
            // Vertical movement
            if (direction.y > 0)
                anim.Play("OldMan_UP");
            else if (direction.y < 0)
                anim.Play("OldMan_DOWN");
        }
    }
}   