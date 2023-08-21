using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AnvilInteractScene1 : MonoBehaviour
{
    public GameObject beachedPlayer;
    public GameObject anvil;
    public GameObject E;
    void Update()
    {
        float distance = Vector2.Distance(beachedPlayer.transform.position, anvil.transform.position);

        if(distance < 1)
        {
            E.SetActive(true);
            if(Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("GameScene1");
            }
        }
        else 
        {
            E.SetActive(false);
        }
    }
}
