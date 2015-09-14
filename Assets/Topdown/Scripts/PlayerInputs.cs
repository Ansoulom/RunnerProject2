using UnityEngine;
using System.Collections;

public class PlayerInputs : MonoBehaviour {

	// Vi bör skapa ett privat decimaltal som håller koll på hur fort man skjuter. Ett värde av 0.2f fungerade bra. 
	private float shootSpeed = 0.5f;
	private float speed;
	
	public bool boosting;
	private Vector2 boostVelocity;
	private float boostSpeed;
	private float boostTime;
	private float boostTimer;
	private float boostCost;
	private int boostDamage;
	private float rechargeRate;

	public GameObject fireBall;
	public GameObject frostBall;

	private float shootTimer = 0f;
	private float shootCost;
	private int activeWeapon = 0; // Genomgående i det här scriptet har någon fifflat med värdet på denna variabel.

	private Animator anim;

	private float vertical;
	private float horizontal;

	void Start () {
		anim = GetComponent<Animator>();
		this.speed = 1.5f;
		this.boosting = false;
		this.boostSpeed = 8;
		this.boostTime = 0.3f;
		this.boostCost = 40;
		this.rechargeRate = 3;
		this.shootCost = 5f;
		this.boostDamage = 10;
	}

	/*void FixedUpdate() {
		
	}*/

	void Update () {
		if (!this.boosting) {
			horizontal = this.speed * Input.GetAxis ("Horizontal");
			vertical = this.speed * Input.GetAxis ("Vertical");
		} else {
			horizontal = boostVelocity.x;
			vertical = boostVelocity.y;
			boostTimer += Time.deltaTime;
			if (this.boostTimer >= boostTime) {
				this.boosting = false;
				this.boostTimer = 0;
				GameObject.FindGameObjectWithTag("Pointer").GetComponent<SpriteRenderer>().enabled = true;
				GameObject.FindGameObjectWithTag ("Pointer").transform.rotation = this.transform.rotation;
				GameObject.FindGameObjectWithTag ("Pointer").transform.position = this.transform.position;
			}
		}

		transform.Translate(new Vector3(horizontal,vertical,0)*Time.deltaTime, Camera.main.transform); // Varför kan karaktären inte röra sig i X- och Y-led när vi har tagit fram två variabler för det två rader upp?
		if (this.transform.position.y < -1.8f + GameObject.FindGameObjectWithTag("MainCamera").transform.position.y) {
			this.transform.position = new Vector3(this.transform.position.x, -1.8f + GameObject.FindGameObjectWithTag("MainCamera").transform.position.y, this.transform.position.z);
		}
		if (this.transform.position.x < -3.35f) {
			this.transform.position = new Vector3(-3.35f, this.transform.position.y, this.transform.position.z);
		}
		else if (this.transform.position.x > 3.35f) {
			this.transform.position = new Vector3(3.35f, this.transform.position.y, this.transform.position.z);
		}

		float speed = Mathf.Abs(vertical) + Mathf.Abs(horizontal);
		anim.SetFloat("Speed",speed);


		var diff = DirectionVector ();
		float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rot_z + 90);
		if (!this.boosting) {
			GameObject.FindGameObjectWithTag ("Pointer").transform.rotation = this.transform.rotation;
			GameObject.FindGameObjectWithTag ("Pointer").transform.position = this.transform.position;
		}

		shootTimer += Time.deltaTime;
		if(Input.GetButton("Fire1") && shootTimer > shootSpeed && this.gameObject.GetComponent<PlayerVariables>().stamina >= this.shootCost){ // Vi bör ändra så man inte bara kan skjuta var 100e sekund.
			this.gameObject.GetComponent<PlayerVariables>().stamina -= this.shootCost;
			Fire();
		}
		if (Input.GetButton("Fire2") && boostTimer == 0 && this.gameObject.GetComponent<PlayerVariables>().stamina >= this.boostCost) {
			this.boosting = true;
			print (diff.magnitude);
			this.boostVelocity = new Vector2(diff.x * this.boostSpeed, diff.y * this.boostSpeed);
			this.boostTimer = 0;
			this.gameObject.GetComponent<PlayerVariables>().stamina -= this.boostCost;
			GameObject.FindGameObjectWithTag("Pointer").GetComponent<SpriteRenderer>().enabled = false;
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			activeWeapon = 0;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && GetComponent<PlayerVariables>().level>1){
			activeWeapon = 1;
		}
		this.gameObject.GetComponent<PlayerVariables> ().stamina += this.rechargeRate * Time.deltaTime;
	}

	Vector3 DirectionVector () {
		Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		if (diff.y < 0)
			diff.y = 0;
		diff.z = 0;
		diff.Normalize ();
		// Den här funktionen gör ingenting om den inte skickar tillbaka värdet som deklareras här ovan.
		return diff;
	}

	void Fire () {
		shootTimer = 0f;

		GameObject projectileInstance;

		if(activeWeapon==0){
			projectileInstance = (GameObject)Instantiate(fireBall,transform.position,transform.rotation);
			projectileInstance.GetComponent<Rigidbody2D>().velocity = -projectileInstance.transform.up * 5f;
			projectileInstance.GetComponent<Projectile>().damage *= GetComponent<PlayerVariables>().level;
		}
		else {
			projectileInstance = (GameObject)Instantiate(frostBall,transform.position,transform.rotation);
			projectileInstance.GetComponent<Rigidbody2D>().velocity = -projectileInstance.transform.up * 1.5f;
			projectileInstance.GetComponent<Projectile>().damage *= GetComponent<PlayerVariables>().level;
		}

		// Vi bör ta bort de instantierade projektilerna ca tre sekunder efter de skapas. Hade inte Destroy ett tillägg för just detta?

	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Enemy") && this.boosting){
			//Move harm timer here so multiple enemies can harm player at the same time?
			
			// Leta upp komponenten PlayerVariables på det kolliderade objektet och kalla på funktionen Harm(float) med skadan som fienden gör.
			other.GetComponent<Enemy>().Harm(this.boostDamage);
		}
	}
}


