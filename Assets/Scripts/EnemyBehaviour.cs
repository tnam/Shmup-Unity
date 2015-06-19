using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float m_HP = 150f;
	public float m_ProjectileSpeed = 5f;
	public float m_FireFrequency = 0.5f;
	public int m_ScoreValue = 150;
	
	public GameObject m_LaserPrefab;
	
	void Start() {
	
		//m_ScoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		m_ScoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>();
		
		// Initialize SFX
		m_Sounds = GetComponents<AudioSource>();
		m_FireSound = m_Sounds[0];
		m_DestroyedSound = m_Sounds[1];
	}
	
	void Update () {
	
		float fireProbability = Time.deltaTime * m_FireFrequency;
		
		if(Random.value < fireProbability) {
			Fire();
		}
	}
	
	void Fire() {
		GameObject laserBeam = Instantiate(m_LaserPrefab, transform.position, Quaternion.identity) as GameObject;
		laserBeam.rigidbody2D.velocity = new Vector3(0, -m_ProjectileSpeed, 0);
		m_FireSound.Play();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Projectile playerProjectile = collider.gameObject.GetComponent<Projectile>();
		
		if(playerProjectile) {
			m_HP -= playerProjectile.GetDamage();
			playerProjectile.Hit();
			if(m_HP < 0) {
				Die();
			}
		}
	}
	
	void Die() {
		m_DestroyedSound.Play();
		Destroy(gameObject);
		m_ScoreKeeper.AddScore(m_ScoreValue);
	}
	
	private ScoreKeeper m_ScoreKeeper;
	private AudioSource[] m_Sounds;
	private AudioSource m_FireSound;
	private AudioSource m_DestroyedSound;
}
