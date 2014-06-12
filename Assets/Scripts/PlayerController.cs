using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float jumpForce;
	public int maxJumps;
	public GUIText countText;
	public GUIText winText;
	public GameObject ground;

	private int nrOfJumps;
	private int count;
	private float lastJumpTouchTime;

	private Dictionary<int, GameObject> grounds;
	private Dictionary<int, int> dict;

	private GameObject GenerateGround(int index){
		GameObject go =  (GameObject) Instantiate(ground,
		                                Vector3.forward * index * 200,
		                                Quaternion.identity);
		print(go.transform.position.x);
		return go;

	}

	private void GenerateGrounds(int index){
		int max = int.MinValue, min = int.MaxValue;
		foreach( int key in grounds.Keys){
			if(key < min)
				min = key;
			if(key > max)
				max = key;
		}
		print (min.ToString() + index.ToString() + max.ToString());
		if(index == min){
			Destroy(grounds[max]);
			grounds.Remove(max);
			grounds.Add(min - 1, GenerateGround(min - 1));
		}
		if(index == max){

			countText.text = grounds[min].transform.position.z.ToString();
			GameObject go = grounds[min];

			grounds.Remove(min);
			Destroy(go);
			grounds.Add(max + 1, GenerateGround(max + 1));
		}

	}
	void SetCountText(){
		countText.text = "Count :" + count.ToString();
		winText.gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		grounds = new Dictionary<int, GameObject>();
		count = 0;
		SetCountText();

		/*Instantiate(ground,
		            Vector3.forward * (-1) * 200,
		            Quaternion.identity);
		*/
		GameObject go;
		go = GenerateGround((-1));
		grounds.Add(-1, go);
		GenerateGround(0);
		grounds.Add(0, go);
		GenerateGround(1);
		grounds.Add(1, go);
	

		//grounds.Add(1, GenerateGround(1));

	}

	void Jump(){
		if(nrOfJumps <= 0)
			return;
		rigidbody.AddForce(Vector3.up * jumpForce);
		nrOfJumps--;
	}
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown("space") ){
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
		if( other.gameObject.tag.Contains("Ground")){
			int index = (int) other.gameObject.transform.position.z / 200;
			print(other.gameObject.transform.position.z);
			GenerateGrounds(index);
		}
	}

	/*void onCollisionStay(Collision collision){
		if(collision.gameObject.tag == "Ground"){
			transform.Translate(Vector3.forward * 5 * Time.deltaTime, Space.World);
		}
	}*/
}
	