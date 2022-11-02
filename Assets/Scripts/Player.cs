using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Candle_Size candle;
    [SerializeField]
    private Transform topCircle;
    [SerializeField]
    private Transform leftCircle;
    [SerializeField]
    private Transform rightCircle;
    [SerializeField]
    private Transform hand;

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
        candle.transform.localPosition = Vector3.forward; // Position the picked up object facing the same direction as the player
        candle.transform.localRotation = Quaternion.Euler(Vector3.forward); // Not exactly sure what this does but if I leave it out it becomes random
        candle.transform.Translate(0.5f,1.0f,0); // offset the position
        candle.transform.localRotation = Quaternion.Euler(90,0,90); // offset the rotation
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
            // create eased transition
            float t = timeElapsed / transitionDuration;
            t = (Mathf.Sin(Mathf.PI*t-Mathf.PI/2)/2)+.5f;
            float newAngle;
            
            if (toDivine) {
                newAngle = Mathf.Lerp(0, 30, t);
                candle.transform.position = new Vector3(Mathf.Lerp(hand.position.x, topCircle.position.x, t), Mathf.Lerp(hand.position.y, topCircle.position.y, t), Mathf.Lerp(hand.position.z, topCircle.position.z, t));
                candle.transform.eulerAngles = new Vector3(Mathf.Lerp(hand.eulerAngles.x, topCircle.eulerAngles.x, t), Mathf.Lerp(hand.eulerAngles.y, topCircle.eulerAngles.y, t), Mathf.Lerp(hand.eulerAngles.z, topCircle.eulerAngles.z, t));
            }
            else {
                newAngle = Mathf.Lerp(30, 0, t);
                candle.transform.position = new Vector3(Mathf.Lerp(topCircle.position.x, hand.position.x, t), Mathf.Lerp(topCircle.position.y, hand.position.y, t), Mathf.Lerp(topCircle.position.z, hand.position.z, t));
                candle.transform.eulerAngles = new Vector3(Mathf.Lerp(topCircle.eulerAngles.x, hand.eulerAngles.x, t), Mathf.Lerp(topCircle.eulerAngles.y, hand.eulerAngles.y, t), Mathf.Lerp(topCircle.eulerAngles.z, hand.eulerAngles.z, t));
            }

            Vector3 angle = camera.transform.eulerAngles;
            camera.transform.eulerAngles = new Vector3(newAngle, angle.y, angle.z);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // place the candle on the ground or in the hand
        

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