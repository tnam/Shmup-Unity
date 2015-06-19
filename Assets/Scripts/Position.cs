using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
	
	void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, m_Radius);
	}
	
	private float m_Radius = 1f;
}
