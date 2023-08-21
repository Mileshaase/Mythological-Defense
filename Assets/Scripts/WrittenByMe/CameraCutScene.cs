using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutScene : MonoBehaviour
{
    public float speed;
    public GameObject UI;
    public PlayerMovement PM;
    public CameraMovement CM;
    public Camera Cam;
    public Rigidbody2D Camera;
    public Transform cameraPos;
    public Transform ShipMover;
    public Transform ShipPos;
    public Rigidbody2D ships;
    public GameObject trigger;
    private bool moving;

    public void Update()
    {
        float distance = Vector2.Distance(gameObject.transform.position, cameraPos.position);
        float ShipDistance = Vector2.Distance(ShipMover.position, ShipPos.position);
        if(moving)
        {
            if(distance > 1)
            {
                Vector2 direction = cameraPos.position - gameObject.transform.position;
                direction.Normalize();

                Vector2 newPosition = (Vector2)gameObject.transform.position + direction * speed * Time.deltaTime;

                Camera.MovePosition(newPosition);
            }
            else
            {
                if(ShipDistance > 0.5)
                {
                    Vector2 direction = ShipPos.position - ShipMover.position;
                    direction.Normalize();

                    Vector2 newPosition = (Vector2)ShipMover.position + direction * speed * Time.deltaTime;

                    ships.MovePosition(newPosition);
                }
                else
                {
                    StartCoroutine(Wait());
                }
            }
        }
    }
    public void Ships()
    {
        UI.SetActive(false);
        PM.enabled = false;

        CM.enabled = false;
        Cam.orthographicSize = 6;
        

        moving = true;
    }

    public IEnumerator Wait() 
    {
        yield return new WaitForSeconds(1);
        Reset();
    }
    
    public void Reset()
    {
        UI.SetActive(true);
        PM.enabled = true;

        CM.enabled = true;
        Cam.orthographicSize = 2;
        moving = false;

        trigger.SetActive(true);
    }
}
