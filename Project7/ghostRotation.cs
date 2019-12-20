using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;

public class ghostRotation : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600);
    //System.IO.Ports.SerialPort stream = new System.IO.Ports.SerialPort("COM7", 9600);  //ORIGINAL

    const int minLeftDistance = -32;    // Used for setting boundaries for the Input value.
    const int maxLeftDistance = -2;
    const int minRightDistance = 2;
    const int maxRightDistance = 32;

    bool goForward = false;             // Flag to move (rotate) forward (to-the-left).
    bool goBack = false;                // Flag to move (rotate) backward (to-the-right).

    public int tempInput;               // Variable that gets input from Arduino.

    int[] recentValues = new int[10];   // (Circular) Array containing the ten most recent input values.
    public int recentAverage = 0;       // Average value of all array's values.
    int arrIn = 0;                      // Is equal to the number of the array's cell into which we can insert data. When the array is full, the oldest value gets overwritten.
    int arrSize = 0;                    // Is equal to the array's size (number of not empty cells). Used for calculating average value.
    //ADD: float moveDist = 0;


    // Use this for initialization
    void Start()
    {
        sp.Open();
        //stream.Open(); //ORIGINAL
    }


    // Update is called once per frame
    void Update()
    {
        CheckMotion();
    }


    void CheckMotion()
    {
        //float tempInput = float.Parse(stream.ReadLine()); //ORIGINAL
        tempInput = sp.ReadByte();  // Get input from Serial Port.
        tempInput -= 32;            // I added 32 before sending it, so I have to subtract 32 now to get the real value.

        if ((tempInput >= minLeftDistance && tempInput <= maxLeftDistance) || (tempInput >= minRightDistance && tempInput <= maxRightDistance))  // If the input value is between the boundaries...
        {
            recentValues[arrIn] = tempInput;    //...Add the input to the array.

            if (arrSize < 10)
            {
                arrSize++;                          // Increment arrSize every time a new value gets added, until arrSize == 10.
            }

            GetDirection();                         // Call GetDirection() to move the Main Camera.

            arrIn++;                                // Increment array's counter.
            if (arrIn > 9)                          // If the end of the array is reached...
            {
                arrIn = 0;                          //..."Point" to the first cell again.
            }
        }
        else                                        // Else, if the input is out of bounds...
        {
            DeleteRecentValues();                   //...Call DeleteRecentValues() to delete all array's values.
        }
    }


    void GetDirection()
    {
        recentAverage = 0;
        //for (int i = 0; i <= arrIn; i++)          // ORIGINAL
        for (int i = 0; i < arrSize; i++)             // Calculating average value for all array's not-empty cells:
        {
            recentAverage += recentValues[i];
        }

        recentAverage = recentAverage / arrSize;

        if (recentValues[arrIn] > recentAverage)    // If the latest Input value is greater than the average value, moves the camera to the left (wrong, should be right).
        {
            Debug.Log("Back");
            goBack = true;                          // REPLACE "Back" with "Right".
            goForward = false;                      // REPLACE "Forward" with "Left".
        }

        if (recentValues[arrIn] < recentAverage)    // If the latest Input value is smaller than the average value, moves the camera to the right (wrong, should be left).
        {
            Debug.Log("Forward");
            goForward = true;                       // REPLACE "Forward" with "Left".
            goBack = false;                         // REPLACE "Back" with "Right".
        }

        //ADD: if recentValues[] == recentAverage ---> false; false;

        //ADD: moveDist = (recentAverage + recentValues[arrIn]) / 2; // I have to make this work somehow, maybe add another function.

        if (goForward)
            //this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);   // Moves the Main Camera to the Right (should be Left).
            // --- DISK ROTATION ---
            this.transform.Rotate(0, -10, 0);                     // Rotate the disk in the Y-Axis in the direction and degrees provided by angleToRotate.

        if (goBack)
            //this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);   // Moves the Main Camera to the Left (should be Right).
            // --- DISK ROTATION ---
            this.transform.Rotate(0, 10, 0);                     // Rotate the disk in the Y-Axis in the direction and degrees provided by angleToRotate.

        if (goForward || goBack)
        {
            Debug.Log("Recent input: " + recentValues[arrIn] + " & Recent average: " + recentAverage);
        }
    }


    void DeleteRecentValues()   // Function used for "deleting" all recent values from the array.
    {
        arrIn = 0;      // Put the next input value to the first array's cell.
        arrSize = 0;    // Array's size == 0 means that it is empty.
    }
}
