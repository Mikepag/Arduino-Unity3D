using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class timer : MonoBehaviour {

    public GameObject Compass;

    public Text timerText;      // Declaring a public Text called timerText
    private float startTime;    // 
    private int minutesInt;
    private int secondsInt;
    private int milisecInt;
    private int stopTime;
    private int updatedFile;    // ==1 when the time has already been writen to test.txt, ==0 when it need to be written.

    private string filename = "test.txt";
    private string textToWrite = "Time: ";


    // Use this for initialization
    void Start () {
        startTime = Time.time;  // Time.time gives us the time since the application started.

        updatedFile = 0;
        if (File.Exists(filename))  // If test.txt already exists...
        {
            File.Delete(filename);  // ...I delete it so that all the previous data stored is deleted. The file is created again when I send data to it, but it is empty.
        }
    }
	
	// Update is called once per frame
	void Update () {

        stopTime = Compass.GetComponent<compassRotation>().stopRotating;

        if (stopTime == 1){
            timerText.color = Color.red;

            if (updatedFile == 0)
            {
                textToWrite = textToWrite + " " + timerText.text.ToString() + "\n";    //create a proper string.
                File.AppendAllText(filename, textToWrite);  //write to the file.
                updatedFile = 1;
            }

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
