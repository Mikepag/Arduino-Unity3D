using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour {

    public GameObject Compass;

    public Text timerText;      // Declaring a public Text called timerText
    private float startTime;    // 
    private int minutesInt;
    private int secondsInt;
    private int milisecInt;
    private int stopTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;  // Time.time gives us the time since the application started.
		
	}
	
	// Update is called once per frame
	void Update () {

        stopTime = Compass.GetComponent<compassRotation>().stopRotating;

        if (stopTime == 1){
            timerText.color = Color.red;
            return;
        }

        float t = Time.time - startTime;  // Gives the time in seconds since timer started.

        string minutes = ((int)t / 60).ToString();  // gives number of minutes by dividing the integer value of t by 60.
        //string seconds = (t % 60).ToString("f2");   // f2 is there to keep only 2 decimal places.
        string seconds = ((int)t % 60).ToString();
        string miliseconds = ((int)(t * 100f) % 100).ToString();

        minutesInt = ((int)t / 60);
        secondsInt = ((int)t % 60);
        milisecInt = ((int)(t * 100f) % 100);

        if (minutesInt < 10){
            minutes = "0" + minutes;
        }
        if(secondsInt < 10){
            seconds = "0" + seconds;
        }
        if(milisecInt < 10){
            miliseconds = "0" + miliseconds;
        }

        timerText.text = minutes + ":" + seconds + ":" + miliseconds;
	}
}
