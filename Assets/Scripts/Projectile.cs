using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float m_Damage = 100f;
	
	public float GetDamage () {
		return m_Damage;
	}
	
	public void Hit() {
		Destroy(gameObject);
	}	
	
	void OnBecameInvisible () {
		Destroy(gameObject);
	}
}
