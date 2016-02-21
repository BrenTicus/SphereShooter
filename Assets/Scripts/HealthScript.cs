using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public GameObject boom;
	public float maxHealth;

	public float health { get; set; }

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	public void takeDamage(int damage)
	{
		health -= damage;
		this.GetComponent<Renderer>().material.color = new Color(1, health / maxHealth, health / maxHealth);
		if (health <= 0)
		{
			Destroy(gameObject);
			Instantiate(boom, transform.position, transform.rotation);
		}
	}
}
