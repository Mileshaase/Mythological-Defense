using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhippedAway : MonoBehaviour
{
    public float rotationSpeed = 800f;
    public float moveSpeed = 2f;

    private bool isSpinning = false;
    // Update is called once per frame

    private void Update()
    {
        // Rotate the GameObject on the Z-axis
        if (isSpinning)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            // Move the GameObject up on the Y-axis
            if (transform.position.y < 5f) // Change 5f to the desired height you want to reach
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        // Stop spinning and moving up after reaching a certain height (e.g., 5 units)
        if (transform.position.y >= 5f && isSpinning)
            isSpinning = false;
    }

    public void Spin()
    {
        isSpinning = true;
    }
}
