using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private Transform player;
	private Animator anim;

	public float speed = 20f;
	public float damage = 25f;
	public int health = 100;
	private int startHealth = 100;

	public GameObject staminaRestore;
	public GameObject healthRestore;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator>();


		// När skelettet dör använder vi dess health som experience-points för spelaren. Men när skelettet har dött är ju detta 0...! 
		// Vi borde därför sätta den redan deklarerade variabeln för start-hälsan till att vara det health är i Start-funktionen. 
	}
	
	void Update () {

		float distance = Vector3.Distance(player.position, transform.position);

		if(distance<0.3f){
			//Stand still
		}
		else if(distance<3.4f){
			var diff = DirectionVector ();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

			transform.Translate(Vector3.down*speed*Time.deltaTime); // Vilken hastighet ska detta skelett skutta fram med?

			anim.SetFloat("Speed",1f);
		}
		else {
			anim.SetFloat("Speed",0f);
		}
	}

	Vector3 DirectionVector () {
		Vector3 diff = player.position - transform.position;
		diff.Normalize ();
		// Den här funktionen gör ingenting om den inte skickar tillbaka värdet som deklareras här ovan.
		return diff;
	}

	public void Harm(int dmg){

		// health bör minska med skadan som denna funktion tar. 
		this.health -= dmg;
		
		if(health<1f){
			int score = PlayerPrefs.GetInt("SP1GameScore");
			// score är variabeln som spelet sparat i minnet. Öka detta med lämpligt värde innan det sätts i minnet igen.
			score++;
			PlayerPrefs.SetInt("SP1GameScore", score);

			player.gameObject.GetComponent<PlayerVariables>().experience += startHealth;

			StartCoroutine(Die ());
			// Att bara ta bort det här objektet går bra när skelettet dör. Men annars kanske man köra StartCoroutine(Die ()); och hitta på något annat snyggt.
		}
	}

	IEnumerator Die() {
		yield return new WaitForSeconds(0);
		Destroy (this.gameObject);
		if (Random.value < 0.5f) {
			GameObject staminaInstance = (GameObject)Instantiate(this.staminaRestore,transform.position,transform.rotation);
		}
		else if (Random.value <= 1) {
			GameObject healthInstance = (GameObject)Instantiate(this.healthRestore,transform.position,transform.rotation);
		}

	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Player")){
			//Move harm timer here so multiple enemies can harm player at the same time?

			// Leta upp komponenten PlayerVariables på det kolliderade objektet och kalla på funktionen Harm(float) med skadan som fienden gör.
			other.GetComponent<PlayerVariables>().Harm(damage);
		}
	}
}
