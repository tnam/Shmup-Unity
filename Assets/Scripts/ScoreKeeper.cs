using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int s_Score;

	void Start () {
		m_ScoreText = GetComponent<Text>();
		ResetScore();
	}
	
	public void AddScore (int score) {
		s_Score += score;
		m_ScoreText.text = s_Score.ToString();
	}
	
	public void ResetScore () {
		s_Score = 0;
	}
	
	private Text m_ScoreText;
	
}
