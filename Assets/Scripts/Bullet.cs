using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed;
	public int damage;

	float timeAlive = 0;

	// Use this for initialization
	void Start () {
		Vector3 direction = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
		GetComponent<Rigidbody>().AddForce(direction * speed);
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive += Time.deltaTime;
		if (timeAlive > 5) Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.SendMessage("takeDamage", damage);
			GameObject.Find("Points").SendMessage("UpdateScore");
		}
		Destroy(gameObject);
	}
}
