using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownOption : MonoBehaviour
{
    void Update() {
    	if (Input.GetButtonDown("Down")) {
    		// transform.LeanScale(Vector2.one, 1f);
    		transform.LeanMoveLocal(new Vector2(0, -220), 1).setEaseOutQuart();
    		// transform.localScale = Vector2.zero;
    		// transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
            TextScript.score += 10;
    	}

    	if (Input.GetButtonDown("Up")) {
    		transform.LeanMoveLocal(new Vector2(0, -125), 1).setEaseOutQuart();
    	}

    	if (Input.GetButtonDown("Left")) {
    		transform.LeanMoveLocal(new Vector2(0, -125), 1).setEaseOutQuart();
    	}

    	if (Input.GetButtonDown("Right")) {
    		transform.LeanMoveLocal(new Vector2(0, -125), 1).setEaseOutQuart();
    	}
    }
}
