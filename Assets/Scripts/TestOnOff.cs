using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{

	public GameObject obj;
	// public bool on = true;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			CardsShow.on = true;
			Debug.Log("1" + CardsShow.on);
			// obj.SetActive (true);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			CardsShow.on = false;
			Debug.Log("2" + CardsShow.on);
			// obj.SetActive (false);
		}
    }
}
