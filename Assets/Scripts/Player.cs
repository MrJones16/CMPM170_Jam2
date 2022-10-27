using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Camera camera;

    [Header("Settings")]
    [SerializeField]
    private float transitionDuration = 2;

    private bool divining = false;
    private bool eventing = true;

    private void Start() {
        
    }

    private void Update() {
        if (eventing) {
            EventUpdate();
        } else if (divining) {
            DivinationUpdate();
        }
    }

    private void EventUpdate() {
        if (Input.GetButtonDown("Down")) {
            StartCoroutine(SetDivination(true));
            return;
        } 
    }

    private void DivinationUpdate() {
        if (Input.GetButtonDown("Up")) {
            StartCoroutine(SetDivination(false));
            return;
        } 
    }

    private IEnumerator SetDivination(bool toDivine)
    {
        divining = false;
        eventing = false;

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
        divining = toDivine;
        eventing = !toDivine;
    }
}