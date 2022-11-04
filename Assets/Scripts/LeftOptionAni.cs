using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftOptionAni : MonoBehaviour
{
    void Update() {
    	if (Input.GetButtonDown("Left")) {
    		// transform.LeanScale(Vector2.one, 1f);
    		transform.LeanMoveLocal(new Vector2(-210, -30), 1).setEaseOutQuart();
    		// transform.localScale = Vector2.zero;
    		// transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    	}

    	if (Input.GetButtonDown("Up")) {
    		transform.LeanMoveLocal(new Vector2(-75, -30), 1).setEaseOutQuart();
    	}

    	if (Input.GetButtonDown("Down")) {
    		transform.LeanMoveLocal(new Vector2(-75, -30), 1).setEaseOutQuart();
    	}

    	if (Input.GetButtonDown("Right")) {
    		transform.LeanMoveLocal(new Vector2(-75, -30), 1).setEaseOutQuart();
    	}
    }
}
