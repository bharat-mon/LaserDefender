using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed;
	public float padding;
	public GameObject Laser;
	public float projectileSpeed;
	public float fireRate = 1.0f;
	public float health = 500;
	
	public AudioClip fireLaser;
	public AudioClip hitTaken;
	
	private Vector3 shipPos;
	private float xMin;
	private float xMax;
	
	// Use this for initialization
	void Start () {
		float distanceZ = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceZ));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceZ));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement();
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("ShootLaser", 0.000001f, fireRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("ShootLaser");
		}
	}
	
	void PlayerMovement () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			shipPos = Vector3.left * moveSpeed * Time.deltaTime;
			this.transform.position += shipPos;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			shipPos = Vector3.right * moveSpeed * Time.deltaTime;
			this.transform.position += shipPos;
		}
		float clampX = Mathf.Clamp(this.transform.position.x, xMin, xMax);
		this.transform.position = new Vector3 (clampX, this.transform.position.y, this.transform.position.z);
	}
	
	void ShootLaser () {
		Vector3 laserSpawn = this.transform.position + new Vector3(0, 0.5f, 0);
		GameObject laserBeam = Instantiate(Laser, laserSpawn, Quaternion.identity) as GameObject;
		laserBeam.rigidbody2D.velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireLaser, this.transform.position);
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser) {
			laser.Hit();
			health -= laser.GetDamage();
			AudioSource.PlayClipAtPoint(hitTaken, this.transform.position);
			if (health <= 0) {
				Destroy(gameObject);
			}
			}
		}
}
