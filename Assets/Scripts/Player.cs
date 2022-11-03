using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private ParticleSystem smoke;
    [SerializeField]
    private EventHandlerScript eventHandler;
    [SerializeField]
    private SpriteRenderer circles;
    [SerializeField]
    private Sprite twoCircles;
    [SerializeField]
    private Sprite threeCircles;
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
    [SerializeField]
    public int foodAmount;
    [SerializeField]
    private int progress = 20;
    [SerializeField]
    private int victoryCondition = 100;
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource walkingSFX;
    [SerializeField]
    private AudioSource fireSFX;
    [SerializeField]
    private AudioSource cardSFX;

    // states
    private bool eventing = true;
    private bool divining = false;
    private bool walking = false;
    private bool companion = false;
    private bool walkingSoundOn = false;


    private Event currentEvent;

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
        //Walking SFX
        //I didn't use the walkupdate() function because it doesn't really work with playing an audio clip
        //It would just repeatedly start the sound while walking
        if (walking && !walkingSoundOn){
            walkingSFX.Play();
            walkingSoundOn = true;
        }
        if (!walking && walkingSoundOn){
            walkingSFX.Stop();
            walkingSoundOn = false;
        }
    }

    private void EventUpdate() {
        // get an event
        if (currentEvent == null) {
            currentEvent = eventHandler.getRandomEvent();
            // set 2 or 3 circles depending on # of choices (later: and whether you have a companion)
            circles.sprite = currentEvent.options.Count > 2 && companion ? threeCircles : twoCircles;

            // display event in console for now
            Debug.Log(currentEvent.name);
            Debug.Log(currentEvent.description);
        }

        if (Input.GetButtonDown("Down")) {
            StartCoroutine(SetDivination(true));
            fireSFX.Play();
            BadOmen(currentEvent.options[1].bad);
            return;
        }

        if (Input.GetButtonDown("Left")) {
            StartCoroutine(Walk(false));
            // trigger option 0 of current event
            currentEvent.chooseOption(0);
            // then disable current event
            currentEvent = null;
            //play cardSFX
            cardSFX.Play();
            return;
        }

        if (Input.GetButtonDown("Right")) {
            StartCoroutine(Walk(true));
            // trigger option 1 of current event
            currentEvent.chooseOption(1);
            // then disable current event
            currentEvent = null;
            //play cardSFX
            cardSFX.Play();
            return;
        }

        if (Input.GetButtonDown("Up")) {
            StartCoroutine(Walk(true));
            // if option 2 exists, 
            if (currentEvent.options.Count > 2 && companion) {
                // trigger option 2 of current event
                currentEvent.chooseOption(2);
                // then disable current event
                currentEvent = null;
                //play cardSFX
                cardSFX.Play();
            }
            return;
        }
    }

    private void DivinationUpdate() {
        Vector3 candlePos = candle.position;
        
        // ways to exit divination
        if (Input.GetButtonDown("Up") && (currentEvent.options.Count < 3 || candlePos == topCircle.position)) {
            StartCoroutine(SetDivination(false));
            BadOmen(false);
            fireSFX.Play();
            return;
        }
        if (Input.GetButtonDown("Down") && candlePos != topCircle.position) {
            StartCoroutine(SetDivination(false));
            BadOmen(false);
            fireSFX.Play();
            return;
        }

        // move the candle
        if (Input.GetButtonDown("Up") && candlePos != topCircle.position) {
            if (currentEvent.options.Count >= 3) {
                StartCoroutine(MoveCandle(candlePos, topCircle.position));
                BadOmen(currentEvent.options[2].bad);
                fireSFX.Play();
            }
            return;
        }
        if (Input.GetButtonDown("Left") && candlePos != leftCircle.position) {
            StartCoroutine(MoveCandle(candlePos, leftCircle.position));
            BadOmen(currentEvent.options[0].bad);
            fireSFX.Play();
            return;
        }
        if (Input.GetButtonDown("Right") && candlePos != rightCircle.position) {
            StartCoroutine(MoveCandle(candlePos, rightCircle.position));
            BadOmen(currentEvent.options[1].bad);
            fireSFX.Play();
            return;
        }
        if (Input.GetButtonDown("Down") && candlePos == topCircle.position) {
            StartCoroutine(MoveCandle(candlePos, rightCircle.position));
            BadOmen(currentEvent.options[1].bad);
            fireSFX.Play();
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
        Transform destination = toDivine ? rightCircle : hand;

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
    
    public void ChangeFood(int sustenance) {
        foodAmount += sustenance;
        if (foodAmount <= 0) {
            //should end game
            Debug.Log("GameOver");
        }
    }

    public void ChangeProgress(int progressChange) {
        int progressUpdate = progress+progressChange;
        // progress can't go below 0
        progress = progressUpdate > 0 ? progressUpdate : 0;
        // escape the forest
        if (progress >= victoryCondition) {
            //should end game in a victory
            Debug.Log("Victory");
        }
    }

    public void GainCompanion() {
        companion = true;
    }
}