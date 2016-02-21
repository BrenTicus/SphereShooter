using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {
	public float maxHealth;
	public float health;

	public GameObject boom;
	public GameObject towerContainer;
	public GameObject tower;
	public GameObject enemyContainer;
	public GameObject enemy;
	public int numTowers; // Multiply by 4, remember some may end up in each other.
	public int nearTower; // Closest possible tower
	public int farTower; // Furthest possible tower
	public int enemyCount;
	public int enemyExtraHealth;
	public int round;

	// Use this for initialization
	void Start () {
		health = maxHealth;

		generateObstacles(numTowers);
	}
	
	// Update is called once per frame
	void Update () {
		// Killed everything, round over.
		if (enemyContainer.transform.childCount == 0)
		{
			maxHealth++;
			setHealth(health + 1);
			round++;
			this.GetComponent<Renderer>().material.color = new Color(1, health / maxHealth, health / maxHealth);
			spawnEnemies(enemyCount);
			if (round % 8 == 0) { enemyExtraHealth++; enemyCount /= 2; }
			if (round % 2 == 0) enemyCount++;
		}
	}

	// Kill the enemy. Hurt self.
	void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			HealthScript enemyHealth = collision.gameObject.GetComponent<HealthScript>();
			setHealth(health - enemyHealth.health);
			//This is just an arbitrarily large number to trigger the usual death stuff.
			enemyHealth.takeDamage(5000);
			if (health <= 0)
			{
				Instantiate(boom, transform.position, transform.rotation);
				Destroy(gameObject);
			}
		}
	}

	// Generate 4 * numTowers towers around the map to provide obstacles.
	void generateObstacles(int numObstacles)
	{
		for(int i = 0; i < numObstacles; i++)
		{
			float x = 0, z = 0;
			// Get a random position as long as said position isn't too close to the goal or too far away.
			do
			{
				x = Random.Range(0, farTower);
				z = Random.Range(0, farTower);
			} while (Vector3.Magnitude(new Vector3(x, 0, z)) < nearTower);
			// Generate 4 towers, mirrored around the map.
			GameObject newTower = (GameObject)Instantiate(tower, new Vector3(transform.position.x + x, 2.5f, transform.position.z + z), Quaternion.identity);
			newTower.transform.parent = towerContainer.transform;
			newTower = (GameObject)Instantiate(tower, new Vector3(transform.position.x - x, 2.5f, transform.position.z + z), Quaternion.identity);
			newTower.transform.parent = towerContainer.transform;
			newTower = (GameObject)Instantiate(tower, new Vector3(transform.position.x + x, 2.5f, transform.position.z - z), Quaternion.identity);
			newTower.transform.parent = towerContainer.transform;
			newTower = (GameObject)Instantiate(tower, new Vector3(transform.position.x - x, 2.5f, transform.position.z - z), Quaternion.identity);
			newTower.transform.parent = towerContainer.transform;
		}
	}

	void spawnEnemies(int number)
	{
		float x = 0, z = 0;
		// Get a random position outside of the tower range.
		do
		{
			x = Random.Range(farTower, 2000);
			z = Random.Range(farTower, 2000);
		} while (Vector3.Magnitude(new Vector3(x, 0, z)) > farTower * 2);
		if (x % 2 == 0) x *= -1;
		if (z % 2 == 0) z *= -1;
		float v = 0, w = 0; 
		// Get a random position somewhere around the point.
		for (int i = 1; i <= number; i++)
		{
			v = Random.Range(x - 5, x + 5);
			w = Random.Range(z - 5, z + 5);

			GameObject spawn = (GameObject)Instantiate(enemy, new Vector3(transform.position.x + v, 1.0f, transform.position.z + w), Quaternion.identity);
			spawn.transform.parent = enemyContainer.transform;
			spawn.GetComponent<HealthScript>().maxHealth += enemyExtraHealth;
		}
	}

	void setHealth(float health)
	{
		this.health = Mathf.Min(health, maxHealth);
		this.GetComponent<Renderer>().material.color = new Color(1, health / maxHealth, health / maxHealth);
	}
}
