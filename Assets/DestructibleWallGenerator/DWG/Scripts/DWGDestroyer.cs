using UnityEngine;
using System.Collections;

public class DWGDestroyer : MonoBehaviour {

	public float radius = 2;
	public float force = 50f;
	
	void OnCollisionEnter(Collision col){
			ExplodeForce();
			Destroy(GetComponent<DWGDestroyer>());
	}
	
	// Explode force by radius only if a destructible tag is found
	void ExplodeForce(){	
		Vector3 explodePos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explodePos, radius); 
		foreach (Collider hit in colliders){
			if(hit.collider.tag == "Destructible"){
				if(hit.rigidbody){
					hit.rigidbody.isKinematic = false; 
					hit.rigidbody.AddExplosionForce(force, explodePos,radius);
				}
			}
		}
	}
}
