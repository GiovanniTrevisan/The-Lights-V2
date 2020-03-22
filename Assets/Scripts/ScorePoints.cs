using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePoints : MonoBehaviour {

    public GameObject canvas;
    public int scorePoint = 0;
    Text scoreText;

    void Start () {
        scoreText = canvas.GetComponent<Text>();
        scoreText.text = " " + scorePoint;
    }
	
	void Update () {
		
	}

    public void changeScore(int amount)
    {
        scorePoint += amount;
        scoreText.text = " " + scorePoint;
    }

}
