using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsShow : MonoBehaviour
{

	public GameObject obj;
	public bool on = true;

	void Update () {
		if (on == true){
			obj.SetActive (true);
		}
		else {
			obj.SetActive (false);
		}

		// if (Input.GetKeyDown(KeyCode.Alpha1)) {
		// 	Debug.Log("1");
		// 	on = true;
		// 	obj.SetActive (true);
		// }

		// if (Input.GetKeyDown(KeyCode.Alpha2)) {
		// 	Debug.Log("2");
		// 	on = false;
		// 	obj.SetActive (false);
		// }

	}
}
