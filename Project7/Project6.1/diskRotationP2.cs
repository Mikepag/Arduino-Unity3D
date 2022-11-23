using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.IO;
using System;

public class diskRotationP2 : MonoBehaviour
{
    //__________________________________________________Variable Declaration:__________________________________________________
    public GameObject Disk;

    //Recieving data from the arduino:
    SerialPort sp = new SerialPort("COM3", 9600);

    private const int MinLeftDistance = -32;            // Used for setting boundaries for the Input value.
    private const int MaxLeftDistance = -2;             // Used for setting boundaries for the Input value.
    private const int MinRightDistance = 2;             // Used for setting boundaries for the Input value.
    private const int MaxRightDistance = 32;            // Used for setting boundaries for the Input value.
    private const int MaxAOR = 25;                      // MaxAngleOfRotation. Maximum angle the Disk can be rotated in one timestep. (unit of measurement: degrees/timestep)
    private const int SIZE = 15;                        // previousPositions[] size.
    private const int maxDampedRotationDuration = 20;   // Maximum number of timesteps the Disk's damped rotation can last.

    private int timestepsSinceOutOfBounds;              // Counter of the elapsed timesteps since the hand position went out of the predefined boundaries.

    private int previous_resBtnClicked;                 // Used for saving the previous value of the resBtnClicked variable from the restartP2.cs script. (I use it to know when the restart button gets clicked by compairing it to the current resBtnClicked).
    private int resBtnClicked;                          // Used for saving the current value of the resBtnClicked variable from the restartP2.cs script. (resBtnClicked contains the last round's number in which the restart button got clicked).
    private int unfinCD;                                // Used for saving the value of the unfinCD variable from the timer.cs script.
    private int didCntdown;                             // Used for saving the value of the didCntdown variable from the timer.cs script.

    public int goalReached;                             // ==1 when the goal is reached, ==0 when the goal is not reached yet.

    private int randomAngle;                            // Gets a "random" value which is used when rotating the disk each time the restart button is clicked.
    private int currentAngle;                           // Gets the disk's current rotation value of Y Axis (Disk.transform.localRotation.eulerAngles.y).
    public float angleOfRotation;                       // Contains the value in which the disk will be rotated.
    public int currentPosition;                         // Variable that gets input from Arduino.

    private int[] previousPositions = new int[SIZE];    // (Circular) Array containing the [SIZE] most recent previous hand positions.
    public int averagePreviousPosition;                 // Average value of all array's values.
    private int arrIn;                                  // Is equal to the number of the array's cell into which we can insert data. When the array is full, the oldest value gets overwritten.
    private int currentArrSize;                         // Is equal to the array's current size (number of not empty cells). Used for calculating average value.

    public float initialAngleOfDampedRotation;          // Gets the last angleOfRotation value before the hand position went out of the predefined boundaries. Used as the initial speed for the damped rotation.
    private int dampedRotationDuration;                 // Duration of the damped rotation in timesteps.


    //__________________________________________________START():__________________________________________________
    void Start()    // Use this for initialization
    {
        previous_resBtnClicked = 0;                     // The restart button has not been clicked in the previous frame.
        resBtnClicked = 0;                              // The restart button has not been clicked yet.
        goalReached = 0;
        angleOfRotation = 0;
        averagePreviousPosition = 0;
        arrIn = 0;
        currentArrSize = 0;
        timestepsSinceOutOfBounds = 0;
        dampedRotationDuration = 0;
    }


    //__________________________________________________UPDATE():__________________________________________________
    void Update()   // Update is called once per frame
    {
        currentAngle = (int)Disk.transform.localRotation.eulerAngles.y;     // Get the current Y-Axis rotation value.

        previous_resBtnClicked = resBtnClicked;                             // Save the previous frame's resBtnClicked value.
        resBtnClicked = Disk.GetComponent<restartP2>().resBtnClicked;       // Getting the current value of resBtnClicked from the restartP2.cs script.
        unfinCD = Disk.GetComponent<timerP2>().unfinCD;                     // Getting the value of unfinCD from the timer.cs script.
        didCntdown = Disk.GetComponent<timerP2>().didCntdown;               // Getting the value of didCntdown from the timer.cs script.

        if ((resBtnClicked - previous_resBtnClicked) == 1)                  // If the restart button has just been clicked...
        {
            randomAngle = UnityEngine.Random.Range(0, 180);                 // Returns a random integer number between 0 and 179.
            if (randomAngle >= 90)
            {
                randomAngle += 180;                                         // I want randomAngle to take values in: [0,90) or in [270,360). So, if  90<=randomAngle<180 I add 180 and it becomes: 180<=randomAngle<270.
            }
            transform.localRotation = Quaternion.Euler(0, randomAngle, 0);  // Set Disk's rotation of Y axis to the randomAngle.
            goalReached = 0;                                                // Every time the restart button gets clicked, a new round starts in which the goal has not been reached yet.
            DeletePreviousPositions();                                      // Call DeletePreviousPositions() to delete all array's values.
        }
        else if (goalReached == 0 && unfinCD == 0 && didCntdown == 1)       //Else, if the the goal is Not reached yet AND the countdown is not currently taking place AND there was a countdown already...
        {
            CheckRotation();                                                //...Call CheckRotation() to receive input from Arduino, check if the disk should be rotated, and if yes, rotate it accordingly.
        }
    }


    //__________________________________________________CHECKROTATION():__________________________________________________
    void CheckRotation()
    {
        sp.Open();                                                                                                  // Open the Serial Port to get current hand position value.
        currentPosition = sp.ReadByte();                                                                            // Get the current hand position value as input from Serial Port.
        sp.Close();                                                                                                 // Close the Serial Port so position values don't get stored to it in between rounds and during the countdown causing "random rotation" and lag.
        currentPosition -= 32;                                                                                      // I added 32 before sending it here, so I have to subtract 32 now to get the real value.

        if ((currentPosition >= MinLeftDistance && currentPosition <= MaxLeftDistance) || (currentPosition >= MinRightDistance && currentPosition <= MaxRightDistance))
        {                                                                                                           // If the input value is between the boundaries...
            timestepsSinceOutOfBounds = 0;

            previousPositions[arrIn] = currentPosition;                                                             //...Add the input to the array.

            if (currentArrSize < SIZE)
            {
                currentArrSize++;                                                                                   // Increment currentArrSize every time a new value gets added, until currentArrSize == SIZE.
            }

            angleOfRotation = GetAngleOfRotation();                                                                 // Call GetAngleOfRotation() to get the desired angle (and direction) of the rotation.
            DiskRotation(angleOfRotation);                                                                          // Call DiskRotation() to rotate the Disk.

            if (angleOfRotation > -1 && angleOfRotation < 1 && currentAngle >= 175 && currentAngle <= 185)          // If while the hand position is in bounds, the disk's angle is inside the target area and the angleOfRotation is very small (<1°/frame)...
            {
                goalReached = 1;                                                                                    //...The goal has been successfully reached (for this round).
            }

            arrIn++;                                                                                                // Increment array's counter.
            if (arrIn > (SIZE - 1))                                                                                 // If the end of the array is reached...
            {
                arrIn = 0;                                                                                          //..."Point" to the first cell again.
            }
        }
        else                                                                                                        // Else, if the input is out of bounds...
        {
            if (timestepsSinceOutOfBounds == 0)                                                                     // If it is the first time the currentPosition is out of bounds...
            {
                initialAngleOfDampedRotation = angleOfRotation;                                                     // Save the last calculated angleOfRotation to be used as the initialAngleofDampedRotation.
                dampedRotationDuration = (int)(Math.Abs(initialAngleOfDampedRotation));                             // Geting the integer absolute value of initialAngleOfRotation. 
                if (dampedRotationDuration > maxDampedRotationDuration)
                {
                    dampedRotationDuration = maxDampedRotationDuration;                                             // If the calculated duration is longer than the predefined maximum duration, set it to the maximum value.
                }
            }

            if (timestepsSinceOutOfBounds < dampedRotationDuration)                                                 // If the timestepsSinceOutOfBounds haven't reached the maximum rotationDuration value...
            {
                DampedDiskRotation(initialAngleOfDampedRotation, timestepsSinceOutOfBounds);                        //...Call DampedDiskRotation() to rotate the Disk.
            }
            else if (currentAngle >= 175 && currentAngle <= 185 && (resBtnClicked - previous_resBtnClicked) == 0)   // Else, If after the damped rotation stopped, the currentAngle is between 175 and 185 and the button has not been clicked yet to start the next round...
            {
                goalReached = 1;                                                                                    //...The goal has been successfully reached (for this round).
            }

            if (timestepsSinceOutOfBounds >= 2 || goalReached == 1)                                                 // If 2 timesteps have passed since the time in which the hand position went out of bounds, OR the goal has been reached...
            {
                DeletePreviousPositions();                                                                          //...Call DeletePreviousPositions() to delete all array's values.
            }

            timestepsSinceOutOfBounds++;                                                                            // Increment the timesteps since the timestamp in which the input value went out of bounds.
        }
    }


    //_________________________________________________________________________________________________________________________
    //__________________________________________________GETANGLEOFROTATION():__________________________________________________
    float GetAngleOfRotation()
    {
        averagePreviousPosition = 0;
        if (currentArrSize > 0)                                                                                     // To avoid the division with zero when the array is empty. (Not really needed since the function is only called if there is at least one value in the array.)
        {
            for (int i = 0; i < currentArrSize; i++)                                                                // Calculating average value for all array's "not-empty" cells:
            {
                averagePreviousPosition += previousPositions[i];
            }
            averagePreviousPosition = averagePreviousPosition / currentArrSize;
        }

        // angleOfRotation gets a value based on the difference between the latest hand position value and the array's average value. The value expresses the direction and total degrees of rotation angle.
        // The variable's value range is [-MaxAOR,MaxAOR].
        angleOfRotation = (float)(MaxAOR * (currentPosition - averagePreviousPosition)) / 64;
        angleOfRotation = -angleOfRotation;                                                                         // Setting angleOfRotation to its opposite value so the disk rotates in the right direction. aTR<0 == CW, aTR>0 == CCW.

        return angleOfRotation;
    }


    //__________________________________________________DISKROTATION():__________________________________________________
    void DiskRotation(float angleOfRotation)    // Call DiskRotation() to rotate the Disk while the hand position is between the boundaries.
    {
        // --- DISK ROTATION ---
        Disk.transform.Rotate(0, angleOfRotation, 0);   // Rotates the disk in the Y-Axis in the direction and degrees provided by angleOfRotation.
    }


    //__________________________________________________DAMPEDDISKROTATION():__________________________________________________
    void DampedDiskRotation(float initialAngleOfDampedRotation, int timestepsSinceOutOfBounds) // Call DampedDiskRotation() to smoothly stop (dampen) the rotating Disk after the hand position stopped being between the boundaries.
    {
        // Automatically calculates the angleOfRotation's decreasing rate as a percentage of the initial angle of rotation to control how long the rotation is going to last. Starts with 100% and finishes with 0% of iAODR.
        // aOR = iAODR * (100 - (100 * ts/dur))/100, OR: aOR = iAODR * (1 - ts/dur), OR: aOR = iAODR * X%, where X is the appropriate percentage (e.g: aOR = iAODR * 50%, aOR = iAODR * 95% etc.).
        // Removes the appropriate percentage from the initial angle of rotation for every elapsed timestep, so the angleOfRotation decreases and reaches 0 (0% of the iAODR) when the maximum rotation's duration is reached:
        angleOfRotation = initialAngleOfDampedRotation * (100 - (100 / dampedRotationDuration * (1 + timestepsSinceOutOfBounds))) / 100;

        // --- DISK ROTATION ---
        Disk.transform.Rotate(0, angleOfRotation, 0);   // Rotates the disk in the Y-Axis in the direction and degrees provided by angleOfRotation.
    }


    //__________________________________________________DELETEPREVIOUSPOSITIONS():__________________________________________________
    void DeletePreviousPositions()  // Function used for "deleting" all recent values from the array.
    {
        arrIn = 0;                  // Put the next input value to the first array's cell.
        currentArrSize = 0;         // Array's size == 0 means that array is currently empty.
    }
}
