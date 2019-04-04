using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

using System.IO;

public class diskRotation : MonoBehaviour
{
    public GameObject Disk;

    //Recieving data from the arduino:
    SerialPort sp = new SerialPort("COM3", 9600);

    public int goalReached;     // ==1 when the goal is reached, ==0 when the goal is not reached yet.
    private int angle;          // Gets the disk's current rotation value of Y Axis (Disk.transform.localRotation.eulerAngles.y).

    private int resBtnClicked;  // Used to save the value of the resBtnClicked variable from the restart.cs script.
    private int unfinCD;        // Used to save the value of the unfinCD variable from the timer.cs script.
    private int didCntdown;     // Used to save the value of the didCntdown variable from the timer.cs script.
    private int randomAngle;    // Gets a "random" value which is used when rotating the disk each time the restart button is clicked.

    private const int MinLeftDistance = -32;    // Used for setting boundaries for the Input value.
    private const int MaxLeftDistance = -2;
    private const int MinRightDistance = 2;
    private const int MaxRightDistance = 32;

    private bool rotateCW = false;  // Flag to rotate disk Clockwise.
    private bool rotateCCW = false; // Flag to rotate disk Counterclockwise.

    public int tempInput;               // Variable that gets input from Arduino.

    int[] recentValues = new int[10];   // (Circular) Array containing the ten most recent input values.
    public int recentAverage = 0;       // Average value of all array's values.
    int arrIn = 0;                      // Is equal to the number of the array's cell into which we can insert data. When the array is full, the oldest value gets overwritten.
    int arrSize = 0;                    // Is equal to the array's size (number of not empty cells). Used for calculating average value.

    public float angleToRotate = 0;    // Contains the value in which the disk will be rotated.
    const int maxRotationalSpeed = 5;

    // Use this for initialization
    void Start()
    {
        sp.Open();
        //TODELETE: isStationary = 1;
        goalReached = 0;
    }



    // Update is called once per frame
    void Update()
    {
        angle = (int)Disk.transform.localRotation.eulerAngles.y;     // Get the current Y-Axis rotation value.

        resBtnClicked = Disk.GetComponent<restart>().resBtnClicked;  // Getting the value of resBtnClicked from the restart.cs script.
        unfinCD = Disk.GetComponent<timer>().unfinCD;                // Getting the value of unfinCD from the timer.cs script.
        didCntdown = Disk.GetComponent<timer>().didCntdown;          // Getting the value of didCntdown from the timer.cs script.

        if (resBtnClicked == 1)                                             // If the restart button has been clicked...
        {
            randomAngle = Random.Range(0, 180);                             // Returns a random integer number between 0 and 179.
            if (randomAngle >= 90)
            {
                randomAngle += 180;                                         // I want randomAngle to take values in: [0,90) or in [270,360). So, if  90<=randomAngle<180 I add 180 and it becomes  180<=randomAngle<270
            }
            transform.localRotation = Quaternion.Euler(0, randomAngle, 0);  // Set Disk's rotation of Y axis to the randomAngle.
            goalReached = 0;                                                // Every time the restart button gets clicked, a new round starts in which the goal has not been reached yet.
            DeleteRecentValues();                   //...Call DeleteRecentValues() to delete all array's values.
        }


        if (goalReached == 0 && unfinCD == 0 && didCntdown == 1)    // If the the goal is Not reached yet AND the countdown is not currently taking place AND there was a countdown already...
        {
            CheckMotion();           // Call CheckMotion() to check if the disk should be rotated.
        }
    }



    void CheckMotion()
    {
        tempInput = sp.ReadByte();  // Get input from Serial Port.
        tempInput -= 32;            // I added 32 before sending it here, so I have to subtract 32 now to get the real value.

        if ((tempInput >= MinLeftDistance && tempInput <= MaxLeftDistance) || (tempInput >= MinRightDistance && tempInput <= MaxRightDistance))  // If the input value is between the boundaries...
        {
            recentValues[arrIn] = tempInput;    //...Add the input to the array.

            if (arrSize < 10)
            {
                arrSize++;                          // Increment arrSize every time a new value gets added, until arrSize == 10.
            }

            GetDirection();                         // Call GetDirection() to rotate the Disk.

            arrIn++;                                // Increment array's counter.
            if (arrIn > 9)                          // If the end of the array is reached...
            {
                arrIn = 0;                          //..."Point" to the first cell again.
            }
        }
        else                                        // Else, if the input is out of bounds...
        {
            //DELETE THIS: isStationary = 1;   // The disk stops rotating.

            //TODELETE: if (isStationary == 1 && angle >= 175 && angle <= 185 && resBtnClicked == 0)    // If the current angle is between 175 and 185 and the button has not been clicked yet to start the next round...
            if (angle >= 175 && angle <= 185 && resBtnClicked == 0)    // If the current angle is between 175 and 185 and the button has not been clicked yet to start the next round...
            {
                goalReached = 1;    // ...The goal has been successfully reached (for this round).
            }

            DeleteRecentValues();                   //...Call DeleteRecentValues() to delete all array's values.
        }
    }

    void GetDirection()
    {
        //DELETE THIS: isStationary = 0;
        recentAverage = 0;
        for (int i = 0; i < arrSize; i++)             // Calculating average value for all array's "not-empty" cells:
        {
            recentAverage += recentValues[i];
        }
        recentAverage = recentAverage / arrSize;

        if (recentValues[arrIn] > recentAverage)    // If the latest Input value is greater than the average value...
        {
            Debug.Log("Gesture-to-the-Right, Clockwise rotation");
            rotateCW = false; //the disk needs to be rotated Clockwise.
            rotateCCW = true;
        }
        else if (recentValues[arrIn] < recentAverage)    // If the latest Input value is smaller than the average value... the disk needs to be rotated Counterclockwise.
        {
            Debug.Log("Gesture-to-the-Left, Counterclockwise rotation");
            rotateCCW = false;   // the disk needs to be rotated Counterclockwise.
            rotateCW = true;
        }

        //TO DELETE: //ADD: if recentValues[] == recentAverage ---> false; false;

        //TO DELETE: //ADD: moveDist = (recentAverage + recentValues[arrIn]) / 2; // I have to make this work somehow, maybe add another function.


        angleToRotate = (float)(25 * (recentValues[arrIn] - recentAverage)) / 64;   // Gets a value based on the difference between the latest Input value and array's average value. The value expresses the direction and total degrees of rotation angle...
                                                                                    //...I also change the variable's value's range from [-64,64] to [-MaxATR,MaxATR].
        angleToRotate = -angleToRotate; // Setting angleToRotate to its opposite value so the disk rotates in the right direction. aTR<0 == CW, aTR>0 == CCW.

        if (angleToRotate < 1 && angleToRotate > 0) // If 0<angleToRotate<1...
        {
            angleToRotate = 1;  //...I set angleToRotate back to 1. That helps by rotating the disk even for very small hand gestures and improves accuracy.
        }
        else if (angleToRotate > -1 && angleToRotate < 0)   // If -1<angleToRotate<0...
        {
            angleToRotate = -1; //...I set angleToRotate back to -1. That helps by rotating the disk even for very small hand gestures and improves accuracy.
        }
        else if (angleToRotate == 0 && rotateCW)    // When angleToRotate reaches 0, I need the disk to continue rotating (really slow) in the same direction for as long as my hand is detected inside the boundaries.
        {
            angleToRotate = 1; // If rotateCW == true, I set angleToRotate to 1 so it rotates 1 degree Clockwise.
        }
        else if (angleToRotate == 0 && rotateCCW)   // When angleToRotate reaches 0, I need the disk to continue rotating (really slow) in the same direction for as long as my hand is detected inside the boundaries.
        {
            angleToRotate = -1; // If rotateCCW == true, I set angleToRotate to 1 so it rotates 1 degree Counterclockwise.
        }


        //if (goBack)
        //{
        //    //angleToRotate = (float)(maxRotationalSpeed / ((recentValues[arrIn] +1) - recentAverage));
        //    Disk.transform.Rotate(0, -5, 0);
        //}

        //if (goForward)
        //{
        //    Disk.transform.Rotate(0, + 5, 0);
        //}

        //if (rotateCW || rotateCCW)
        //{
            Disk.transform.Rotate(0, angleToRotate, 0); // Rotate the disk in the Y-Axis in the direction and degrees provided by angleToRotate.
            //Debug.Log("Recent input: " + recentValues[arrIn] + " & Recent average: " + recentAverage);
        //}
    }

    void DeleteRecentValues()   // Function used for "deleting" all recent values from the array.
    {
        arrIn = 0;      // Put the next input value to the first array's cell.
        arrSize = 0;    // Array's size == 0 means that it is empty.
    }
}
