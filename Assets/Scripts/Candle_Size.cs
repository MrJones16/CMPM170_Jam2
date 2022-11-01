using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_Size : MonoBehaviour
{
    public Countdown_Timer timer;

    // Don't manually set this; set the Y scale to whatever the biggest candle size is
    private float Max_Candle_Size;

    // Start is called before the first frame update
    void Start()
    {
        Max_Candle_Size = this.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(0, Max_Candle_Size, timer.Current_Time/timer.Start_Timer), transform.localScale.z);
    }
}
