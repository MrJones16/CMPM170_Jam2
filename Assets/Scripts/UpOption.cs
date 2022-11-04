using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpOption : MonoBehaviour
{
    void Update() {
    	if (Input.GetButtonDown("Up")) {
    		// transform.LeanScale(Vector2.one, 1f);
    		transform.LeanMoveLocal(new Vector2(0, 275), 1).setEaseOutQuart();
    		// transform.localScale = Vector2.zero;
    		// transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    	}

    	if (Input.GetButtonDown("Down")) {
    		transform.LeanMoveLocal(new Vector2(0, 75), 1).setEaseOutQuart();
    	}

    	if (Input.GetButtonDown("Left")) {
    		transform.LeanMoveLocal(new Vector2(0, 75), 1).setEaseOutQuart();
    	}

    	if (Input.GetButtonDown("Right")) {
    		transform.LeanMoveLocal(new Vector2(0, 75), 1).setEaseOutQuart();
    	}
    }
}

