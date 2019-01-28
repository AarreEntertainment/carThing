using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inCollision : MonoBehaviour {

	public void Update(){
		if (Physics.Raycast (transform.position, -transform.up, transform.lossyScale.y *0.55f))
			transform.parent.GetComponent<suspension> ().grounded = true;
		else
			transform.parent.GetComponent<suspension> ().grounded = false;
	}
}
