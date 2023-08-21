using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GodlyAbilities : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement PM;
    private GameObject[] enemies;

    [Header("Lightning")]
    public GameObject lightningStrike;
    public float regenerationRateL;
    private float lightning = 0;
    public Image Lightning;
    private Animator anim;

    [Header("Wave")]
    public GameObject wave;
    public ParticleSystem splash;
    public float regenerationRateW;
    private float waveF = 0;
    public Image Wave;
    private bool waveScalingInProgress = false; // New flag to track scaling state

    [Header("Skeletons")]
    public GameObject Skeleton;
    public Transform[] spawns;
    private List<GameObject> skeletons;
    public float regenerationRateS;
    private float skeleton = 0;
    public Image SkeletonI;

    public bool usingAbilityL = false;
    public bool usingAbilityW = false;
    public bool usingAbilityS = false;

    public bool unlockedLightning = false;
    public bool unlockedWave = false;
    public bool unlockedSkeletons = false;

    private SpartanDamage SD;

    void Start()
    {
        PM = player.GetComponent<PlayerMovement>();
        splash = wave.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        Lightning.fillAmount = lightning/100;
        Wave.fillAmount = waveF/100;
        SkeletonI.fillAmount = skeleton/100;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(!usingAbilityL && !usingAbilityW && !usingAbilityW) 
        {
            if(unlockedLightning)
            {
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    if (lightning == 0)
                    {
                        if (enemies.Length > 0)
                        {
                            foreach (GameObject enemy in enemies)
                            {
                                float distance = Vector2.Distance(player.transform.position, enemy.transform.position);
                                if (distance < 2)
                                {
                                    usingAbilityL = true;
                                    lightning = 100;
                                    anim = enemy.GetComponent<Animator>();
                                    StartCoroutine(LAnim(enemy, anim));
                                }
                            }
                        }
                    }
                }
            }

            if(unlockedWave)
            {
                if (Input.GetKey(KeyCode.Alpha2))
                {
                    if ((waveF == 0) && !waveScalingInProgress)
                    {
                        waveF = 100;
                        usingAbilityW = true;
                        wave.SetActive(true);
                        StartCoroutine(ScaleObject());
                    }
                }
            }

            if (unlockedSkeletons)
            {
                if (Input.GetKey(KeyCode.Alpha3))
                {
                    if(skeleton == 0)
                    {
                        usingAbilityS = true;
                        skeleton = 100;
                        foreach (Transform spawn in spawns)
                        {
                            GameObject skeleton = Instantiate(Skeleton, spawn.transform.position, spawn.transform.rotation);
                        }
                    }
                }
            }
        }

        if(lightning > 0 && !usingAbilityL)
        {
            reLoadL();
        }
        if(waveF > 0 && !usingAbilityW)
        {
            reLoadW();
        }
        if(skeleton > 0 && !usingAbilityS)
        {
            reLoadS();
        }
    }

    public IEnumerator LAnim(GameObject enemy, Animator anim)
    {
        PM.disabled = true;
        Vector3 position = new Vector3(enemy.transform.position.x + 0.16f, enemy.transform.position.y + 0.35f, enemy.transform.position.z);
        GameObject lightning = Instantiate(lightningStrike, position, enemy.transform.rotation);
        SD = enemy.GetComponent<SpartanDamage>();
        SD.currentHealth = SD.currentHealth/2;
        SD.UpdateHealthBar();
        anim.SetBool("Struck", true);
        yield return new WaitForSeconds(1);
        Destroy(lightning);
        anim.SetBool("Struck", false);
        usingAbilityL = false;
        PM.disabled = false;
    }

    private IEnumerator ScaleObject()
    {
        PM.disabled = true;
        waveScalingInProgress = true; // Set scaling in progress flag
        splash.Play();

        float targetScale = 4;

        while (wave.transform.localScale.x < targetScale)
        {
            float scaleIncrement = 1f * Time.deltaTime;
            wave.transform.localScale += new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);
            yield return null;
        }

        while (wave.transform.localScale.x > 0.5f)
        {
            float scaleIncrement = 5f * Time.deltaTime;
            wave.transform.localScale -= new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);
            yield return null;
        }

        wave.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        waveScalingInProgress = false;

        wave.SetActive(false);
        PM.disabled = false;
        usingAbilityW = false;
        splash.Stop();
        yield break;
    }

    private void reLoadL()
    {
        lightning -= regenerationRateL * Time.deltaTime;
        lightning = Mathf.Clamp(lightning, 0f, 100f);
    }
    private void reLoadW()
    {
        waveF -= regenerationRateW * Time.deltaTime;
        waveF = Mathf.Clamp(waveF, 0f, 100f);
    }
    private void reLoadS()
    {
        skeleton -= regenerationRateS * Time.deltaTime;
        skeleton = Mathf.Clamp(skeleton, 0f, 100f);
    }
}
