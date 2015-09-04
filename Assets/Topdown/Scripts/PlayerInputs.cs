using UnityEngine;
using System.Collections;

public class PlayerInputs : MonoBehaviour {

	// Vi bör skapa ett privat decimaltal som håller koll på hur fort man skjuter. Ett värde av 0.2f fungerade bra. 

	public GameObject fireBall;
	public GameObject frostBall;

	private float shootTimer = 0f;
	private int activeWeapon = 0; // Genomgående i det här scriptet har någon fifflat med värdet på denna variabel.

	private Animator anim;

	private float vertical;
	private float horizontal;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");
		transform.Translate(new Vector3(0,0,0)*Time.deltaTime, Camera.main.transform); // Varför kan karaktären inte röra sig i X- och Y-led när vi har tagit fram två variabler för det två rader upp?
		
		float speed = Mathf.Abs(vertical) + Mathf.Abs(horizontal);
		anim.SetFloat("Speed",speed);


		var diff = DirectionVector ();
		float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rot_z + 90);

		shootTimer += Time.deltaTime;
		if(Input.GetButton("Fire1") && shootTimer > 100f){ // Vi bör ändra så man inte bara kan skjuta var 100e sekund.
			// Eld var det här!
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			activeWeapon = 0;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && GetComponent<PlayerVariables>().level>1){
			activeWeapon = 0;
		}
	}

	Vector3 DirectionVector () {
		Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		diff.Normalize ();
		// Den här funktionen gör ingenting om den inte skickar tillbaka värdet som deklareras här ovan.
	}

	void Fire () {
		shootTimer = 0f;

		GameObject projectileInstance;

		if(activeWeapon==0){
			projectileInstance = (GameObject)Instantiate(fireBall,transform.position,transform.rotation);
			projectileInstance.GetComponent<Rigidbody2D>().velocity = -projectileInstance.transform.up * 2f;
			projectileInstance.GetComponent<Projectile>().damage *= GetComponent<PlayerVariables>().level;
		}
		else {
			projectileInstance = (GameObject)Instantiate(frostBall,transform.position,transform.rotation);
			projectileInstance.GetComponent<Rigidbody2D>().velocity = -projectileInstance.transform.up * 1.5f;
			projectileInstance.GetComponent<Projectile>().damage *= GetComponent<PlayerVariables>().level;
		}

		// Vi bör ta bort de instantierade projektilerna ca tre sekunder efter de skapas. Hade inte Destroy ett tillägg för just detta?

	}
}


