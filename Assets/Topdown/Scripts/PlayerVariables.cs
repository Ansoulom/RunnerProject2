using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerVariables : MonoBehaviour {

	public float health = 100f;
	public float maxHealth;
	public float stamina, maxStamina;
	public float damageTimer = 1f;
	public Slider healthSlider, staminaSlider;

	public int level = 2;
	public int experience = 0;

	void Start () {
		healthSlider = GameObject.Find("HealthSliderUI").GetComponent<Slider>();
		this.staminaSlider = GameObject.Find ("StaminaSliderUI").GetComponent<Slider> ();
		this.maxStamina = 100f;
		this.maxHealth = 100;
		this.stamina = this.maxStamina;
	}
	
	void Update () {
		// damageTimer bör öka med tiden som gått från senaste uppdate-loopen. Tiden räknas ut med Time.deltaTime;
		this.damageTimer += Time.deltaTime;

		healthSlider.value = this.health;
			this.staminaSlider.value = this.stamina;
		if(experience > level * level * 100){

			// När experience når över den mattematiska formeln i if-satsen bör vi öka level.
			this.level++;
		
		}
	}

	public void Harm(float dmg){
		if (!this.gameObject.GetComponent<PlayerInputs> ().boosting) {
			if (this.damageTimer > 1) {
				this.health -= dmg;
				this.damageTimer = 0;
			}
			// Om damageTimer är större än en sekund bör vi sänka health med damage. Vi bör även sätta damageTimer till 0f för att nollställa timern.

			if (this.health < 0) {
				StartCoroutine (Die ());
			}
		}
			// Om health är mindre än 1f så bör vi starta funktionen Die(). Det kan bara göras med StartCoroutine eftersom Die() är en IEnumerator.

	}

	IEnumerator Die(){
		GetComponent<PlayerInputs>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Animator>().SetTrigger("Die");

		yield return new WaitForSeconds(2f);

		// Här bör en ny level laddas eller en icke implementerad Respawn() funktion kallas på. 
		Application.LoadLevel (0);
		//TODO FIX THIS

	}
}
