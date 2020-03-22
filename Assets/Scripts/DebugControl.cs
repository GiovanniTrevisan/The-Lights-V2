using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Keypad1))
			SceneManager.LoadScene ("menu-def");
		if (Input.GetKeyDown (KeyCode.Keypad2))
			SceneManager.LoadScene ("ChooseScene");
		if (Input.GetKeyDown (KeyCode.Keypad3))
			SceneManager.LoadScene ("Village of Barovia - night def");
	//	if (Input.GetKeyDown (KeyCode.Keypad4))
//			SceneManager.LoadScene ("Village of Barovia - night test 3");
		if (Input.GetKeyDown (KeyCode.Keypad5))
			SceneManager.LoadScene ("tcc-ap");
	}
}
