using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown_Timer : MonoBehaviour
{
    public float Start_Timer = 180f;
    float Tick_Rate = 0f; // starts at 0 for the title screen
    public float Current_Time;
    // Start is called before the first frame update
    void Start()
    {
        Current_Time = Start_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        Current_Time -= Tick_Rate * Time.deltaTime; //to make time go down

        if (Current_Time <= 0) {
            SceneManager.LoadScene(1);
            Debug.Log("Game Over");
        }
    }

    public void Change_Time(float new_Time) {
        Current_Time += new_Time;
    }

    public void Change_Tick_Rate(float new_Tick) { 
        Tick_Rate = new_Tick;
    }

}
