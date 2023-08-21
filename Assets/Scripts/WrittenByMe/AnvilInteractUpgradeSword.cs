using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnvilInteractUpgradeSword : MonoBehaviour
{
    public GameObject player;
    public GameObject anvil;
    public GameObject text;
    public GameObject E;

    public WaveManager WM;
    public SwordManagement SM;

    public bool check;

    void Start()
    {
        WM = GameObject.FindGameObjectWithTag("Manager").GetComponent<WaveManager>();
        SM = GameObject.FindGameObjectWithTag("Manager").GetComponent<SwordManagement>();
    }

    void Update()
    {
            float distance = Vector2.Distance(player.transform.position, anvil.transform.position);

        if(distance < 1)
        {
            E.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                int level = SM.SwordLevel;

                if(WM.numberKilled < 3)
                {
                    SM.SwordLevel = 0;
                }
                else if(WM.numberKilled >= 3 && WM.numberKilled < 8)
                {
                    SM.SwordLevel = 1;
                }
                else if(WM.numberKilled >= 8 && WM.numberKilled < 16) 
                {
                    SM.SwordLevel = 2;
                }
                else if(WM.numberKilled >= 16)
                {
                    SM.SwordLevel = 3;
                }

                if(SM.SwordLevel == level)
                {
                    text.SetActive(true);
                    StartCoroutine(Wait());
                }

                SM.upgradeSword();
                check = false;
            }
        }
        else 
        {
            E.SetActive(false);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        text.SetActive(false);
    }
}
