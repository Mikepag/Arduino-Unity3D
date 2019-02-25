using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class timer : MonoBehaviour {

    public GameObject Compass;
    public int unfinCD = 0;   // Finished Countdown
    public int didCntdown;  // ==1 when I already counted down, ==0 at the start of the game, when I need to do a countdown.

    public Text timerText;      // Declaring a public Text called timerText
    private float startTime;    // 
    private int minutesInt;
    private int secondsInt;
    private int milisecInt;
    private int updatedFile;    // ==1 when the time has already been writen to test.txt, ==0 when it need to be written.
    private float passedTime;
    private string filename = "Times.txt";
    private string textToWrite = "Times:\n";

    private int goalReached;
    private int resBtnClicked;
    private int roundNum;


    // Use this for initialization
    void Start() {
        //startTime = Time.time;  // Time.time gives us the time since the application started.
        goalReached = 0;
        secondsInt = 0;
        didCntdown = 0;
        roundNum = 0;
        timerText.text = "Press The Button";

        updatedFile = 0;
        if (File.Exists(filename))  // If test.txt already exists...
        {
            File.Delete(filename);  // ...I delete it so that all the previous data stored is deleted. The file is created again when I send data to it, but it is empty.
        }
        File.AppendAllText(filename, textToWrite);  // Print it to the file.

        //Countdown(startTime);
    }

    // Update is called once per frame
    void Update() {
        goalReached = Compass.GetComponent<compassRotation>().goalReached;    // Getting the value of goalReached from the compassRotation.cs script.
        resBtnClicked = Compass.GetComponent<restart>().resBtnClicked;  // Getting the value of resBtnClicked from the restart.cs script.
        roundNum = Compass.GetComponent<restart>().roundNum;  // Getting the value of roundNum from the restart.cs script.

        if (goalReached == 1 && resBtnClicked == 0) {  // If the user reached the goal and has not clicked the restart button yet...
            timerText.color = Color.red;    // Set the timer's text's colour to red

            if (updatedFile != roundNum) {   // If I haven't printed the data to the file yet...
                //textToWrite = textToWrite + " " + timerText.text.ToString() + "\n";    // Create a proper string of the time's value.
                textToWrite = timerText.text.ToString() + "\n";    // Create a proper string of the time's value.
                File.AppendAllText(filename, textToWrite);  // Print it to the file.
                updatedFile = roundNum;
            }
            //else
            //{   // If I have already printed data to the file yet...
            //    textToWrite = "\n" + textToWrite + " " + timerText.text.ToString() + "\n";    // Create a proper string of the time's value.
            //    File.AppendAllText(filename, textToWrite);  // Print it to the file.
            //    updatedFile = 1;
            //}

            //return;
        }

        if (resBtnClicked == 1)
        {
            timerText.color = Color.black;    // Set the timer's text's colour to black
            didCntdown = 0;
            
            StartCoroutine(Countdown());
            //unfinCD = 0;
            //didCntdown = 1;
            //toCountdown = 0;
            //startTime = Time.time;
        }

        if (didCntdown == 1 && goalReached == 0)
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
        unfinCD = 1;
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
