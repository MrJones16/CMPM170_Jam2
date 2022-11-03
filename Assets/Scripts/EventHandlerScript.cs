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
                foodChange: 0,                          // OPTIONAL: This changes the player's food. DEFAULT=0.
                progressChange: 0                       // OPTIONAL: This changes the player's food. DEFAULT=0.
            ), // At this point you can add as many options as you want. In this example, I will add a second option
            new Option(
                text: "optionText",
                textAfterClick: "optionTextAfterClick"
                // This option gives the same exact result as the first one, ommitting default parameters.
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
                textAfterClick: "My candle seemed to sense danger, and began to burn faster. I have to get out of here before that animal tracks *me*.",
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
                progressChange: 5
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


public class Event{
    //Members
    public string name; // Name of the event
    public string description; // Description of the event (what the text will be)
    Countdown_Timer timer;
    public List<Option> options;
    private Player player;
    public Event(string name, string description){
        this.name = name;
        this.description = description;
        options = new List<Option>();
        timer = GameObject.Find("Timer").GetComponent<Countdown_Timer>();
        player = GameObject.Find("Player").GetComponent<Player>();
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

    public Option(string text, string textAfterClick, bool bad=false, float timerChange=0, float timerRate=1, int foodChange=0, int progressChange=0, bool gainCompanion=false){
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
