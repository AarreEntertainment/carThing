using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suspension : MonoBehaviour {

	public Rigidbody rb;
	car script;

	public float springforce;
	public float damperforce;
	public float springcontant;
	public float damperconstant;
	public float restlength;

	public bool grounded;

	float previouslength;
	float currentlength;
	float springvelocity;

	// Use this for initialization
	void Start () {
		script = transform.parent.GetComponent<car> ();
	}
	void Update(){
		
	}
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, restlength + script.wheelRadius)) {
	
			previouslength = currentlength;
			currentlength = restlength - (hit.distance - script.wheelRadius);
			springvelocity = (currentlength - previouslength) / Time.fixedDeltaTime;
			springforce = springcontant * currentlength;
			damperforce = damperconstant * springvelocity;
			transform.GetChild (0).localPosition = new Vector3 (0, 	currentlength, 0);
			rb.AddForceAtPosition (transform.up * (springforce + damperforce), transform.position);
		} 
			

	}
}
