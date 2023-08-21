using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D PlayerRB;
    public float Stamina;
    public Image stamBar;
    public int speed;

    public PlayerMovement PM;

    private bool onCooldown = false;

    public float regenerationDelay;
    public float regenerationRate;
    private float timeSinceLastDash; 
    void Update()
    {
        stamBar.fillAmount = Stamina/100;
        Vector2 direction = PM.GetLastLookDirection();

        if (Stamina < 100)
        {
            timeSinceLastDash += Time.deltaTime;

            if (timeSinceLastDash >= regenerationDelay)
            {
                RegenerateStamina();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Stamina >= 10)
            {
                if (!onCooldown)
                {
                    Stamina -= 10;
                    timeSinceLastDash = 0f;
                    StartCoroutine(Dash());
                }
            }
        }
    }
    IEnumerator Dash()
    {
        PM.Speed = PM.Speed*3;
        yield return new WaitForSeconds(0.5f);
        PM.Speed = PM.Speed/3;
        StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(0.5f);  
        onCooldown = false;
    }

    private void RegenerateStamina()
    {
        Stamina += regenerationRate * Time.deltaTime;
        Stamina = Mathf.Clamp(Stamina, 0f, 100f);
    }
}
