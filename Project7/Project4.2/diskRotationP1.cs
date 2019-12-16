using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class diskRotationP1 : MonoBehaviour
{
    public GameObject Disk;

    //Recieving data from the arduino:
    SerialPort sp = new SerialPort("COM3", 9600);

    public int goalReached;             // ==1 when the goal is reached, ==0 when the goal is not reached yet.

    private int directionSteps;         // Every time arduino sends a value via the Serial Port, that value is saved in the directionSteps variable. (==1-10 for right-to-left gestures, ==11-20 for left-to-right gestures, ==0 to not move).
    private int timesteps;              // Is used to suspend the coroutine execution.
    private int isStationary;           // ==0 when the disk is Stationary, ==1 when the disk is currently rotating and has not finished its rotation.
    private int angle;                  // Gets the disk's current rotation value of Y Axis (Disk.transform.localRotation.eulerAngles.y).
    private int resBtnClicked;          // Used to save the current value of the resBtnClicked variable from the restartP1.cs script. (resBtnClicked contains the last round's number in which the restart button got clicked).
    private int previous_resBtnClicked; // Used to save the previous frame's value of the resBtnClicked variable from the restartP1.cs script. (I use it to know when the restart button gets clicked by compairing it to the current resBtnClicked).
    private int unfinCD;                // Used to save the value of the unfinCD variable from the timer.cs script.
    private int didCntdown;             // Used to save the value of the didCntdown variable from the timer.cs script.
    private int randomAngle;            // Gets a "random" value which is used when rotating the disk each time the restart button is clicked.


    // Use this for initialization
    void Start()
    {
        sp.Open();                      // Opens a new serial port connection.
        isStationary = 1;               // The disk is currently stationary
        goalReached = 0;                // The goal has not been reached.
        previous_resBtnClicked = 0;     // The restart button has not been clicked in the previous frame.
        resBtnClicked = 0;              // The restart button has not been clicked yet.
    }


    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            angle = (int)Disk.transform.localRotation.eulerAngles.y;            // Get the current Y Axis rotation value.

            unfinCD = Disk.GetComponent<timerP1>().unfinCD;                     // Getting the value of unfinCD from the timer.cs script.
            didCntdown = Disk.GetComponent<timerP1>().didCntdown;               // Getting the value of didCntdown from the timer.cs script.

            previous_resBtnClicked = resBtnClicked;                             //Save the previous frame's resBtnClicked value.
            resBtnClicked = Disk.GetComponent<restartP1>().resBtnClicked;       // Getting the current value of resBtnClicked from the restart.cs script.
            //////

            if ((resBtnClicked - previous_resBtnClicked) == 1)
            {                                                                   // If the restart button has been clicked...
                randomAngle = Random.Range(0, 180);                             // Returns a random integer number between 0 and 179.
                if (randomAngle >= 90)
                {
                    randomAngle += 180;                                         // I want randomAngle to take values in: [0,90) or in [270,360). So, if  90<=randomAngle<180 I add 180 and it becomes  180<=randomAngle<270
                }
                transform.localRotation = Quaternion.Euler(0, randomAngle, 0);  // Set disk's rotation of Y axis to the randomAngle.
                goalReached = 0;                                                // Every time the restart button gets clicked, a new round starts in which the goal has not been reached yet.
            }

            if (isStationary == 1)
            {                                                                   // If the disk is not currently rotating...
                directionSteps = sp.ReadByte();                                 // ...Get value from Arduino via Serial Port.
            }

            if (goalReached == 0 && unfinCD == 0 && didCntdown == 1)
            {                                                                   // If the the goal is Not reached yet AND the countdown is not currently taking place AND there was a countdown already...
                StartCoroutine(RotateObject(directionSteps));                   // Call RotateObject() to rotate the disk.
            }
        }
    }


    IEnumerator RotateObject(int directionSteps)
    {
        if (directionSteps > 0 && directionSteps <= 10)
        {                                                                           // If the value sent from the arduino is >0 and <=10 (Right to Left rotation)
            timesteps = 10 - directionSteps;                                        // The bigger the value of directionSteps, the slower the gesture was so I want the disk's rotation to last for a smaller amount of timesteps.
            directionSteps = (11 - directionSteps) * 7;                             // The bigger the value of directionSteps, the slower the gesture was so I want the disk to rotate less (That's why I use 11-diractionSteps). The " *7" is there so the total angle is not too small.
            while (timesteps < 10)
            {
                isStationary = 0;                                                   // Because the disk is currently rotating.
                transform.Rotate(Vector3.up * directionSteps * Time.deltaTime);     // Rotate the disk.
                yield return new WaitForSeconds(0.05F);                             // Suspends the coroutine execution for the given amount of seconds using scaled time. I use this so the rotation takes some time to complete instead of beeing instant.
                timesteps++;
                if (timesteps >= 10)
                {
                    isStationary = 1;                                               // When timesteps==10, the rotation terminates, so the disk is stationary.
                }
            }
        }
        else if (directionSteps > 10)
        {                                                                           // Else, if the value sent from the arduino is >10 (Left to Right rotation). directionSteps is > 10 because I added "10" to seperate it from the Right to Left rotation, so I have to subtract 10 before using it.
            timesteps = 10 - (directionSteps - 10);                                 // The bigger the value of directionSteps, the slower the gesture was so I want the disk's rotation to last for a smaller amount of timesteps.
            directionSteps = (11 - (directionSteps - 10)) * 7;                      // The bigger the value of directionSteps, the slower the gesture was so I want the disk to rotate less (That's why I use 11-diractionSteps). The " *7" is there so the total angle is not too small.
            while (timesteps < 10)
            {
                isStationary = 0;                                                   // Because the disk is currently rotating.
                transform.Rotate(Vector3.down * directionSteps * Time.deltaTime);   // Rotate the disk.
                yield return new WaitForSeconds(0.05F);                             // Suspends the coroutine execution for the given amount of seconds using scaled time. I use this so the rotation takes some time to complete instead of beeing instant.
                timesteps++;
                if (timesteps >= 10)
                {
                    isStationary = 1;                                               // When timesteps==10, the rotation terminates, so the disk is stationary.
                }
            }
        }

        if (isStationary == 1 && angle >= 175 && angle <= 185 && ((resBtnClicked - previous_resBtnClicked) == 0))
        {                             // If the current angle is between 175 and 185 and the button has not been clicked yet to start the next round...
            goalReached = 1;          // ...The goal has been successfully reached (for this round).
        }
    }
}
