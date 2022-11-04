using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainText : MonoBehaviour
{

	public TextMeshProUGUI mainText;
	public string mainTitle;

	// public void mainEvent(){
	// 	mainText.GetComponent<Text>().text = "2";
	// }

	void Start() {
		mainText = GetComponent<TextMeshProUGUI>();
		mainTitle = "Hihihihihi";
	}

	void Update() {
		mainText.text = mainTitle.ToString();
	}
}
