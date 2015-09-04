using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Exit : MonoBehaviour {

	public Image fadeToBlackSprite;
	private bool doFade = true;

	IEnumerator OnTriggerEnter2D (Collider2D other) {
		if(other.CompareTag("Player")) {
			other.GetComponent<PlayerInputs>().enabled = false;

			yield return new WaitForSeconds(0.5f);

			if(doFade) {
				fadeToBlackSprite.gameObject.SetActive(true);
			}
			// Ska vi sätta igång faden? Det finns en bool för det.
			//TODO FIX STUFF
			// fadeToBlackSprite.gameObject bör aktiveras här.

			yield return new WaitForSeconds(2f);
			
			Application.LoadLevel (0);
		}
	}

	void Update () {
		if(doFade){
			fadeToBlackSprite.color = Color.Lerp(fadeToBlackSprite.color, new Color(0,0,0,1f),Time.deltaTime);
		}
	}
}
