using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GhostRotationNTRL : MonoBehaviour
{
    //__________________________________________________Variable Declaration:__________________________________________________
    public GameObject Ghost;

    //Recieving data from the arduino:
    //SerialPort sp = new SerialPort("COM3", 9600);
    SerialPort sp = new SerialPort("/dev/ttyACM0", 9600);

    private const int MinLeftDistance = -32;            // Used for setting boundaries for the Input value.
    private const int MaxLeftDistance = -2;             // Used for setting boundaries for the Input value.
    private const int MinRightDistance = 2;             // Used for setting boundaries for the Input value.
    private const int MaxRightDistance = 32;            // Used for setting boundaries for the Input value.
    private const int MaxAOR = 25;                      // MaxAngleOfRotation. Maximum degrees angle the Ghost can be rotated.
    private const int SIZE = 15;                        // previousPositions[] size.
    private const int maxDampedRotationDuration = 20;   // Maximum number of timesteps the Ghost's damped rotation can last.

    private int timestepsSinceOutOfBounds = 0;          // Counter of the elapsed timesteps since the hand position went out of the predefined boundaries.

    public float angleOfRotation;                       // Contains the value in which the Ghost will be rotated.
    public int currentPosition;                         // Variable that gets input from Arduino.

    private int[] previousPositions = new int[SIZE];    // (Circular) Array containing the [SIZE] most recent input values.
    public int averagePreviousPosition;                 // Average value of all array's values.
    private int arrIn;                                  // Is equal to the number of the array's cell into which we can insert data. When the array is full, the oldest value gets overwritten.
    private int currentArrSize;                         // Is equal to the array's current size (number of not empty cells). Used for calculating average value.

    public float initialAngleOfDampedRotation;          // Gets the last angleOfRotation value before the hand position went out of the predefined boundaries. Used as the initial speed for the damped rotation.
    private int dampedRotationDuration;                 // Duration of the damped rotation in timesteps.


    //__________________________________________________START():__________________________________________________
    void Start()    // Use this for initialization
    {
        sp.Open();

        angleOfRotation = 0;
        averagePreviousPosition = 0;
        arrIn = 0;
        currentArrSize = 0;
        dampedRotationDuration = 0; 
    }


    //__________________________________________________UPDATE():__________________________________________________
    void Update()   // Update is called once per frame
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {                                                       // Whenever the Backspace key is clicked...
            SceneManager.LoadScene("Assets/Scenes/Menu.unity"); // Load Menu Scene.
        }

        currentPosition = sp.ReadByte();                        // Get input from Serial Port.
        currentPosition -= 32;                                  // I added 32 before sending it here, so I have to subtract 32 now to get the real value.

        if ((currentPosition >= MinLeftDistance && currentPosition <= MaxLeftDistance) || (currentPosition >= MinRightDistance && currentPosition <= MaxRightDistance))
        {                                                       // If the input value is between the boundaries...
            timestepsSinceOutOfBounds = 0;

            previousPositions[arrIn] = currentPosition;         //...Add the input to the array.

            if (currentArrSize < SIZE)
            {
                currentArrSize++;                               // Increment currentArrSize every time a new value gets added, until currentArrSize == SIZE.
            }
            
            angleOfRotation = GetAngleOfRotation();             // Call GetAngleOfRotation() to get the desired angle (and direction) of the rotation.
            GhostRotation(angleOfRotation);                     // Call GhostRotation() to rotate the Ghost.
            
            arrIn++;                                            // Increment array's counter.
            if (arrIn > (SIZE - 1))
            {                                                   // If the end of the array is reached...
                arrIn = 0;                                      //..."Point" to the first cell again.
            }
        }
        else                                                                                    // Else, if the input is out of bounds...
        {
            if (timestepsSinceOutOfBounds == 0)                                                 // If it is the first time the currentPosition is out of bounds...
            {
                initialAngleOfDampedRotation = angleOfRotation;                                 // Save the last calculated angleOfRotation to be used as the initialAngleofDampedRotation.
                dampedRotationDuration = (int)(Math.Abs(initialAngleOfDampedRotation));         // Geting the integer absolute value of initialAngleOfRotation. 
                if (dampedRotationDuration > maxDampedRotationDuration)
                {
                    dampedRotationDuration = maxDampedRotationDuration;                         // If the calculated duration is longer than the predefined maximum duration, set it to the maximum value.
                }
            }

            if (timestepsSinceOutOfBounds < dampedRotationDuration)                             // While the timestepsSinceOutOfBounds haven't reached the maximum rotationDuration value...
            {   
                DampedGhostRotation(initialAngleOfDampedRotation, timestepsSinceOutOfBounds);   //...Call DampedGhostRotation() to rotate the Ghost.
            }
            
            if (timestepsSinceOutOfBounds >= 2)                                                 // If 2 timesteps have passed since the time in which the input value went out of bounds...
            {
                DeletePreviousPositions();                                                      //...Call DeletePreviousPositions() to delete all array's values.
            }

            timestepsSinceOutOfBounds++;                                                        // Increment the timesteps since the time in which the input value went out of bounds.
        }
    }

    


    //_________________________________________________________________________________________________________________________
    //__________________________________________________GETANGLEOFROTATION():__________________________________________________
    float GetAngleOfRotation()
    {
        averagePreviousPosition = 0;
        if (currentArrSize > 0) // To avoid the division with zero when the array is empty. (Not really needed since the function is only called if there is at least one value in the array.)
        {
            for (int i = 0; i < currentArrSize; i++)    // Calculating average value for all array's "not-empty" cells:
            {
                averagePreviousPosition += previousPositions[i];
            }
            averagePreviousPosition = averagePreviousPosition / currentArrSize;
        }

        // angleOfRotation gets a value based on the difference between the latest Input value and array's average value. The value expresses the direction and total degrees of rotation angle.
        // Also, changed the variable's value range from [-64,64] to [-MaxAOR,MaxAOR].
        angleOfRotation = (float)(MaxAOR * (currentPosition - averagePreviousPosition)) / 64;

        return angleOfRotation;
    }


    //__________________________________________________GHOSTROTATION():__________________________________________________
    void GhostRotation(float angleOfRotation)   // Call GhostRotation() to rotate the Ghost while the hand position is between the boundaries.
    {
        // --- GHOST ROTATION ---
        Ghost.transform.Rotate(0, 0, angleOfRotation);  // Rotates the Ghost in the Z-Axis in the direction and degrees provided by angleOfRotation.
    }


    //__________________________________________________DAMPEDGHOSTROTATION():__________________________________________________
    void DampedGhostRotation(float initialAngleOfDampedRotation, int timestepsSinceOutOfBounds) // Call DampedGhostRotation() to smoothly stop (dampen) the rotating Ghost after the hand position stopped being between the boundaries.
    {
        // Automatically calculates the angleOfRotation's decreasing rate as a percentage of the initial angle of rotation to control how long the rotation is going to last.
        // aOR = iAODR * (100 - (100 * ts/dur))/100, OR: aOR = iAODR * (1 - ts/dur), OR: aOR = iAODR * X%, where X is the appropriate percentage (e.g: aOR = iAODR * 50%, aOR = iAODR * 95% etc.).
        // Removes the appropriate percentage from the initial angle of rotation for every elapsed timestep, so the rotational speed decreases and reaches 0 (0% of the iAODR) when the maximum rotation's duration is reached:
        angleOfRotation = initialAngleOfDampedRotation * (100 - (100/dampedRotationDuration * (1 + timestepsSinceOutOfBounds))) / 100;

        // --- GHOST ROTATION ---
        Ghost.transform.Rotate(0, 0, angleOfRotation);  // Rotates the Ghost in the Z-Axis in the direction and degrees provided by angleOfRotation.
    }


    //__________________________________________________DELETEPREVIOUSPOSITIONS():__________________________________________________
    void DeletePreviousPositions()  // Function used for "deleting" all recent values from the array.
    {
        arrIn = 0;                  // Put the next input value to the first array's cell.
        currentArrSize = 0;         // Array's size == 0 means that array is currently empty.
    }
}