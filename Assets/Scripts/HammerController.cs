using UnityEngine;
using System.Collections;

public class HammerController : MonoBehaviour {
	public float rotSpeed;
	public bool toRight;
	private int direction;
	public GameObject holdPoint;
	// Use this for initialization
	void Start () {
		if(toRight)
			direction = 1;
		else
			direction = -1;
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(holdPoint.transform.position, 
		                       new Vector3( 0, 0, 1) , 
		                       direction * rotSpeed * Time.deltaTime);
		Vector3 eAngles = transform.eulerAngles; 
		if(eAngles.z > 120 && eAngles.z < 180){ 
			//transform.eulerAngles = new Vector3(eAngles.x, eAngles.y, 119.99f);

			direction = -1;
		}

	}

	void OnCollisionEnter(Collision other){
		//if( other.gameObject.tag == "Ground"){*/
			direction = 1;
		//}
		if( other.gameObject.tag == "Player" && other.relativeVelocity.magnitude > 1){
			other.gameObject.rigidbody.AddExplosionForce( 1000, other.rigidbody.centerOfMass, 50);
		}
	}
}
