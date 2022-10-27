using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Camera camera;

    [Header("Settings")]
    [SerializeField]
    private float transitionDuration;

    // states
    private bool eventing = true;
    private bool divining = false;
    private bool walking = false;

    private void Start() {
        
    }

    private void Update() {
        if (eventing) {
            EventUpdate();
        } else if (divining) {
            DivinationUpdate();
        } else if (walking) {
            WalkUpdate();
        }
    }

    private void EventUpdate() {
        if (Input.GetButtonDown("Down")) {
            StartCoroutine(SetDivination(true));
            return;
        }

        if (Input.GetButtonDown("Right")) {
            StartCoroutine(Walk(true));
            return;
        }

        if (Input.GetButtonDown("Left")) {
            StartCoroutine(Walk(false));
            return;
        }
    }

    private void DivinationUpdate() {
        if (Input.GetButtonDown("Up")) {
            StartCoroutine(SetDivination(false));
            return;
        }
    }

    private void WalkUpdate() {

    }

    private IEnumerator SetDivination(bool toDivine)
    {
        // pause controls
        eventing = false;
        divining = false;

        float timeElapsed = 0;
        while(timeElapsed < transitionDuration)
        {
            float t = timeElapsed / transitionDuration;
            t = t * t * (3f - 2f * t);
            float newAngle = toDivine ? Mathf.Lerp(0, 30, t) : Mathf.Lerp(30, 0, t);

            Vector3 angle = camera.transform.eulerAngles;
            camera.transform.eulerAngles = new Vector3(newAngle, angle.y, angle.z);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // allow update to start
        eventing = !toDivine;
        divining = toDivine;
    }

    private IEnumerator Walk(bool right)
    {
        // pause controls
        eventing = false;
        divining = false;
        walking = true;

        float timeElapsed = 0;
        while(timeElapsed < transitionDuration)
        {
            float t = timeElapsed / transitionDuration;
            t = t * t * (3f - 2f * t);
            // float newAngle = right ? Mathf.Lerp(0, 30, t) : Mathf.Lerp(30, 0, t);
            float newZ = Mathf.Lerp(0, 10, t);

            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // allow update to start
        walking = false;
        eventing = true;
    }
}