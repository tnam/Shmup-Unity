using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float m_Speed = 15f;
	public float m_BorderDistance = 1f;
	public float m_ProjectileSpeed = 5f;
	public float m_FiringRate = 0.2f;
	public float m_HP = 300f;
	
	public GameObject m_LaserPrefab;

	// Use this for initialization
	void Start () {
		
		m_LevelManager = GameObject.FindObjectOfType<LevelManager>();
		
		Vector3 leftScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
		Vector3 rightScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
		
		m_XMin = leftScreenEdge.x + m_BorderDistance;
		m_XMax = rightScreenEdge.x - m_BorderDistance;
		
		m_Sounds = GetComponents<AudioSource>();
		m_LaserSound = m_Sounds[0];
		m_ExplosionSound = m_Sounds[1];
	}
	
	// Update is called once per frame
	void Update () {
		FireInput();
		MovePlayer();
	}
	
	void MovePlayer() {
		
		// Ship movement
		float direction = Input.GetAxis("Horizontal");
		float move = direction * m_Speed * Time.deltaTime;
		transform.position += new Vector3(move, 0, 0);
		
		// Clamping ship's position
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, m_XMin, m_XMax);
		transform.position = pos;
	}
	
	void FireInput() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.001f, m_FiringRate);
		}
		
		if(Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	}
	
	// TODO Consider using object pooling
	void Fire() {	
			GameObject laserBeam = Instantiate(m_LaserPrefab, transform.position, Quaternion.identity) as GameObject;
			laserBeam.rigidbody2D.velocity = new Vector3(0, m_ProjectileSpeed, 0);
			
			m_LaserSound.Play();
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile enemyProjectile = collider.gameObject.GetComponent<Projectile>();
		
		if(enemyProjectile) {
			m_HP -= enemyProjectile.GetDamage();
			enemyProjectile.Hit();
			if(m_HP < 0) {
				Die();
			}
		}
	}
	
	void Die() {
		m_ExplosionSound.Play();
		renderer.enabled = false;
		Destroy(gameObject, m_ExplosionSound.clip.length);
		//m_LevelManager.LoadLevel("Game Over");
		Invoke("LoadGameOver", 2);
	}
	
	void LoadGameOver() {
		m_LevelManager.LoadLevel("Game Over");
	}
	
	
	private float m_XMin;
	private float m_XMax;
	private LevelManager m_LevelManager;
	private AudioSource[] m_Sounds;
	private AudioSource m_LaserSound;
	private AudioSource m_ExplosionSound;
	
}
