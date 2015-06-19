using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	public GameObject m_EnemyPrefab;
	public float m_Speed;
	public float m_SpawningTime = 0.5f;

	// Use this for initialization
	void Start () {
		
		SpawnEnemies();
		
		m_LeftScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
		m_RightScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
		
		// Moving left initially
		m_MoveDirection = Vector3.left * m_Speed * Time.deltaTime;
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveFormation();
		
		if(EmptyFormation()) {
			SpawnEnemies();
		}
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(m_Width, m_Height));
	}
	
	void MoveFormation() {
			
		float offset = m_Width / 2;
		
		if(transform.position.x - offset < m_LeftScreenEdge.x || transform.position.x + offset > m_RightScreenEdge.x) {
			m_MoveDirection = -m_MoveDirection;
		}
		
		transform.position += m_MoveDirection;
	}
	
	bool EmptyFormation() {
		foreach (Transform position in transform) {
			if(position.childCount > 0)
				return false;
		}
		return true;
	}
	
	void SpawnEnemies() {
		Transform freePosition = NextFreePosition();
		
		if(freePosition) {
			GameObject enemy = Instantiate(m_EnemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		
		if(NextFreePosition()) {
			Invoke("SpawnEnemies", m_SpawningTime);
		}
	}
	
	Transform NextFreePosition() {
		foreach (Transform position in transform) {
			if(position.childCount == 0)
				return position;
		}
		return null;
	}
	
	[SerializeField] private float m_Width = 10f;
	[SerializeField] private float m_Height = 5f;
	
	private Vector3 m_MoveDirection;
	private Vector3 m_LeftScreenEdge;
	private Vector3 m_RightScreenEdge;
}
