using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour {

	public GameObject frontLeft;
	public GameObject frontRight;
	public GameObject backLeft;
	public GameObject backRight;


	suspension fl;
	suspension fr;
	suspension bl;
	suspension br;

	float drag;
	float adrag;

	public float maxSpeed;

	public float wheelRadius;
	Rigidbody rb;
	public UnityEngine.UI.Text textobject;
	// Use this for initialization
	void Start () {
		fl = frontLeft.GetComponent<suspension> ();
		fr = frontRight.GetComponent<suspension> ();
		bl = backLeft.GetComponent<suspension> ();
		br = backRight.GetComponent<suspension> ();

		rb = GetComponent<Rigidbody> ();
		drag = rb.drag;
		adrag = rb.angularDrag;

	}
	bool wheelie = false;
	bool grounded = true;
	public float handbrake = 0;
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		
		frontLeft.transform.localRotation = Quaternion.Euler (0, 35*Input.GetAxis("Horizontal"), 0);
		frontRight.transform.localRotation =Quaternion.Euler (0, 35*Input.GetAxis("Horizontal"), 0);

		if (((fl.grounded && bl.grounded)&&(!fr.grounded&&!br.grounded))
		 ||
			((fr.grounded && br.grounded)&&(!fl.grounded&&!bl.grounded))
		) {
			rb.angularDrag = adrag * 4;
			grounded = true;
			wheelie = true;
		}
		else if (!fl.grounded && !fr.grounded && !bl.grounded && !br.grounded) {
			rb.drag = 0;
			rb.angularDrag = 0;
			grounded = false;
			wheelie = false;
			rb.AddForce (-transform.up, ForceMode.Force);
		} else {
			rb.drag = drag-(rb.angularVelocity.magnitude/10*(rb.velocity.magnitude/10));
			rb.angularDrag = adrag - (rb.angularVelocity.magnitude/5);
			grounded = true;
			wheelie = false;
		}
		float speedmodifier = rb.velocity.magnitude / (maxSpeed / drag);
		textobject.text = "Velocity:\n X: " + rb.velocity.x + 
			"\nY: " + rb.velocity.y + 
			"\nZ: " + rb.velocity.z + "\n" + 
			rb.velocity.magnitude + 
			"\n"+ rb.angularVelocity.magnitude +
			"\n Grounded: " + grounded.ToString ()+
			"\n Wheelie:"+ wheelie.ToString()+
			"\n SpeedModifier: "+ rb.velocity.magnitude/(maxSpeed/drag); 
		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0 && grounded) {
			float speed;
			if (Input.GetAxis ("Vertical") > 0)
				speed = maxSpeed*(0.08f+ speedmodifier*0.92f);
			else {
				speed = maxSpeed / 10;
			}
			

			rb.AddForce (transform.forward * Input.GetAxis ("Vertical") * speed, ForceMode.Acceleration);
		
				
		}

		if (handbrake <= 0 && Input.GetButtonDown ("Fire1"))
			handbrake = 1 * (rb.velocity.magnitude/30);
		if (handbrake > 0)
			handbrake -= Time.deltaTime;

		if (Mathf.Abs( Input.GetAxis ("Horizontal")) > 0 && grounded &&rb.velocity.magnitude>1f){
			if(fl.grounded || fr.grounded)
				rb.AddTorque (transform.up * 25000 * Input.GetAxis ("Horizontal"));

			if (handbrake <= 0) {
				rb.angularDrag = adrag * 2.5f;
				rb.drag = drag * 1.1f;
			} else {
				rb.drag = drag * 1.1f;
			}
		}


		
				

	}

	//physics

	//audiovisual

}
