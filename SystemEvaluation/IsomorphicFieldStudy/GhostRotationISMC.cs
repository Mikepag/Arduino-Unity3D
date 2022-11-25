using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GhostRotationISMC : MonoBehaviour
{
    //__________________________________________________Variable Declaration:__________________________________________________
    public GameObject Ghost;

    //Recieving data from the arduino:
    SerialPort sp = new SerialPort("COM3", 9600);

    private const int MinLeftDistance = -32;    // Used for setting boundaries for the Input value.
    private const int MaxLeftDistance = -2;     // Used for setting boundaries for the Input value.
    private const int MinRightDistance = 2;     // Used for setting boundaries for the Input value.
    private const int MaxRightDistance = 32;    // Used for setting boundaries for the Input value.

    public float angleToRotate;                 // Contains the value in which the Ghost will be rotated.
    public int tempInput;                       // Variable that gets input from Arduino.


    // Start is called before the first frame update
    void Start()
    {
        sp.Open();

        angleToRotate = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {                                                       // Whenever the Backspace key is clicked...
            SceneManager.LoadScene("Assets/Scenes/Menu.unity"); // Load Menu Scene.
        }

        //_____ Joystick _____
        tempInput = sp.ReadByte();                              // Get input from Serial Port.
        tempInput -= 32;                                        // I added 32 before sending it here, so I have to subtract 32 now to get the real value.
        if ((tempInput >= MinLeftDistance && tempInput <= MaxLeftDistance) || (tempInput >= MinRightDistance && tempInput <= MaxRightDistance))
        {                                                       // If the input value is between the boundaries...
            angleToRotate = tempInput / 2;                      // Calculate the correct angle to rotate.
            //Ghost.transform.Rotate(0, angleToRotate, 0);        // Rotate the Ghost in the Y-Axis in the direction and degrees provided by angleToRotate.
            Ghost.transform.Rotate(0, 0, angleToRotate);        // Rotate the Ghost in the Y-Axis in the direction and degrees provided by angleToRotate.

        }
    }
}
