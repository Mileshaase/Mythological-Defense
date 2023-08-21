using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Entered");
            Rigidbody2D rigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                rigidbody.simulated = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Exited");
            other.transform.rotation = new Quaternion(other.transform.rotation.x, other.transform.rotation.y, 0f, other.transform.rotation.w);
        }
    }
}
