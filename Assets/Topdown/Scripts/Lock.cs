using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour {

	public GameObject target;

	void OnTriggerEnter2D (Collider2D other) {
		if(other.CompareTag("Player")) { 
			target.SetActive(false);
		}

		// Vi bör se till att det bara är objekt med taggen "Player" som kan reagerar här. 
		// Är det taggen Player bör vi sätta target till Active false.

	}
}
