using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject objectToSpawn;

	public float spawnRate = 2f;

	// Skapar vi en publik variabel, int, som t.ex. kan kallas för amountOfSkeletons så kan speldesignern sätta antalet skelett för varje EnemySpawner direkt i inspectorn.

	IEnumerator Start () {
		for(int i = 0; i<0; i++){ // Istället för i<0 kanske man kan använda den ovan deklarerade amountOfSkeletons.
			Spawn ();
			yield return new WaitForSeconds(100f); // Fanns det inte en variabel som hanterade rate of spawn?
		}
	}
	
	void Spawn(){
		GameObject instantiatedGameobject = (GameObject)Instantiate(objectToSpawn,transform.position, transform.rotation);
		int level = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVariables>().level;
		instantiatedGameobject.GetComponent<Enemy>().speed *= level;
		instantiatedGameobject.GetComponent<Enemy>().damage *= level;
		instantiatedGameobject.GetComponent<Enemy>().health *= level;
	}
}
