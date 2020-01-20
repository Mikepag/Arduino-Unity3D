using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class timerP3 : MonoBehaviour
{

    public GameObject Disk;
    public int unfinCD = 0;                         // ==0 if the Countdown has finished, ==1 while the Countdown is still taking place.
    public int didCntdown;                          // ==1 if there has been a Countdown for this round, ==0 at the start of the round, when I need to start a countdown.

    public Text timerText;                          // Declaring a public Text called timerText
    private float startTime;                        // Gets the time right after the countdown's execution ends. Used to calculate and print the elapsed time on the timer.
    private int minutesInt;                         // Elapsed minutes sinse start of time.
    private int secondsInt;                         // Elapsed seconds sinse start of time.
    private int millisecInt;                        // Elapsed milliseconds since start of time.
    private int updatedFile;                        // Is equal to the round's number the file has last been updated.
    private string filename = "LogTimes_P3.txt";    // External file where the elapsed time of each round is beeing written.
    private string textToWrite = "Times:\n";        // The text that is beeing written to LogTimes_P2.txt. The first line is "Times:".
    private DateTime startSysTime;                  // Gets a System Time timestamp at the start of each round.
    private DateTime currentSysTime;                // Gets a System Time timestamp once for each frame.

    private int goalReached;                        // Used to save the value of the goalReached variable from the diskRotationP1.cs script.
    private int roundNum;                           // Used to save the value of the roundNum variable from the restartP2.cs script.
    private int resBtnClicked;                      // Used to save the current value of the resBtnClicked variable from the restartP2.cs script. (resBtnClicked contains the last round's number in which the restart button got clicked).
    private int previous_resBtnClicked;             // Used to save the previous value of the resBtnClicked variable from the restartP2.cs script. (I use it to know when the restart button gets clicked by compairing it to the current resBtnClicked).

    // Use this for initialization
    void Start()
    {
        previous_resBtnClicked = 0;                 // The restart button has not been clicked yet.
        resBtnClicked = 0;                          // The restart button has not been clicked yet.
        goalReached = 0;                            // Goal has not been reached yet.
        secondsInt = 0;                             // Elapsed seconds are ==0.
        didCntdown = 0;                             // There has been no Countdown yet.
        roundNum = 0;                               // Current round ==0.
        timerText.text = "Press The Button";        // Print a message to the player to press the button to start the first round.

        if (File.Exists(filename))
        {                                           // If Times.txt already exists...
            File.Delete(filename);                  // ...Delete it so that all the previous data stored is deleted. The file is created again when new data is send to it, but it is empty.
        }
        File.AppendAllText(filename, textToWrite);  // Print textToWrite to the file.
        updatedFile = 0;                            // The file has been updated for round 0.
    }

    // Update is called once per frame
    void Update()
    {
        goalReached = Disk.GetComponent<diskRotationP3>().goalReached;          // Getting the value of goalReached from the diskRotationP3.cs script.
        roundNum = Disk.GetComponent<restartP3>().roundNum;                     // Getting the value of roundNum from the restartP3.cs script.

        previous_resBtnClicked = resBtnClicked;                                 //Save the previous frame's resBtnClicked value.
        resBtnClicked = Disk.GetComponent<restartP3>().resBtnClicked;           // Getting the current value of resBtnClicked from the restartP3.cs script.

        if (goalReached == 1 && (resBtnClicked - previous_resBtnClicked) == 0)
        {                                                                       // If the user reached the goal and has not clicked the restart button yet (in the current round)...
            timerText.color = Color.red;                                        //...Set the timer's text's colour to red.

            if (updatedFile != roundNum)
            {                                                                   // If I haven't printed the data to the file yet in this round...
                if (roundNum == 1)
                {
                    updatedFile = roundNum;                                     // The first round's time is not saved to the external file.
                }
                else
                {
                    textToWrite = timerText.text.ToString() + "\n";             // Create a proper string of the time's value.
                    File.AppendAllText(filename, textToWrite);                  // Append it to the file (print it without deleting previous data).
                    updatedFile = roundNum;                                     // I use this to know in which round the file has last been updated.
                }
            }
        }

        if ((resBtnClicked - previous_resBtnClicked) == 1)
        {                                                                       // If the restart button got clicked (in the current round)...
            timerText.color = Color.black;                                      // Set the timer's text's colour to black
            didCntdown = 0;                                                     // There has been no Countdown yet (for this round).

            StartCoroutine(Countdown());                                        // Start the Countdown.
        }

        if (didCntdown == 1 && goalReached == 0)
        {                                                                                   // If there has been a Countdown for this round and the goal has not been reached yet...
            currentSysTime = System.DateTime.Now;                                           // Get a timestamp of the current System Time.

            minutesInt = (int)(currentSysTime - startSysTime).TotalMinutes;                 // Calculates integer number of elapsed minutes using two timestamps of System Time.
            secondsInt = ((int)(currentSysTime - startSysTime).TotalSeconds) % 60;          // Calculates integer number of elapsed seconds using two timestamps of System Time.
            millisecInt = ((int)(currentSysTime - startSysTime).TotalMilliseconds) % 1000;  // Calculates integer number of elapsed milliseconds using two timestamps of System Time.

            string minutes = minutesInt.ToString();                             // Converts integer value of elapsed minutes to String.
            string seconds = secondsInt.ToString();                             // Converts integer value of elapsed seconds to String.
            string milliseconds = millisecInt.ToString();                       // Converts integer value of elapsed milliseconds to String.

            if (minutesInt < 10)
            {                                                                   // If < 10...
                minutes = "0" + minutes;                                        //...Add a zero so it looks better.
            }
            if (secondsInt < 10)
            {                                                                   // If < 10...
                seconds = "0" + seconds;                                        //...Add a zero so it looks better.
            }
            if (millisecInt < 100)
            {                                                                   // If < 100...
                milliseconds = "0" + milliseconds;                              //...Add a zero so it looks better.
                if (millisecInt < 10)
                {                                                               // If also < 10...
                    milliseconds = "0" + milliseconds;                          //...Add another zero so it looks better.
                }
            }

            timerText.text = minutes + ":" + seconds + "," + milliseconds;      // Set UI timersText's text to the elapsed minutes:seconds:milliseconds.
        }
    }


    IEnumerator Countdown() // Prints a Countdown.
    {
        unfinCD = 1;                                // The countdown has not finished yet.
        secondsInt = 0;                             // Counts elapsed seconds since the Countdown started.
        while (secondsInt <= 3)
        {
            if (secondsInt == 0)
            {
                timerText.text = "3";               // Print "3".
                secondsInt = 1;
            }
            else if (secondsInt == 1)
            {
                timerText.text = "2";               // Print "2".
                secondsInt = 2;
            }
            else if (secondsInt == 2)
            {
                timerText.text = "1";               // Print "1".
                secondsInt = 3;
            }
            else
            {
                timerText.text = "GO!";             // Print "GO!".
                secondsInt = 4;
            }
            yield return new WaitForSeconds(1);     // Suspends the coroutine execution for the given amount of seconds using scaled time.
        }
        unfinCD = 0;                                // The Countdown has finished.
        didCntdown = 1;                             // There has been a Countdown for this round.
        startSysTime = System.DateTime.Now;         // Timer starts now.
    }
}
