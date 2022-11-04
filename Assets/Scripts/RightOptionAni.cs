using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightOptionAni : MonoBehaviour
{
    void Update() {
    	if (Input.GetKeyDown(KeyCode.D)) {
    		// transform.LeanScale(Vector2.one, 1f);
    		transform.LeanMoveLocal(new Vector2(210, -30), 1).setEaseOutQuart();
    		// transform.localScale = Vector2.zero;
    		// transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    	}

    	if (Input.GetKeyDown(KeyCode.W)) {
    		transform.LeanMoveLocal(new Vector2(75, -30), 1).setEaseOutQuart();
    	}

    	if (Input.GetKeyDown(KeyCode.A)) {
    		transform.LeanMoveLocal(new Vector2(75, -30), 1).setEaseOutQuart();
    	}

    	if (Input.GetKeyDown(KeyCode.S)) {
    		transform.LeanMoveLocal(new Vector2(75, -30), 1).setEaseOutQuart();
    	}
    }
}
