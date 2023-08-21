using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpartanDamage : MonoBehaviour
{
    //This class should be placed on anything enemy related! Or anything that the player can damage

	public WaveManager WM;

	[Header("Settings")]
	public bool EnemyHealthBar = false;
	public int maxHealth = 100;
	public int currentHealth;

	[Header("Health Bar")]
	[Tooltip("Padding between healthbar and enemy")]
	public float padding = 2f;

	[Tooltip("Use this to control how wide or tall the health bar is")]
	public Vector2 Dimensions;

	[Tooltip("Prefab for healthbar")]
	public GameObject HealthBar;
	public RectTransform canvasRectTransform;


	private Image healthBarImage;
	private RectTransform healthRectTransform;
    public float shakeDistance = 0.1f;
    public float shakeDuration = 0.2f;
    public float shakeFrequency = 10f;

    private Vector3 originalPosition;

	private void Awake() 
	{
		WM = GameObject.FindGameObjectWithTag("Manager").GetComponent<WaveManager>();
	}

	void Start()
	{
		currentHealth = maxHealth;

		if (EnemyHealthBar)
		{
			if (canvasRectTransform == null)
				canvasRectTransform = GameObject.FindGameObjectWithTag("EnemyHealthCanvas").GetComponent<RectTransform>();

			GameObject newHealthBar = Instantiate(HealthBar, transform.position, Quaternion.identity);
			healthRectTransform = newHealthBar.GetComponent<RectTransform>();
			healthRectTransform.sizeDelta += Dimensions;

			newHealthBar.transform.SetParent(canvasRectTransform);

			healthBarImage = newHealthBar.GetComponent<Image>();
			healthBarImage.type = Image.Type.Filled;

			UpdateHealthBar();
		}
	}

	void Update()
	{
		if (EnemyHealthBar)
		{
			Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
			healthRectTransform.anchoredPosition = screenPoint - canvasRectTransform.sizeDelta / 2f;
			healthRectTransform.anchoredPosition += new Vector2(0f, padding);
		}
	}

	public void DecreaseHealth(int value)
	{
        originalPosition = transform.position;
		currentHealth -= value;
        StartCoroutine(ShakeCoroutine());
		if (currentHealth <= 0)
		{
			currentHealth = 0;
		}

		if (EnemyHealthBar)
			UpdateHealthBar();
	}

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float xShake = Random.Range(-1f, 1f) * shakeDistance;
            float yShake = Random.Range(-1f, 1f) * shakeDistance;

            transform.position = originalPosition + new Vector3(xShake, yShake, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

	public void UpdateHealthBar()//Updates the health bar according to the new health amounts
	{
		float fillAmount = (float)currentHealth / maxHealth;
		if (fillAmount > 1)
		{
			fillAmount = 1.0f;
		}

		healthBarImage.fillAmount = fillAmount;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out Damager weapon))
		{
			if (weapon.alignmnent == Damager.Alignment.Player || weapon.alignmnent == Damager.Alignment.Environment)
			{
				DecreaseHealth(weapon.damageValue);

				if (EnemyHealthBar)
					UpdateHealthBar();

				if (currentHealth == 0)
				{
					Destroy(this.gameObject); //If this enemy reaches 0 health, they are straight up destroyed. 
											  //If you want something fancy like an animation or the like, you can try to implement it here
					Destroy(healthBarImage.gameObject);

					WM.livin = false;
					WM.numberKilled++;
				}
			}
		}
	}
}
