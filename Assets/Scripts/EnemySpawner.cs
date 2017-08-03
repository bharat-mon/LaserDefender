using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 5f;
	public float height = 10f;
	public float moveSpeed = 3f;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float xMin;
	private float xMax;

	// Use this for initialization
	void Start () {
		PlayAreaMapping ();
		SpawnEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		EnemyDirection();
		if (AllEnemiesDead()) {
			SpawnEnemy();
		}
	}
	
	void OnDrawGizmos () {
		Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height));
	}
	
	void PlayAreaMapping () {
		float distanceToCamera = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3 (1, 0, distanceToCamera));
		xMin = leftBoundary.x;
		xMax = rightBoundary.x;
	}
	
//	void SpawnEnemy () {
//		foreach (Transform child in transform) {
//			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
//			enemy.transform.parent = child;
//		}
//	}
	
	void SpawnEnemy () {
		Transform freePosition = NextFreePosition();
		if (freePosition) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()) {
			Invoke("SpawnEnemy", spawnDelay);
		}		
	}
	
	void EnemyDirection () {
		if (movingRight) {
			this.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
		} else {
			this.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
		}
		float leftEdge = this.transform.position.x - (0.5f * width);
		float rightEdge = this.transform.position.x + (0.5f * width);
		if (leftEdge < xMin) {
			movingRight = true;
		} else if (rightEdge > xMax) {
			movingRight = false;
		}
	}
	
	Transform NextFreePosition () {
		foreach(Transform child in transform) {
			if (child.childCount <= 0) {
				return child;
			}
		}
		return null;
	}
	
	bool AllEnemiesDead () {
		foreach(Transform child in transform) {
			if (child.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
