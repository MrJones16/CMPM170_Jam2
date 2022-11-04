using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{

	public Text myText;
	public static int score = 0;

	void Update() {
		myText.text = "Score: " + score;

	}

}
