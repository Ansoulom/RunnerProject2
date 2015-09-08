using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	void LateUpdate () {
		transform.position = new Vector3 (0, target.position.y, -10);
		//transform.position = Vector3.Lerp(transform.position, new Vector3(0, target.position.y+1f, transform.position.z), Time.deltaTime*7f);
	}
}
