using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Transform candle;

    [Header("Settings")]
    [SerializeField]
    private float transitionDuration;
    [SerializeField]
    private float walkDuration;
    [SerializeField]
    private float forwardWalkDistance;
    [SerializeField]
    private float sidewaysWalkDistance;
    [SerializeField]
    private float walkTurnAngle;

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

        // RotateCandle();
    }

    private void RotateCandle() {
        candle.localPosition = Vector3.forward; // Position the picked up object facing the same direction as the player
        candle.localRotation = Quaternion.Euler(Vector3.forward); // Not exactly sure what this does but if I leave it out it becomes random
        candle.Translate(0.5f,1.0f,0); // offset the position
        candle.localRotation = Quaternion.Euler(90,0,90); // offset the rotation
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
            // change angle in an eased curve
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
        while(timeElapsed < walkDuration)
        {
            // forward travel in a sine curve
            float t = timeElapsed / walkDuration;
            t = (Mathf.Sin(Mathf.PI*t-Mathf.PI/2)/2)+.5f;
            float newZ = Mathf.Lerp(0, forwardWalkDistance, t);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

            // sideways travel in a sine curve
            float t2 = timeElapsed / walkDuration;
            t2 = (Mathf.Sin(Mathf.PI*t2-Mathf.PI/2)/2)+.5f;
            float newX = right? Mathf.Lerp(0, sidewaysWalkDistance, t) : Mathf.Lerp(0, -sidewaysWalkDistance, t);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            // view angle follows a sine wave
            float t3 = timeElapsed / walkDuration;
            t3 = (Mathf.Sin(2*Mathf.PI*t3-Mathf.PI/2)/2)+.5f;
            float newAngle = right ? Mathf.Lerp(0, walkTurnAngle, t3) : Mathf.Lerp(0, -walkTurnAngle, t3);
            Vector3 angle = camera.transform.eulerAngles;
            camera.transform.eulerAngles = new Vector3(angle.x, newAngle, angle.z);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // allow update to start
        walking = false;
        eventing = true;
    }
}