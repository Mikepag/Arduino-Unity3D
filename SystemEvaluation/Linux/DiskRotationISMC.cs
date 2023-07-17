using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.IO;

public class DiskRotationISMC : MonoBehaviour
{
    //__________________________________________________Variable Declaration:__________________________________________________
    public GameObject Disk;

    //Recieving data from the arduino:
    SerialPort sp = new SerialPort("/dev/ttyACM0", 9600);

    private const int MinLeftDistance = -32;    // Used for setting boundaries for the Input value.
    private const int MaxLeftDistance = -2;     // Used for setting boundaries for the Input value.
    private const int MinRightDistance = 2;     // Used for setting boundaries for the Input value.
    private const int MaxRightDistance = 32;    // Used for setting boundaries for the Input value.

    private int previous_resBtnClicked;         // Used to save the previous value of the resBtnClicked variable from the restartP3.cs script. (I use it to know when the restart button gets clicked by compairing it to the current resBtnClicked).
    private int resBtnClicked;                  // Used to save the current value of the resBtnClicked variable from the restartP3.cs script. (resBtnClicked contains the last round's number in which the restart button got clicked).
    private int unfinCD;                        // Used to save the value of the unfinCD variable from the timerP3.cs script.
    private int didCntdown;                     // Used to save the value of the didCntdown variable from the timerP3.cs script.

    public int goalReached;                     // ==1 when the goal is reached, ==0 when the goal is not reached yet.

    private int randomAngle;                    // Gets a "random" value which is used when rotating the disk each time the restart button is clicked.
    private int currentAngle;                   // Gets the disk's current rotation value of Y Axis (Disk.transform.localRotation.eulerAngles.y).
    public float angleOfRotation;               // Contains the value in which the disk will be rotated.
    public int currentPosition;                 // Variable that gets input from Arduino.



    //__________________________________________________START():__________________________________________________
    void Start()    // Use this for initialization
    {
        previous_resBtnClicked = 0;             // The restart button has not been clicked in the previous frame.
        resBtnClicked = 0;                      // The restart button has not been clicked yet.
        goalReached = 0;
        angleOfRotation = 0;
    }


    //__________________________________________________UPDATE():__________________________________________________
    void Update()   // Update is called once per frame
    {
        currentAngle = (int)Disk.transform.localRotation.eulerAngles.y;     // Get the current Y-Axis rotation value.

        previous_resBtnClicked = resBtnClicked;                             // Save the previous frame's resBtnClicked value.
        resBtnClicked = Disk.GetComponent<RestartISMC>().resBtnClicked;       // Getting the current value of resBtnClicked from the restartISMC.cs script.
        unfinCD = Disk.GetComponent<TimerISMC>().unfinCD;                     // Getting the value of unfinCD from the timerISMC.cs script.
        didCntdown = Disk.GetComponent<TimerISMC>().didCntdown;               // Getting the value of didCntdown from the timerISMC.cs script.

        if ((resBtnClicked - previous_resBtnClicked) == 1)                  // If the restart button has just been clicked...
        {
            randomAngle = Random.Range(0, 180);                             // Returns a random integer number between 0 and 179.
            if (randomAngle >= 90)
            {
                randomAngle += 180;                                         // I want randomAngle to take values in: [0,90) or in [270,360). So, if  90<=randomAngle<180 I add 180 and it becomes  180<=randomAngle<270
            }
            transform.localRotation = Quaternion.Euler(0, randomAngle, 0);  // Set Disk's rotation of Y axis to the randomAngle.
            goalReached = 0;                                                // Every time the restart button gets clicked, a new round starts in which the goal has not been reached yet.
            sp.Open();
        }
        else if (goalReached == 0 && unfinCD == 0 && didCntdown == 1)       // Else, if the the goal is Not reached yet AND the countdown is not currently taking place AND there was a countdown already...
        {
            CheckRotation();                                                // Call CheckRotation() to check if the disk should be rotated.
        }
    }


    //__________________________________________________CHECKROTATION():__________________________________________________
    void CheckRotation()
    {
        //sp.Open();                                                          // Open the Serial Port to get current hand position value.
        currentPosition = sp.ReadByte();                                    // Get the current hand position value as input from Serial Port.
        //sp.Close();                                                         // Close the Serial Port so position values don't get stored to it in between rounds and during the countdown causing "random rotation" and lag.
        currentPosition -= 32;                                              // I added 32 before sending it here, so I have to subtract 32 now to get the real value.

        if ((currentPosition >= MinLeftDistance && currentPosition <= MaxLeftDistance) || (currentPosition >= MinRightDistance && currentPosition <= MaxRightDistance))
        {                                                                   // If the input value is between the boundaries...
            //_____ Joystick _____
            angleOfRotation = (float)currentPosition / (-4);                // Calculate the angle of rotation based on the current hand position.
            Disk.transform.Rotate(0, angleOfRotation, 0);                   // Rotate the Ghost in the Y-Axis in the direction and degrees provided by angleOfRotation.
        }
        else                                                                // Else, if the input is out of bounds...
        {
            if (currentAngle >= 175 && currentAngle <= 185 && (resBtnClicked - previous_resBtnClicked) == 0)
            {                                                               // If the current angle is between 175 and 185 and the button has not been clicked yet to start the next round...
                goalReached = 1;                                            //...The goal has been successfully reached (for this round).
                sp.Close();
            }
        }
    }
}
