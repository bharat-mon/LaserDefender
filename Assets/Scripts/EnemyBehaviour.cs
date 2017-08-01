using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 250;
	public GameObject laser;
	public float projectileSpeed;
	public float fireRate = 0.5f;
	
	void Update () {
		ShootLaser();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser) {
			laser.Hit();
			health -= laser.GetDamage();
			if (health <= 0) {
				Destroy(gameObject);
			}
		}
	}
	
	void SpawnLaser () {
		Vector3 laserSpawn = this.transform.position + new Vector3(0, -0.7f, 0);
		GameObject enemyLaser = Instantiate(laser, laserSpawn, Quaternion.identity) as GameObject;
		enemyLaser.rigidbody2D.velocity = new Vector3(0, -projectileSpeed, 0);
	}
	
	void ShootLaser () {
		float probability = Time.deltaTime * fireRate;
		if (Random.value < probability) {
			SpawnLaser();
		}
	}
}
