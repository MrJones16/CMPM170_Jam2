using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_Size : MonoBehaviour
{
    public Countdown_Timer timer;
    public float Max_Candle_Size = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(0, Max_Candle_Size, timer.Current_Time/timer.Start_Timer), transform.localScale.z);
    }
}
