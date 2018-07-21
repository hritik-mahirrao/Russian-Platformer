using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {

	public Transform LookAt;
	public GameObject LeftBound;
	public GameObject RightBound;

	public float boundX = 2.0f;
	public float boundY = 1.0f;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LateUpdate() {

		Vector3 delta = Vector3.zero;

		// X Axis
		float dx = LookAt.position.x - transform.position.x;
			
		if(dx > boundX || dx < -boundX) {

			if(transform.position.x < LookAt.position.x) {
				delta.x = dx - boundX;
			} else {
				delta.x = dx + boundX;
			}
		}

		// Y Axis
		//float dy = LookAt.position.y - transform.position.y;
	
		//if(dy > boundY || dy < -boundY) {

		//    if(transform.position.y < LookAt.position.y) {
		//        delta.y = dy - boundY;
		//    } else {
		//        delta.y = dy + boundY;
		//    }
		//}

		// Move camera
		transform.position = transform.position + delta;

		// Clamp camera
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, LeftBound.transform.position.x, RightBound.transform.position.x),
			Mathf.Clamp(transform.position.y, LeftBound.transform.position.y, RightBound.transform.position.y),
			Mathf.Clamp(transform.position.z, LeftBound.transform.position.z, RightBound.transform.position.z)
		);

	}
}
