using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorController : MonoBehaviour {
	public GameObject piece;
	public float timeBetween;
	public int capacity;

	private float tick;
	private float err = 0.1f;
	private List<GameObject> pieces;


	bool floatDivisible(float a, float b, float err){
		if( (a/b - (int) (a/b)) % 1 < err)
			return true;
		return false;
	}
	// Use this for initialization
	void Start () {
		pieces = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(pieces.Count >= capacity)
			return;

		float time = Time.timeSinceLevelLoad;
		if(time > tick)
			tick = time;

		if(floatDivisible(tick, timeBetween, err)){
			tick++;
			GameObject go = (GameObject) Instantiate(piece, 
			                transform.position + Vector3.up * 6,
			                Quaternion.identity);
			go.transform.parent = this.transform.parent.transform;
			//go.rigidbody.mass = 0.00001f;
			pieces.Add(go);

		}
	}
}
