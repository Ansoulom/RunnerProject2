using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Exit : MonoBehaviour {

	public Image fadeToBlackSprite;
	private bool doFade = false;

	IEnumerator OnTriggerEnter2D (Collider2D other) {
		if(other.CompareTag("Player")) {
			other.GetComponent<PlayerInputs>().enabled = false;

			yield return new WaitForSeconds(0.5f);
			// Ska vi sätta igång faden? Det finns en bool för det.
			// fadeToBlackSprite.gameObject bör aktiveras här.
			doFade = true;
			fadeToBlackSprite.color = new Color(0, 0, 0, 0);
			fadeToBlackSprite.gameObject.SetActive(true);

			yield return new WaitForSeconds(2f);
			
			Application.LoadLevel (1);
		}
	}

	void Update () {
		if(doFade){
			fadeToBlackSprite.color = Color.Lerp(fadeToBlackSprite.color, new Color(0,0,0,1f),Time.deltaTime*2);
		}
	}
}
