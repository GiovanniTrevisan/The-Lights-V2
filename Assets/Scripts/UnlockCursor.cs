using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCursor : MonoBehaviour {

	// Para o Menu

		void Start ()
		{
		Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		void Update ()
		{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		}
	} 	