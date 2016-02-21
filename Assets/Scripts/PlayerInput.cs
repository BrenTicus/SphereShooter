using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	new public GameObject camera;
	new public AudioSource audio;

	public GameObject bulletPrefab;
	public GameObject bulletSpawn;
	public float TurnSpeed;
	public float RollSpeed;
	public float MaxMoveSpeed;
	public float AccelRate;
	public float DecelRate;

	float speed = 0;
	float timeOfFire = 0;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float fire = Input.GetAxis("Fire1");

		Vector3 turnVector = Vector3.Cross(transform.right, Vector3.up);
		turnVector.Normalize();

		Debug.DrawLine(transform.position, transform.position + 2 * turnVector);

		if (v > 0) speed = Mathf.Lerp(speed, MaxMoveSpeed, AccelRate);
		else if (v < 0) speed = Mathf.Lerp(speed, -MaxMoveSpeed, AccelRate);
		else speed = Mathf.Lerp(speed, 0, DecelRate);

		transform.Translate(turnVector * speed, Space.World);
		transform.Rotate(new Vector3(speed * RollSpeed, 0, 0), Space.Self);
		transform.Rotate(new Vector3(0, h * TurnSpeed, 0), Space.World);

		if (Time.time - timeOfFire > 1 && fire != 0)
		{
			Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
			timeOfFire = Time.time;
			audio.Play();
		}
	}
}
