using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private ParticleSystem smoke;
    [SerializeField]
    private Transform candle;
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
    private float candleMoveDuration;
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
        Vector3 candlePos = candle.position;
        
        // ways to exit divination
        if (Input.GetButtonDown("Up") && candlePos == topCircle.position) {
            StartCoroutine(SetDivination(false));
            return;
        }
        if (Input.GetButtonDown("Down") && candlePos != topCircle.position) {
            StartCoroutine(SetDivination(false));
            return;
        }

        // move the candle
        if (Input.GetButtonDown("Up") && candlePos != topCircle.position) {
            StartCoroutine(MoveCandle(candlePos, topCircle.position));
            return;
        }
        if (Input.GetButtonDown("Left") && candlePos != leftCircle.position) {
            StartCoroutine(MoveCandle(candlePos, leftCircle.position));
            // let's say left is always a bad omen for now
            BadOmen(true);
            // once the event system is setup, it'll look something like this:
            // BadOmen(currentEvent.options[0].bad);
            return;
        }
        if (Input.GetButtonDown("Right") && candlePos != rightCircle.position) {
            StartCoroutine(MoveCandle(candlePos, rightCircle.position));
            // let's say right is always a good omen for now
            BadOmen(false);
            return;
        }
        if (Input.GetButtonDown("Down") && candlePos == topCircle.position) {
            StartCoroutine(MoveCandle(candlePos, rightCircle.position));
            return;
        }
    }

    private void BadOmen(bool bad) {
        ParticleSystem.ForceOverLifetimeModule force = smoke.forceOverLifetime;
        ParticleSystem.VelocityOverLifetimeModule velocity = smoke.velocityOverLifetime;
        if (bad) {
            force.enabled = true;
            velocity.yMultiplier = 0.4f;
        } else {
            force.enabled = false;
            velocity.yMultiplier = 0.2f;
        }
    }

    private void WalkUpdate() {

    }

    private IEnumerator SetDivination(bool toDivine)
    {
        // pause controls
        eventing = false;
        divining = false;

        // figure out where candle is going
        Transform source = toDivine ? hand : candle;
        Transform destination = toDivine ? topCircle : hand;

        float timeElapsed = 0;
        while(timeElapsed < transitionDuration)
        {
            // create eased transition
            float t = timeElapsed / transitionDuration;
            t = (Mathf.Sin(Mathf.PI*t-Mathf.PI/2)/2)+.5f;
            
            float newAngle = toDivine? Mathf.Lerp(0, 30, t) : Mathf.Lerp(30, 0, t);
            candle.position = new Vector3(Mathf.Lerp(source.position.x, destination.position.x, t), Mathf.Lerp(source.position.y, destination.position.y, t), Mathf.Lerp(source.position.z, destination.position.z, t));
            candle.eulerAngles = new Vector3(Mathf.Lerp(source.eulerAngles.x, destination.eulerAngles.x, t), Mathf.Lerp(source.eulerAngles.y, destination.eulerAngles.y, t), Mathf.Lerp(source.eulerAngles.z, destination.eulerAngles.z, t));

            Vector3 angle = camera.transform.eulerAngles;
            camera.transform.eulerAngles = new Vector3(newAngle, angle.y, angle.z);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        candle.position = destination.position;

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

    private IEnumerator MoveCandle(Vector3 source, Vector3 destination)
    {
        divining = false;

        float timeElapsed = 0;
        while(timeElapsed < candleMoveDuration)
        {
            // create eased transition
            float t = timeElapsed / candleMoveDuration;
            t = (Mathf.Sin(Mathf.PI*t-Mathf.PI/2)/2)+.5f;
            
            candle.position = new Vector3(Mathf.Lerp(source.x, destination.x, t), Mathf.Lerp(source.y, destination.y, t), Mathf.Lerp(source.z, destination.z, t));
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        candle.position = destination;

        divining = true;
    }
}