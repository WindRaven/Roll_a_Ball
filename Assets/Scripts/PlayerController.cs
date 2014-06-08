using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float jumpForce;
	public int maxJumps;
	private int nrOfJumps;
	private int count;
	public GUIText countText;
	public GUIText winText;
	void SetCountText(){
		countText.text = "Count:" + count.ToString();
		winText.gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		count = 0;
		SetCountText();
		//transform.position = new Vector3( 0.0f, 0.5f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown( "space") && nrOfJumps > 0){
			rigidbody.AddForce(Vector3.up * jumpForce);
			nrOfJumps--;
		}
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rigidbody.AddForce(movement * speed * Time.deltaTime);

	}

	void OnTriggerEnter(Collider other) {
		if( other.gameObject.tag == "PickUp"){
			other.gameObject.SetActive(false);
			count++;
			SetCountText();
		}
		if(count >= 9){
			winText.gameObject.SetActive(true);
		}
	}

	void OnCollisionEnter(Collision other){
		if( other.gameObject.tag == "Ground"){
			nrOfJumps = maxJumps;
		}
		foreach (ContactPoint contact in other.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
			
	
	}
}
	