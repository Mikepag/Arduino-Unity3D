using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class timer : MonoBehaviour {

    public GameObject Compass;
    public int unfinCD = 0;                     // ==0 if the Countdown has finished, ==1 while the Countdown is still taking place.
    public int didCntdown;                      // ==1 if there has been a Countdown for this round, ==0 at the start of the round, when I need to start a countdown.

    public Text timerText;                      // Declaring a public Text called timerText
    private float startTime;                    // Gets the time right after the countdown's execution ends. Used to calculate and print the elapsed time on the timer.
    private int minutesInt;                     // Elapsed minutes sinse start of time.
    private int secondsInt;                     // Elapsed seconds sinse start of time.
    private int milisecInt;                     // Elapsed miliseconds since start of time.
    private int updatedFile;                    // Is equal to the round's number the file has last been updated.
    private float elapsedTime;                  // Elapsed time since start of time.
    private string filename = "Times.txt";      // External file where the elapsed time of each round is beeing written.
    private string textToWrite = "Times:\n";    // The text that is beeing written to Times.txt. The first line is "Times:".

    private int goalReached;                    // Used to save the value of the goalReached variable from the compassRotation.cs script.
    private int resBtnClicked;                  // Used to save the value of the resBtnClicked variable from the restart.cs script.
    private int roundNum;                       // Used to save the value of the roundNum variable from the restart.cs script.


    // Use this for initialization
    void Start() {
        goalReached = 0;    // Goal has not been reached yet.
        secondsInt = 0;     // Elapsed seconds are ==0.
        didCntdown = 0;     // There has been no Countdown yet.
        roundNum = 0;       // Current round ==0.
        timerText.text = "Press The Button";    // Print a message to the player to press the button to start the first round.

        if (File.Exists(filename))                  // If Times.txt already exists...
        {
            File.Delete(filename);                  // ...Delete it so that all the previous data stored is deleted. The file is created again when new data is send to it, but it is empty.
        }
        File.AppendAllText(filename, textToWrite);  // Print textToWrite to the file.
        updatedFile = 0;                            // The file has been updated for round 0.
    }

    // Update is called once per frame
    void Update() {
        goalReached = Compass.GetComponent<compassRotation>().goalReached;  // Getting the value of goalReached from the compassRotation.cs script.
        resBtnClicked = Compass.GetComponent<restart>().resBtnClicked;      // Getting the value of resBtnClicked from the restart.cs script.
        roundNum = Compass.GetComponent<restart>().roundNum;                // Getting the value of roundNum from the restart.cs script.

        if (goalReached == 1 && resBtnClicked == 0) {               // If the user reached the goal and has not clicked the restart button yet...
            timerText.color = Color.red;                            //...Set the timer's text's colour to red.

            if (updatedFile != roundNum) {                          // If I haven't printed the data to the file yet in this round...
                textToWrite = timerText.text.ToString() + "\n";     // Create a proper string of the time's value.
                File.AppendAllText(filename, textToWrite);          // Append it to the file (print it without deleting previous data).
                updatedFile = roundNum;                             // I use this to know in which round the file has last been updated.
            }
        }

        if (resBtnClicked == 1)             // If the restart button got clicked...
        {
            timerText.color = Color.black;  // Set the timer's text's colour to black
            didCntdown = 0;                 // There has been no Countdown yet (for this round).

            StartCoroutine(Countdown());    // Start the Countdown.
        }

        if (didCntdown == 1 && goalReached == 0)    // If there has been a Countdown for this round and the goal has not been reached yet...
        {
            elapsedTime = Time.time - startTime;    // Gives the time in seconds since timer started.

            string minutes = ((int)elapsedTime / 60).ToString();                // Calculates number of elapsed minutes by dividing the integer value of elapsedTime by 60. Then it converts it to String.
            string seconds = ((int)elapsedTime % 60).ToString();                // Calculates number of elapsed seconds by dividing the integer value of elapsedTime by 60 and getting its modulo. Then it converts it to String.
            string miliseconds = ((int)(elapsedTime * 100f) % 100).ToString();  // Calculates number of elapsed miliseconds. Then it converts it to String.

            minutesInt = ((int)elapsedTime / 60);           // Calculates integer number of elapsed minutes.
            secondsInt = ((int)elapsedTime % 60);           // Calculates integer number of elapsed seconds.
            milisecInt = ((int)(elapsedTime * 100f) % 100); // Calculates integer number of elapsed miliseconds.

            if (minutesInt < 10)                    // If < 10...
            {
                minutes = "0" + minutes;            //...Add a zero so it looks better.
            }
            if (secondsInt < 10)                    // If < 10...
            {
                seconds = "0" + seconds;            //...Add a zero so it looks better.
            }
            if (milisecInt < 10)                    // If < 10...
            {
                miliseconds = "0" + miliseconds;    //...Add a zero so it looks better.
            }

            timerText.text = minutes + ":" + seconds + ":" + miliseconds;   // Set UI timersText's text to the elapsed minutes:seconds:miliseconds.
        }
    }


    IEnumerator Countdown() // Prints a Countdown.
    {
        unfinCD = 1;        // The countdown has not finished yet.
        secondsInt = 0;     // Counts elapsed seconds since the Countdown started.
        while (secondsInt <= 3)
        {
            if (secondsInt == 0)
            {
                timerText.text = "3";   // Print "3".
                secondsInt = 1;
            }
            else if (secondsInt == 1)
            {
                timerText.text = "2";   // Print "2".
                secondsInt = 2;
            }
            else if (secondsInt == 2)
            {
                timerText.text = "1";   // Print "1".
                secondsInt = 3;
            }
            else
            {
                timerText.text = "GO!";   // Print "GO!".
                secondsInt = 4;
            }
            yield return new WaitForSeconds(1);    // Suspends the coroutine execution for the given amount of seconds using scaled time.
        }
        unfinCD = 0;            // The Countdown has finished.
        didCntdown = 1;         // There has been a Countdown for this round.
        startTime = Time.time;  // Timer starts now.
    }
}
