using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftOptionAni : MonoBehaviour
{
    void Update() {
    	if (Input.GetKeyDown(KeyCode.A)) {
    		// transform.LeanScale(Vector2.one, 1f);
    		transform.LeanMoveLocal(new Vector2(-220, -30), 1).setEaseOutQuart();
    		// transform.localScale = Vector2.zero;
    		// transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    	}

    	if (Input.GetKeyDown(KeyCode.W)) {
    		transform.LeanMoveLocal(new Vector2(-120, -30), 1).setEaseOutQuart();
    	}

    	if (Input.GetKeyDown(KeyCode.S)) {
    		transform.LeanMoveLocal(new Vector2(-120, -30), 1).setEaseOutQuart();
    	}

    	if (Input.GetKeyDown(KeyCode.D)) {
    		transform.LeanMoveLocal(new Vector2(-120, -30), 1).setEaseOutQuart();
    	}
    }
}
