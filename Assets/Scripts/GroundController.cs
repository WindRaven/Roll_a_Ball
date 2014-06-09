using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {
	public float speed;
	public float playerMult;
	public float textureMult;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2  offset = renderer.material.GetTextureOffset("_MainTex");
		float yOffset = offset.y + speed * textureMult * Time.deltaTime;
		yOffset = yOffset % 1;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, yOffset + speed * textureMult * Time.deltaTime));

	}

	void OnCollisionStay(Collision collision){

		Rigidbody rb = collision.rigidbody;
		GameObject other = collision.gameObject;
		other.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
		/*
		float mlt = 1;
		if(other.tag == "Player"){
			mlt = playerMult;
		}

		ContactPoint contact = collision.contacts[0];
		rb.AddForceAtPosition(Vector3.forward * force * mlt
		                      * Time.deltaTime, contact.point,
		                      ForceMode.VelocityChange);
		*/
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal * 10, Color.red);
			//rb.AddForceAtPosition(Vector3.forward * force * mlt
			  //                    * Time.deltaTime, contact.point,
			    //                  ForceMode.VelocityChange);

		}//*/

	}

}
