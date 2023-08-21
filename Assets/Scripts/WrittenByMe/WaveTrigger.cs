using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;    

public class WaveTrigger : MonoBehaviour
{
    public WaveManager WM;
    public AudioSource Horns;

    public TMP_Text textMesh;
    public float fadeDuration = 1.5f;
    public float displayDuration = 2.0f;

    void Update()
    {
        Horns = this.GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name == "GameScene1" || SceneManager.GetActiveScene().name == "GameScene2" || SceneManager.GetActiveScene().name == "GameScene3" || SceneManager.GetActiveScene().name == "GameScene4")
        {
            WM = GameObject.FindGameObjectWithTag("Manager").GetComponent<WaveManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            if(WM.waveOngoing == false)
            {
                Horns.Play();
                StartCoroutine(ShowAndFadeOut());
                WM.startWave = true;
            }
        }
    }

    private IEnumerator ShowAndFadeOut()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            textMesh.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            textMesh.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        // Ensure alpha is 0 at the end of the fade out
        textMesh.alpha = 0f;
    }
}
