using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandlerScript : MonoBehaviour
{
    List<Event> events;
    public Countdown_Timer countdown_Timer;
    void Start()
    {
        //Initialize event list
        events = new List<Event>();
        //EVENT LIST
        //(Here will be where we hard-code all events we want.)
        //
        //Example Event: (feel free to copy paste and change values)
        /*
        addEvent(
            new Event("example_name",      // Name of the Event
                      "example_description"// Event description
            ),
            new Option(
                text: "optionText",                     // This is the text that the option will say
                textAfterClick: "optionTextAfterClick", // This will be what is said when the option is chosen
                bad: false,                             // OPTIONAL: Tells the candle to show a bad omen or not. DEFAULT=false.
                timerChange: 0f,                        // OPTIONAL: Adds or subtracts remaining candle time. DEFAULT=0.
                timerRate: 1f,                          // OPTIONAL: Sets how fast the candle will burn. DEFAULT=1.
                foodChange: -1,                         // OPTIONAL: This changes the player's food. DEFAULT=-1.
                progressChange: 0                       // OPTIONAL: This changes the player's progress towards escape. DEFAULT=0.
            ), // At this point you can add as many options as you want. In this example, I will add a second option
            new Option(
                text: "optionText",
                textAfterClick: "optionTextAfterClick",
                bad: false,
                timerChange: 0f,
                timerRate: 1f,
                foodChange: -1,
                progressChange: 0
            )
        );
        */
        //Here is what a more legitimate event would look like:
        addEvent(
            new Event("Animal Tracks",
                      "I saw animal tracks on the left path, which could lead to food. But the right path seemed like it would lead me out of the forest faster."
            ),
            new Option(
                text: "I took the path with animal tracks.",
                textAfterClick: "My candle seemed to sense danger, and began to burn faster. I have to get out of here before that animal tracks *me*!.",
                bad: true,
                timerRate: 2f,
                progressChange: 5
            ), 
            new Option(
                text: "I took the shorter path.",
                textAfterClick: "I chose to follow my gut with the shorter path, and found some wax to add to my candle along the way.",
                timerChange: 30.0f,
                progressChange: 10
            ),
            new Option(
                text: "I listened to my companion, who suggested a shortcut through the trees ahead.",           
                textAfterClick: "The \"shortcut\" took longer than expected, but we found berries along the way.",
                timerChange: -10.0f,
                foodChange: 5,
                progressChange: 10
            )
        );

        addEvent(
            new Event("The Mysterious Stranger",
                      "I came across a stranger in the woods. He offered to be my guide, in exchange for some food."
            ),
            new Option(
                text: "I didn't trust anyone. Not anymore.",
                textAfterClick: "I politely declined his offer, hoping he wasn't the type to hold a grudge...",
                bad: true
            ),
            new Option(
                text: "I decided to accept his help, resolving to keep my eye on him.",
                textAfterClick: "He happily devoured my food, taking his sweet time as he did so. I hoped his guidance would prove useful.",
                bad: false,
                timerChange: -10f,
                foodChange: -3,
                gainCompanion: true
            )
        );

        addEvent(
            new Event("The Sound of a Memory",
                      "At a crossing, I heard a faint bell in the distance. It stirred a memory within me, one I'd tried to forget."
            ),
            new Option(
                text: "I ignored the memory. It couldn't lead to anything good.",
                textAfterClick: "After what felt like hours of wandering, to my relief, I stopped hearing the bell. I turned around, more lost than ever.",
                bad: true,
                timerChange: 0f,
                timerRate: 1f,
                progressChange: -5
            ),
            new Option(
                text: "I followed the bell. I had to find out what it was doing out here.",
                textAfterClick: "Though I forged ahead towards the sound, I didn't hear the bell again. Still, I felt confident that this was the right direction.",
                progressChange: 10
            )
        );

        addEvent(
            new Event("The Toad's Warning",
                      "At the next crossroads, what I thought was a ribbit turned out to be a large, yellow toad, croakily warning me of danger in the clearing off to the left."
            ),
            new Option(
                text: "I shook myself. Listening to a talking toad? I must be going mad.",
                textAfterClick: "I ignored the toad's warning. To my surprise, I stumbled across the toad's breeding ground, an area it must've been trying to protect. The moment of peace seemed to make my candle burn slower.",
                timerRate: .1f,
                foodChange: 0,
                progressChange: 10
            ),
            new Option(
                text: "Whatever that toad's deal was, I wasn't going to mess with it.",
                textAfterClick: "Shrugging my shoulders, I obeyed and took the rightward path. Who knew what dangers the other path held, but this one seemed safe enough.",
                progressChange: 5
            ),
            new Option(
                text: "My companion looked skeptical, advising that the leftward path was faster.",
                textAfterClick: "To our surprise, we stumbled across the toad's breeding ground, an area it must've been trying to protect. The moment of peace seemed to make my candle burn slower.",
                timerRate: .1f,
                foodChange: 0,
                progressChange: 10
            )
        );

        addEvent(
            new Event("The Smell of Home",
                      "At one point, I came across a smell that reminded me of home. That awful place I'd left behind long ago."
            ),
            new Option(
                text: "I had to follow it. It might even be food!",
                textAfterClick: "I soon found out I'd been drawn in by the deceptive musk of a telepathic beast, lying in wait. I sprinted away as fast as I could.",
                bad: true,
                timerChange: -15f,
                foodChange: -2,
                progressChange: 5
            ),
            new Option(
                text: "The reminder of time gone by repulsed me.",
                textAfterClick: "I forged through the trees, away from that sickening smell. Tears welled up as the memory faded into the night.",
                progressChange: 15
            ),
            new Option(
                text: "Do not follow it, warned my companion. We must get out of here, quick as we can.",
                textAfterClick: "As we left the crossing in a hurry, my companion told tales of a monster that weaponizes its own musk to draw in prey.",
                timerChange: 30f,
                progressChange: 10
            )
        );
        
    }
    public void removeEvent(Event item){
        events.Remove(item);
    }
    public Event getRandomEvent(){
        if (events.Count == 0){
            Debug.Log("There are no more events!");
            return null;
        }
        return events[UnityEngine.Random.Range(0, events.Count)];
        // later: remove the selected event from events list
    }

    //Function to add an event with a variable amount of options. 
    void addEvent(Event newEvent, params Option[] options){
        foreach (Option op in options){
            newEvent.addOption(op);
        }
        events.Add(newEvent);
        Debug.Log("Added event: " + newEvent.name);
    }


}


public class Event
{
    //Members
    public string name; // Name of the event
    public string description; // Description of the event (what the text will be)
    Countdown_Timer timer;
    public List<Option> options;
    private Player player;
    private AudioSource pindropSFX;
    public Event(string name, string description){
        this.name = name;
        this.description = description;
        options = new List<Option>();
        timer = GameObject.Find("Timer").GetComponent<Countdown_Timer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pindropSFX = GameObject.Find("PinDropSFX").GetComponent<AudioSource>();
    }
    //Adds an option to the event.
    public void addOption(Option option){
        this.options.Add(option);
    }

    //Choose an option to execute by passing in the option itself.
    public void chooseOption(Option option){
        Debug.Log(option.textAfterClick);
        timer.Change_Time(option.timerChange);
        timer.Change_Tick_Rate(option.timerRate);
        player.ChangeFood(option.foodChange);
        player.ChangeProgress(option.progressChange);
        if (option.gainCompanion) player.GainCompanion();
        if (option.timerChange < 0){
            pindropSFX.Play();
        }
    }

    //choose an option to execute by passing in the option's index.
    public void chooseOption(int optionIndex){
        Option option = this.options[optionIndex];
        chooseOption(option);
    }
}


public struct Option{
    public string text; // What the option will say
    public string textAfterClick;
    public bool bad;
    public float timerChange;
    public float timerRate;
    public int foodChange;
    public int progressChange;
    public bool gainCompanion;

    public Option(string text, string textAfterClick, bool bad=false, float timerChange=0, float timerRate=1, int foodChange=-1, int progressChange=0, bool gainCompanion=false){
        this.text = text;
        this.textAfterClick = textAfterClick;
        this.bad = bad;
        this.timerChange = timerChange;
        this.timerRate = timerRate;
        this.foodChange = foodChange;
        this.progressChange = progressChange;
        this.gainCompanion = gainCompanion;
    }
}
