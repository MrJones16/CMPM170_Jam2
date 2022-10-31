using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandlerScript : MonoBehaviour
{
    List<Event> events;
    void Start()
    {
        //Initialize event list
        events = new List<Event>();
        //EVENT LIST
        //(Here will be where we hard-code all events we want.)
        //
        //Example Event: (feel free to copy paste and change values)
        addEvent(
            new Event("example_name",      //Name of the Event
                      "example_description"//Event description
            ),
            new Option(
                "optionText",           //This is the text that the option will say
                "optionTextAfterClick", //This will be what is said when the option is chosen
                0.0f,                   //This is the change on the timer. Positive values add time, negative values subtract time.
                1.0f                    //This is the value of how fast the candle will burn. 1 second is the default.
            ), // At this point you can add as many options as you want. In this example, I will add a second option
            new Option(
                "optionText",           //Option Text
                "optionTextAfterClick", //Text After choosing
                0.0f,                   //Timer Change
                1.0f                    //Candle Burn Rate
            )
        );
        //Here is what a more legitimate event would look like:
        addEvent(
            new Event("Animal Tracks",
                      "You see animal tracks on the left path which could lead to food, but the right path seems like it will lead you out of the forest faster"
            ),
            new Option(
                "Take the path with animal tracks",//Option Text
                "I chose to follow the animal tracks, even though it could be dangerous", //Text After choosing
                0f, //Timer change
                2.0f //Candle Burn Rate (I doubled the burn rate to simulate the danger of the animal)
            ), 
            new Option(
                "Take the shorter path",           //Option Text
                "I chose to follow my gut with the shorter path, and found some wax on the side of the path", //Text After choosing
                30.0f, //Timer Change (I added 30 seconds to the candle since they found wax)
                1.0f   //Candle Burn Rate
            )
        );
    }

    //Function to add an event with a variable amount of options. 
    void addEvent(Event newEvent, params Option[] options){
        foreach (Option op in options){
            newEvent.addOption(op);
        }
        events.Add(newEvent);
    }


}


public class Event{
    //Members
    string name; // Name of the event
    string description; // Description of the event (what the text will be)
    List<Option> options;
    public Event(string name, string description){
        this.name = name;
        this.description = description;
        options = new List<Option>();
    }
    //Adds an option to the event.
    public void addOption(Option option){
        this.options.Add(option);
    }

    //Choose an option to execute by passing in the option itself.
    public void chooseOption(Option option){
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //NOT YET IMPLEMENTED : This requires me to know the functions to change the candle burn rate and overal time still.
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }

    //choose an option to execute by passing in the option's index.
    public void chooseOption(int optionIndex){
        Option option = this.options[optionIndex];
        //NOT YET IMPLEMENTED : This requires me to know the functions to change the candle burn rate and overal time still.
    }
}


public struct Option{
    string text; // What the option will say
    string textAfterClick;
    float timerChange;
    float timerRate;

    public Option(string text, string textAfterClick, float timerChange, float timerRate){
        this.text = text;
        this.textAfterClick = textAfterClick;
        this.timerChange = timerChange;
        this.timerRate = timerRate;
    }
}