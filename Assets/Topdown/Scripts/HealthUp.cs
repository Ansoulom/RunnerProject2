using UnityEngine;
using System.Collections;

public class HealthUp : MonoBehaviour {

	public float healthRestore;

	// Use this for initialization
	void Start () {
		this.healthRestore = 20;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Player")){
			//Move harm timer here so multiple enemies can harm player at the same time?
			
			// Leta upp komponenten PlayerVariables på det kolliderade objektet och kalla på funktionen Harm(float) med skadan som fienden gör.
			other.GetComponent<PlayerVariables>().health += this.healthRestore;
			if (other.GetComponent<PlayerVariables>().health > other.GetComponent<PlayerVariables>().maxHealth){
				other.GetComponent<PlayerVariables>().health = other.GetComponent<PlayerVariables>().maxHealth;
			}
			Destroy(this.gameObject);
		}
	}
}
