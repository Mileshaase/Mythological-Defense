using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public bool startWave = false;
    public int waveLevel = 1;   
    public int numOfSpartans;
    public bool timeForBoss;
    public int count = 0;

    public Transform player;
    public Transform Spawn;

    public PlayerHealth PH;
    public PlayerMovement PM;

    public GameObject Spartan;
    public GameObject SpartanBoss;
    public bool livin;

    public GameObject[] spawns;
    private GameObject[] enemies;

    public bool waveOngoing = false;

    public int numberKilled = 0;

    public WhippedAway WA;
    public Victory V;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);   
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene1" || SceneManager.GetActiveScene().name == "GameScene2" || SceneManager.GetActiveScene().name == "GameScene3" || SceneManager.GetActiveScene().name == "GameScene4")
        {
            WA = GameObject.FindGameObjectWithTag("Player").GetComponent<WhippedAway>();
            V = GameObject.FindGameObjectWithTag("Player").GetComponent<Victory>();
            PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Spawn = GameObject.FindGameObjectWithTag("Spawn").transform;

            spawns = GameObject.FindGameObjectsWithTag("Spawns");
        }
        
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(startWave)
        {
            switch (waveLevel)
            {
                case 1:
                    Wave1();
                    
                    break;
                case 2:
                    Wave2();
                    break;
                case 3:
                    Wave3();
                    break;
                case 4:
                    Wave4();
                    break;
                case 5:
                    Wave5();
                    break;
                default:
                    break;
            }
        }
    }

    public void Wave1()
    {
        startWave = false;
        waveOngoing = true;
        numOfSpartans = 1;

        for (int i = 0; i < numOfSpartans; i++)
        {
            GameObject spartan = Instantiate(Spartan, ChooseRandomSpawn().position, Spartan.transform.rotation);
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        StartCoroutine(WhenTheyAllFall());
    }

    public void Wave2()
    {
        startWave = false;
        waveOngoing = true;
        numOfSpartans = 5;

        for (int i = 0; i < numOfSpartans; i++)
        {
            GameObject spartan = Instantiate(Spartan, ChooseRandomSpawn().position, Spartan.transform.rotation);
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        StartCoroutine(WhenTheyAllFall());
    }

    public void Wave3()
    {
        startWave = false;
        waveOngoing = true;
        numOfSpartans = 8;

        for (int i = 0; i < numOfSpartans; i++)
        {
            GameObject spartan = Instantiate(Spartan, ChooseRandomSpawn().position, Spartan.transform.rotation);
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        StartCoroutine(WhenTheyAllFall());
    }

    public void Wave4()
    {
        startWave = false;
        waveOngoing = true;
        numOfSpartans = 10;

        for (int i = 0; i < numOfSpartans; i++)
        {
            GameObject spartan = Instantiate(Spartan, ChooseRandomSpawn().position, Spartan.transform.rotation);
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        StartCoroutine(WhenTheyAllFall());
    }

    public void Wave5()
    {
        startWave = false;
        waveOngoing = true;
        numOfSpartans = 1;

        for (int i = 0; i < numOfSpartans; i++)
        {
            GameObject spartan = Instantiate(SpartanBoss, ChooseRandomSpawn().position, SpartanBoss.transform.rotation);
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        StartCoroutine(WhenTheyAllFall());
    }

    public IEnumerator WhenTheyAllFall()
    {
        yield return new WaitUntil(() => enemies.Length == 0);
        waveLevel++;
        V.Win();
        yield return new WaitForSeconds(11f);
        waveOngoing = false;
        player.position = Spawn.position;
        PH.ResetHealth();
        waveOngoing = false;
        if(waveLevel == 2)
        {
            yield return new WaitForSeconds(3f);
            PM.disabled = false;
            WA.Spin();
            yield return new WaitForSeconds(3f);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("GodEncounter1");
        }
        else if(waveLevel == 4)
        {
            yield return new WaitForSeconds(3f);
            PM.disabled = true;
            WA.Spin();
            yield return new WaitForSeconds(3f);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("GodEncounter2");
        }
        else if(waveLevel == 5)
        {
            yield return new WaitForSeconds(3f);
            PM.disabled = true;
            WA.Spin();
            yield return new WaitForSeconds(3f);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("GodEncounter3");
        }
    }

    public Transform ChooseRandomSpawn()
    {
        int randomIndex = Random.Range(0, spawns.Length);
        return spawns[randomIndex].transform;
    }
}
