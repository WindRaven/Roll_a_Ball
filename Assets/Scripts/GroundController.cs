using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal * 10, Color.red);
			collision.gameObject.rigidbody.AddForceAtPosition(new Vector3(0, 0, 2) * Time.deltaTime, contact.point, ForceMode.VelocityChange);
		}
		//collision.gameObject.transform.Translate( new Vector3(0, 0, 2) * Time.deltaTime, Space.World);
		//collision.gameObject.rigidbody.AddForce(new Vector3(0, 0, 10) * Time.deltaTime,ForceMode.VelocityChange);

	}
}
