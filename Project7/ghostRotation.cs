using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ghostRotation : MonoBehaviour
{
    //__________________________________________________Variable Declaration:__________________________________________________
    public GameObject Ghost;

    //Recieving data from the arduino:
    SerialPort sp = new SerialPort("COM3", 9600);

    private const int MinLeftDistance = -32;    // Used for setting boundaries for the Input value.
    private const int MaxLeftDistance = -2;     // Used for setting boundaries for the Input value.
    private const int MinRightDistance = 2;     // Used for setting boundaries for the Input value.
    private const int MaxRightDistance = 32;    // Used for setting boundaries for the Input value.
    private const int MaxATR = 25;              // MaxAngleToRotate. Maximum degrees angle the Ghost can be rotated.
    private const int SIZE = 15;                // recentValues[] size.

    private int randomAngle;                    // Gets a "random" value which is used when rotating the Ghost each time the restart button is clicked.
    public float angleToRotate;                 // Contains the value in which the Ghost will be rotated.
    public int tempInput;                       // Variable that gets input from Arduino.
    private bool rotateCW;                      // Flag to rotate Ghost Clockwise.
    private bool rotateCCW;                     // Flag to rotate Ghost Counterclockwise.

    private int[] recentValues = new int[SIZE]; // (Circular) Array containing the [SIZE] most recent input values.
    public int recentAverage;                   // Average value of all array's values.
    private int arrIn;                          // Is equal to the number of the array's cell into which we can insert data. When the array is full, the oldest value gets overwritten.
    private int currentArrSize;                 // Is equal to the array's current size (number of not empty cells). Used for calculating average value.


    //__________________________________________________START():__________________________________________________
    void Start()    // Use this for initialization
    {
        sp.Open();

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
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Assets/Scenes/Menu.unity"); // Load Menu Scene.
        }


        //CheckMotion();                                                  // Call CheckMotion() to check if the Ghost should be rotated.

        //  |   |   |   |   |   Temporarily
        //  |   |   |   |   |   replaced
        //  v   v   v   v   v   with:

        //_____ Joystick _____
        tempInput = sp.ReadByte();                                      // Get input from Serial Port.
        tempInput -= 32;                                                // I added 32 before sending it here, so I have to subtract 32 now to get the real value.
        if ((tempInput >= MinLeftDistance && tempInput <= MaxLeftDistance) || (tempInput >= MinRightDistance && tempInput <= MaxRightDistance))  // If the input value is between the boundaries...
        {
            angleToRotate = -tempInput/2;
            Ghost.transform.Rotate(0, angleToRotate, 0);                     // Rotate the Ghost in the Y-Axis in the direction and degrees provided by angleToRotate.
        }
    }


    //__________________________________________________CHECKMOTION():__________________________________________________
    void CheckMotion()
    {
        tempInput = sp.ReadByte();                                      // Get input from Serial Port.
        tempInput -= 32;                                                // I added 32 before sending it here, so I have to subtract 32 now to get the real value.

        if ((tempInput >= MinLeftDistance && tempInput <= MaxLeftDistance) || (tempInput >= MinRightDistance && tempInput <= MaxRightDistance))  // If the input value is between the boundaries...
        {
            recentValues[arrIn] = tempInput;                            //...Add the input to the array.

            if (currentArrSize < SIZE)
            {
                currentArrSize++;                                       // Increment currentArrSize every time a new value gets added, until currentArrSize == 10.
            }

            GetDirection();                                             // Call GetDirection() to rotate the Ghost.

            arrIn++;                                                    // Increment array's counter.
            if (arrIn > (SIZE - 1))                                     // If the end of the array is reached...
            {
                arrIn = 0;                                              //..."Point" to the first cell again.
            }
        }
        else                                                            // Else, if the input is out of bounds...
        {
            DeleteRecentValues();                                       //...Call DeleteRecentValues() to delete all array's values.
        }
    }


    //__________________________________________________GETDIRECTION():__________________________________________________
    void GetDirection()                                                 // Call GetDirection() to rotate the Ghost.
    {
        recentAverage = 0;
        for (int i = 0; i < currentArrSize; i++)                        // Calculating average value for all array's "not-empty" cells:
        {
            recentAverage += recentValues[i];
        }
        recentAverage = recentAverage / currentArrSize;

        if (recentValues[arrIn] > recentAverage)                        // If the latest Input value is greater than the average value...
        {
            Debug.Log("Gesture-to-the-Right, Counterclockwise rotation");
            rotateCW = false;                                           //the Ghost needs to be rotated Clockwise.
            rotateCCW = true;
        }
        else if (recentValues[arrIn] < recentAverage)                   // If the latest Input value is smaller than the average value... the Ghost needs to be rotated Counterclockwise.
        {
            Debug.Log("Gesture-to-the-Left, Clockwise rotation");
            rotateCCW = false;                                          // the Ghost needs to be rotated Counterclockwise.
            rotateCW = true;
        }

        // --- SMOOTH ROTATION ---
        // angleToRotate gets a value based on the difference between the latest Input value and array's average value. The value expresses the direction and total degrees of rotation angle.
        // Also, changed the variable's value range from [-64,64] to [-MaxATR,MaxATR].
        angleToRotate = (float)(MaxATR * (recentValues[arrIn] - recentAverage)) / 64;
        angleToRotate = -angleToRotate;                                 // Setting angleToRotate to its opposite value so the Ghost rotates in the right direction. aTR<0 == CW, aTR>0 == CCW.

        if (angleToRotate < 1 && angleToRotate > 0)                     // If 0<angleToRotate<1...
        {
            angleToRotate = 1;                                          //...I set angleToRotate back to 1. That helps by rotating the Ghost even for very small hand gestures and improves accuracy.
        }
        else if (angleToRotate > -1 && angleToRotate < 0)               // If -1<angleToRotate<0...
        {
            angleToRotate = -1;                                         //...I set angleToRotate back to -1. That helps by rotating the Ghost even for very small hand gestures and improves accuracy.
        }
        else if (angleToRotate == 0 && rotateCW)                        // When angleToRotate reaches 0, I need the Ghost to continue rotating (really slow) in the same direction for as long as my hand is detected inside the boundaries.
        {
            angleToRotate = 1;                                          // If rotateCW == true, I set angleToRotate to 1 so it rotates 1 degree Clockwise.
        }
        else if (angleToRotate == 0 && rotateCCW)                       // When angleToRotate reaches 0, I need the Ghost to continue rotating (really slow) in the same direction for as long as my hand is detected inside the boundaries.
        {
            angleToRotate = -1;                                         // If rotateCCW == true, I set angleToRotate to 1 so it rotates 1 degree Counterclockwise.
        }

        // --- GHOST ROTATION ---
        angleToRotate *= 2;
        Ghost.transform.Rotate(0, angleToRotate, 0);                     // Rotate the Ghost in the Y-Axis in the direction and degrees provided by angleToRotate.
    }


    //__________________________________________________DELETERECENTVALUES():__________________________________________________
    void DeleteRecentValues()                                           // Function used for "deleting" all recent values from the array.
    {
        arrIn = 0;                                                      // Put the next input value to the first array's cell.
        currentArrSize = 0;                                             // Array's size == 0 means that array is currently empty.
    }
}
