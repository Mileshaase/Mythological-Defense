using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victory : MonoBehaviour
{
    public ParticleSystem PS;
    public AudioSource Sound;
    public TMP_Text textMesh;
    public float fadeDuration = 1.5f;
    public float displayDuration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        PS = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    public void Win()
    {
        Sound.Play();
        StartCoroutine(ShowAndFadeOut());
        StartCoroutine(victory());
    }

    public IEnumerator victory()
    {
        PS.Play();
        yield return new WaitForSeconds(10);
        PS.Stop();
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
