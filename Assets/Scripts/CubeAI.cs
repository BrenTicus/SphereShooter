using UnityEngine;
using System.Collections;

public class CubeAI : MonoBehaviour {
	public float speed;
	
	GameObject goal;
	Vector3 goalPosition;
	Vector3 direction;
	int layerMask;


	// Use this for initialization
	void Start () {
		goal = GameObject.FindGameObjectWithTag("Goal");
		layerMask = ~(1 << 2); // Cast against everything except layer 2, IgnoreRaycast

		calculateNewGoal(transform.position, goal.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		// If the goal died, exit.
		if (goal == null) return;

		// If it's at the intermediate goal, find a new one.

		// Move towards the goal
		direction = goalPosition - transform.position;
		if (Vector3.Magnitude(direction) < 1.5f)
		{
			calculateNewGoal(transform.position, goal.transform.position);
			direction = goalPosition - transform.position;
		}

		direction.Normalize();

		// This is nice to see where they're going in scene view.
		Debug.DrawLine(transform.position, goalPosition, Color.red);
	}

	void FixedUpdate()
	{
		Quaternion look;
		direction.y = 0;
		transform.position += direction * speed;

		look = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, look, 0.1f);
	}

	void calculateNewGoal(Vector3 from, Vector3 to)
	{
		RaycastHit hitInfo;
		// If something's in the way, find a way around it.
		if (Physics.Raycast(from, to - from, out hitInfo, Vector3.Magnitude(to - from), layerMask))
		{
			// Find a vector pointing orthogonally to the main vector and set your new goal position to a point 
			// midway between yourself and the original goal, but in the direction of the orthogonal vector.
			Vector3 ortho = Vector3.Normalize(Vector3.Cross(to - from, Vector3.up));
			goalPosition = hitInfo.point + ortho * 5;
			// Check to make sure this works.
			calculateNewGoal(from, goalPosition);
		}
		else
		{
			goalPosition = to;
		}
	}

}
