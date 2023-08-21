using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordManagement : MonoBehaviour
{
    public int SwordLevel;
    public PlayerAttack PA;

    public GameObject[] SwordIcons;
    
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene1" || SceneManager.GetActiveScene().name == "GameScene2" || SceneManager.GetActiveScene().name == "GameScene3" || SceneManager.GetActiveScene().name == "GameScene4")
        {
            PA = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
            SwordIcons = GameObject.FindGameObjectsWithTag("SwordIcons");
            FixUI();
        }
    }

    public void upgradeSword()
    {
        switch (SwordLevel)
        {
            case 0:
                PA.switchWeaponAtIndex(0);
                break;
            case 1:
                PA.switchWeaponAtIndex(2);
                break;
            case 2:
                PA.switchWeaponAtIndex(3);
                break;
            case 3:
                PA.switchWeaponAtIndex(1);
                break;
        }
    }

    public void FixUI()
    {
        switch (SwordLevel)
        {
            case 0:
                SwordIcons[0].GetComponent<SpriteRenderer>().sortingOrder = 1;
                SwordIcons[1].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[2].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[3].GetComponent<SpriteRenderer>().sortingOrder = -1;
                break;
            case 1:
                SwordIcons[0].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[1].GetComponent<SpriteRenderer>().sortingOrder = 1;
                SwordIcons[2].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[3].GetComponent<SpriteRenderer>().sortingOrder = -1;
                break;
            case 2:
                SwordIcons[0].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[1].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[2].GetComponent<SpriteRenderer>().sortingOrder = 1;
                SwordIcons[3].GetComponent<SpriteRenderer>().sortingOrder = -1;
                break;
            case 3:
                SwordIcons[0].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[1].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[2].GetComponent<SpriteRenderer>().sortingOrder = -1;
                SwordIcons[3].GetComponent<SpriteRenderer>().sortingOrder = 1;
                break;
        }
    }
}
