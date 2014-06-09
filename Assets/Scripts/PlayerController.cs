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
	private float lastJumpTouchTime;
	void SetCountText(){
		countText.text = "Count :" + count.ToString();
		winText.gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		count = 0;
		SetCountText();
		//transform.position = new Vector3( 0.0f, 0.5f, 0.0f);
	}

	void Jump(){
		if(nrOfJumps <= 0)
			return;
		rigidbody.AddForce(Vector3.up * jumpForce);
		nrOfJumps--;
	}
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown( "space") ){
			Jump();
		}
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		if (Input.touchCount > 0){
			moveHorizontal = Input.touches[0].deltaPosition.x * 0.5f;
			moveVertical = Input.touches[0].deltaPosition.y * 0.5f;

			if(Input.touchCount > 1){
				float tTime = Time.realtimeSinceStartup;
				print (tTime - lastJumpTouchTime);
				if((tTime - lastJumpTouchTime) > 0.4){
					Jump();
					lastJumpTouchTime = tTime;
				}
			}

		}
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
	}

	/*void onCollisionStay(Collision collision){
		if(collision.gameObject.tag == "Ground"){
			transform.Translate(Vector3.forward * 5 * Time.deltaTime, Space.World);
		}
	}*/
}
	