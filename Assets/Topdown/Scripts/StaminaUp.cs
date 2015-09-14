using UnityEngine;
using System.Collections;

public class StaminaUp : MonoBehaviour {

	public float staminaRestore;

	// Use this for initialization
	void Start () {
		this.staminaRestore = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Player")){
			//Move harm timer here so multiple enemies can harm player at the same time?
			
			// Leta upp komponenten PlayerVariables på det kolliderade objektet och kalla på funktionen Harm(float) med skadan som fienden gör.
			other.GetComponent<PlayerVariables>().stamina += this.staminaRestore;
			if (other.GetComponent<PlayerVariables>().stamina > other.GetComponent<PlayerVariables>().maxStamina){
				other.GetComponent<PlayerVariables>().stamina = other.GetComponent<PlayerVariables>().maxStamina;
			}
			Destroy(this.gameObject);
		}
	}
}
