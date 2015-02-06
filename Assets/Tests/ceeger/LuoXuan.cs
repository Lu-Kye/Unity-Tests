using UnityEngine;
using System.Collections;

public class LuoXuan : MonoBehaviour {
	
	public Vector3 target = Vector3.zero;
	public  float yForce = 0.05f;
	
	private int radius = 90;
	private float xForce = 0.5f;
	

	void Update () {
		transform.RotateAround(target, Vector3.up, radius * Time.deltaTime);
	}
	
	void FixedUpdate() {
		rigidbody.AddRelativeForce(xForce, yForce, 0);
	}
}
