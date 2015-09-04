using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int damage = 10;
	private float timer = 0;

	void Update() {
		if (this.timer > 3) {
			Destroy(gameObject);
		}
		this.timer += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")){
			//Do nothing... Or!?
		}

		if(other.GetComponent<Enemy>() != null){

			// Kolliderar detta objekt med ett objekt som har komponenten Enemy så bör vi kalla på dess Harm(float) funktion.
			other.GetComponent<Enemy>().Harm(this.damage);

		}

		Destroy (gameObject);
	}
}
