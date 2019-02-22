using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class timer : MonoBehaviour {

    public GameObject Compass;
    public int unfinCD = 0;   // Finished Countdown

    public Text timerText;      // Declaring a public Text called timerText
    private float startTime;    // 
    private int minutesInt;
    private int secondsInt;
    private int milisecInt;
    private int stopTime;
    private int updatedFile;    // ==1 when the time has already been writen to test.txt, ==0 when it need to be written.
    private int toCountdown;
    private int didCntdown;  // ==1 when I already counted down, ==0 at the start of the game, when I need to do a countdown.
    private float passedTime;

    private string filename = "test.txt";
    private string textToWrite = "Time: ";



    // Use this for initialization
    void Start() {
        //startTime = Time.time;  // Time.time gives us the time since the application started.
        stopTime = 0;
        toCountdown = 0;
        secondsInt = 0;
        didCntdown = 0;
        timerText.text = "Press The Button";

        updatedFile = 0;
        if (File.Exists(filename))  // If test.txt already exists...
        {
            File.Delete(filename);  // ...I delete it so that all the previous data stored is deleted. The file is created again when I send data to it, but it is empty.
        }

        //Countdown(startTime);
    }

    // Update is called once per frame
    void Update() {
        stopTime = Compass.GetComponent<compassRotation>().stopRotating;    // Getting the value of stopRotating from the compassRotation script.
        if (stopTime == 1) {
            timerText.color = Color.red;

            if (updatedFile == 0) {   // If I haven't printed the data to the file yet...
                textToWrite = textToWrite + " " + timerText.text.ToString() + "\n";    // Create a proper string of the time's value.
                File.AppendAllText(filename, textToWrite);  // Print it to the file.
                updatedFile = 1;
            }
            return;
        }

        toCountdown = Compass.GetComponent<restart>().toRestart;
        if (toCountdown == 1)
        {
            //unfinCD = 1;
            didCntdown = 0;
            
            StartCoroutine(Countdown());
            //unfinCD = 0;
            //didCntdown = 1;
            //toCountdown = 0;
            //startTime = Time.time;
        }

        if (didCntdown == 1)
        {
            passedTime = Time.time - startTime;  // Gives the time in seconds since timer started.

            string minutes = ((int)passedTime / 60).ToString();  // gives number of minutes by dividing the integer value of t by 60.
            string seconds = ((int)passedTime % 60).ToString();
            string miliseconds = ((int)(passedTime * 100f) % 100).ToString();

            minutesInt = ((int)passedTime / 60);
            secondsInt = ((int)passedTime % 60);
            milisecInt = ((int)(passedTime * 100f) % 100);

            if (minutesInt < 10)
            {
                minutes = "0" + minutes;
            }
            if (secondsInt < 10)
            {
                seconds = "0" + seconds;
            }
            if (milisecInt < 10)
            {
                miliseconds = "0" + miliseconds;
            }

            timerText.text = minutes + ":" + seconds + ":" + miliseconds;
        }
        //else
        //{
        //    timerText.text = "Press The Button";
        //}
    }




    IEnumerator Countdown()
    {
        //startTime = Time.time;
        secondsInt = 0;
        while (secondsInt <= 3)
        {
            //passedTime = Time.time - startTime;  // Gives the time in seconds since timer started.
            //secondsInt = ((int)passedTime % 60);

            if (secondsInt == 0)
            {
                timerText.text = "3";
                secondsInt = 1;
            }
            else if (secondsInt == 1)
            {
                timerText.text = "2";
                secondsInt = 2;
            }
            else if (secondsInt == 2)
            {
                timerText.text = "1";
                secondsInt = 3;
            }
            else
            {
                timerText.text = "GO!";
                secondsInt = 4;
            }
            yield return new WaitForSeconds(1);    // Suspends the coroutine execution for the given amount of seconds using scaled time.
            //secondsInt++;
        }
        unfinCD = 0;
        didCntdown = 1;
        toCountdown = 0;
        startTime = Time.time;
    }





}

/*
public int Countdown(float startTime) {
    float passedTime; 
    int secondsInt = 0;

    while(secondsInt <= 3)
    {
        passedTime = Time.time - startTime;  // Gives the time in seconds since timer started.
        secondsInt = ((int)passedTime % 60);
        if(secondsInt == 0) {
            timerText.text = "3";
        }
        else if(secondsInt == 1) {
            timerText.text = "2";
        }
        else if (secondsInt == 2) {
            timerText.text = "1";
        }
        else {
            timerText.text = "GO!";
        }
    }
    return 1;    // == "didCntdown = 1;"
}
*/
