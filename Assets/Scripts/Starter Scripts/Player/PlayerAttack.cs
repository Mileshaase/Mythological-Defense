using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerAttack : MonoBehaviour
{
	public SwordManagement SM;


	[Header("References")]

	[Tooltip("There are separate animators for the weapon. If you have a player attack animation from a sprite sheet, make this true. If your attack animation is the weapon itself moving (separate from the player) this can be false.")]
	public bool UsePlayerAttackAnimations = false;
	public Animator anim;
	public Rigidbody2D rb;
	public PlayerMovement playerMoveScript;

	[Header("Player Weapons")]
	[Tooltip("This is the list of all the weapons that your player uses")]
	public GameObject[] weaponList;
	[Tooltip("This is the current weapon that the player is using")]
	public Damager weapon;
	[Tooltip("The coolDown before you can attack again")]
	public float coolDown = 0.4f;

	[Header("Audio")]
	public PlayerAudio playerAudio;

	private bool canAttack = true;

	private void Start()
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		playerMoveScript = GetComponent<PlayerMovement>();
		playerAudio = GetComponent<PlayerAudio>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			Attack();
			if (playerAudio && !playerAudio.AttackSource.isPlaying && playerAudio.AttackSource.clip != null)
			{
				playerAudio.AttackSource.Play();
			}
		}
		else
		{
			StopAttack();
		}
	}

	public void Attack()
	{
		//This is where the weapon is rotated in the right direction that you are facing
		if (weapon && canAttack)
		{
			if (UsePlayerAttackAnimations)
				playerMoveScript.TriggerPlayerAttackAnimation();

			if (weapon is ProjectileWeapon)
				weapon.WeaponStart(this.transform, playerMoveScript.GetLastLookDirection(), rb.velocity);
			else
				weapon.WeaponStart(this.transform, playerMoveScript.GetLastLookDirection());

			StartCoroutine(CoolDown());
		}
	}

	public void StopAttack()
	{
		if (weapon)
		{
			weapon.WeaponFinished();
		}
	}

	public void switchWeaponAtIndex(int index)
	{
		//Makes sure that if the index exists, then a switch will occur
		if (index < weaponList.Length && weaponList[index])
		{
			//Deactivate current weapon
			weapon.gameObject.SetActive(false);

			//Switch weapon to index then activate
			weapon = weaponList[index].GetComponent<Damager>();
			weapon.gameObject.SetActive(true);
		}

	}

	private IEnumerator CoolDown()
	{
		canAttack = false;
		yield return new WaitForSeconds(coolDown);
		canAttack = true;
	}

	private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene1" || scene.name == "GameScene2" || scene.name == "GameScene3" || scene.name == "GameScene4")
        {
			SM = GameObject.FindGameObjectWithTag("Manager").GetComponent<SwordManagement>();
			SM.SwordIcons = GameObject.FindGameObjectsWithTag("SwordIcons");
            weaponList = GameObject.FindGameObjectsWithTag("Sword");
			weapon = weaponList[SM.SwordLevel].GetComponent<Damager>();
			for (int i = 0; i < weaponList.Length; i++)
			{
				if (i != SM.SwordLevel)
				{
					weaponList[i].SetActive(false);
				}
			}
        }
		if(scene.name == "GameScene2" || scene.name == "GameScene3" || scene.name == "GameScene4")
		{
			SM = GameObject.FindGameObjectWithTag("Manager").GetComponent<SwordManagement>();
			SM.SwordIcons = GameObject.FindGameObjectsWithTag("SwordIcons");
			SM.upgradeSword();
		}
    }
}