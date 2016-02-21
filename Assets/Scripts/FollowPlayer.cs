using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public GameObject target;
	public float distance;
	public float height;
	public float rotationDamping;
	public float lerpSpeed;

	private Vector3 lastPosition;

	public void LateUpdate()
	{ 
		if (!target) return;

		Vector3 turnVector = Vector3.Cross(target.transform.right, Vector3.up);
		turnVector.Normalize();

		Vector3 nextPosition = target.transform.position - turnVector * distance;
        nextPosition = new Vector3(nextPosition.x, height, nextPosition.z);

		transform.position = Vector3.Slerp(transform.position, nextPosition, lerpSpeed);
		
		// Always look at the target
		transform.LookAt(target.transform);

	}
}
