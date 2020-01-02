using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.IO;

public class diskRotationP3 : MonoBehaviour
{
    //__________________________________________________Variable Declaration:__________________________________________________
    public GameObject Disk;

    //Recieving data from the arduino:
    SerialPort sp = new SerialPort("COM3", 9600);

    private const int MinLeftDistance = -32;    // Used for setting boundaries for the Input value.
    private const int MaxLeftDistance = -2;     // Used for setting boundaries for the Input value.
    private const int MinRightDistance = 2;     // Used for setting boundaries for the Input value.
    private const int MaxRightDistance = 32;    // Used for setting boundaries for the Input value.
    private const int MaxATR = 25;              // MaxAngleToRotate. Maximum degrees angle the Disk can be rotated.
    private const int SIZE = 15;                // recentValues[] size.

    private int previous_resBtnClicked;         // Used to save the previous value of the resBtnClicked variable from the restartP2.cs script. (I use it to know when the restart button gets clicked by compairing it to the current resBtnClicked).
    private int resBtnClicked;                  // Used to save the current value of the resBtnClicked variable from the restartP2.cs script. (resBtnClicked contains the last round's number in which the restart button got clicked).
    private int unfinCD;                        // Used to save the value of the unfinCD variable from the timer.cs script.
    private int didCntdown;                     // Used to save the value of the didCntdown variable from the timer.cs script.

    public int goalReached;                     // ==1 when the goal is reached, ==0 when the goal is not reached yet.

    private int randomAngle;                    // Gets a "random" value which is used when rotating the disk each time the restart button is clicked.
    private int angle;                          // Gets the disk's current rotation value of Y Axis (Disk.transform.localRotation.eulerAngles.y).
    public float angleToRotate;                 // Contains the value in which the disk will be rotated.
    public int tempInput;                       // Variable that gets input from Arduino.
    private bool rotateCW;                      // Flag to rotate disk Clockwise.
    private bool rotateCCW;                     // Flag to rotate disk Counterclockwise.

    private int[] recentValues = new int[SIZE]; // (Circular) Array containing the [SIZE] most recent input values.
    public int recentAverage;                   // Average value of all array's values.
    private int arrIn;                          // Is equal to the number of the array's cell into which we can insert data. When the array is full, the oldest value gets overwritten.
    private int currentArrSize;                 // Is equal to the array's current size (number of not empty cells). Used for calculating average value.


    //__________________________________________________START():__________________________________________________
    void Start()    // Use this for initialization
    {
        sp.Open();

        previous_resBtnClicked = 0;             // The restart button has not been clicked in the previous frame.
        resBtnClicked = 0;                      // The restart button has not been clicked yet.
        goalReached = 0;
        angleToRotate = 0;
        rotateCW = false;
        rotateCCW = false;
        recentAverage = 0;
        arrIn = 0;
        currentArrSize = 0;
    }


    //__________________________________________________UPDATE():__________________________________________________
    void Update()   // Update is called once per frame
    {
        angle = (int)Disk.transform.localRotation.eulerAngles.y;            // Get the current Y-Axis rotation value.

        previous_resBtnClicked = resBtnClicked;                             // Save the previous frame's resBtnClicked value.
        resBtnClicked = Disk.GetComponent<restartP3>().resBtnClicked;       // Getting the current value of resBtnClicked from the restartP3.cs script.
        unfinCD = Disk.GetComponent<timerP3>().unfinCD;                     // Getting the value of unfinCD from the timerP3.cs script.
        didCntdown = Disk.GetComponent<timerP3>().didCntdown;               // Getting the value of didCntdown from the timerP3.cs script.

        if ((resBtnClicked - previous_resBtnClicked) == 1)                  // If the restart button has been clicked...
        {
            randomAngle = Random.Range(0, 180);                             // Returns a random integer number between 0 and 179.
            if (randomAngle >= 90)
            {
                randomAngle += 180;                                         // I want randomAngle to take values in: [0,90) or in [270,360). So, if  90<=randomAngle<180 I add 180 and it becomes  180<=randomAngle<270
            }
            transform.localRotation = Quaternion.Euler(0, randomAngle, 0);  // Set Disk's rotation of Y axis to the randomAngle.
            goalReached = 0;                                                // Every time the restart button gets clicked, a new round starts in which the goal has not been reached yet.
            ////DeleteRecentValues();                                           // Call DeleteRecentValues() to delete all array's values.
        }


        if (goalReached == 0 && unfinCD == 0 && didCntdown == 1)            // If the the goal is Not reached yet AND the countdown is not currently taking place AND there was a countdown already...
        {
            CheckMotion();                                                  // Call CheckMotion() to check if the disk should be rotated.

            /*
            //_____ Joystick _____
            tempInput = sp.ReadByte();                              // Get input from Serial Port.
            tempInput -= 32;                                        // I added 32 before sending it here, so I have to subtract 32 now to get the real value.
            if ((tempInput >= MinLeftDistance && tempInput <= MaxLeftDistance) || (tempInput >= MinRightDistance && tempInput <= MaxRightDistance))
            {                                                       // If the input value is between the boundaries...
                angleToRotate = -tempInput / 2;                     // Calculate the correct angle to rotate.
                Disk.transform.Rotate(0, angleToRotate, 0);         // Rotate the Ghost in the Y-Axis in the direction and degrees provided by angleToRotate.
            }
            */
        }

        
    }

    //__________________________________________________CHECKMOTION():__________________________________________________
    void CheckMotion()
    {
        tempInput = sp.ReadByte();                              // Get input from Serial Port.
        tempInput -= 32;                                        // I added 32 before sending it here, so I have to subtract 32 now to get the real value.

        if ((tempInput >= MinLeftDistance && tempInput <= MaxLeftDistance) || (tempInput >= MinRightDistance && tempInput <= MaxRightDistance))
        {                                                       // If the input value is between the boundaries...
            //_____ Joystick _____
            angleToRotate = -tempInput / 2;                     // Calculate the correct angle to rotate.
            Disk.transform.Rotate(0, angleToRotate, 0);         // Rotate the Ghost in the Y-Axis in the direction and degrees provided by angleToRotate.
        }
        else                                                            // Else, if the input is out of bounds...
        {
            if (angle >= 175 && angle <= 185 && (resBtnClicked - previous_resBtnClicked) == 0)     // If the current angle is between 175 and 185 and the button has not been clicked yet to start the next round...
            {
                goalReached = 1;                                        //...The goal has been successfully reached (for this round).
            }
        }
    }
}
