using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;




public class UltraSonicCameraControl : MonoBehaviour {

    float posX = 0;
    float posY = 0;
    float posZ = 0;

    int minLeftDistance = -32;    // Used for setting boundaries for the Input value.
    int maxLeftDistance = -2;
    int minRightDistance = 2;
    int maxRightDistance = 32;

    bool goForward = false; // Flag to move (rotate) forward (to-the-left).
    bool goBack = false;    // Flag to move (rotate) backward (to-the-right).

    public int tempInput;   // Variable that gets input from Arduino.

    int[] recentValues = new int[10];   // (Circular) Array containing the ten most recent input values.
    public int recentAverage = 0;   // Average value of all array's values.
    int arrIn = 0;  // Is equal to the number of the array's cell into which we can insert data. When the array is full, the oldest value gets overwritten.
    int arrSize = 0;    // Is equal to the array's size (number of not empty cells). Used for calculating average value.

    

    SerialPort sp = new SerialPort("COM3", 9600);
    //System.IO.Ports.SerialPort stream = new System.IO.Ports.SerialPort("COM7", 9600); 
    //System.IO.Ports.SerialPort stream = new System.IO.Ports.SerialPort("\\\\.\\COM11", 9600);

    // Use this for initialization
    void Start ()
    {
        sp.Open();
        //stream.Open();

        posX = this.transform.position.x;
        posY = this.transform.position.y;
        posZ = this.transform.position.z;

    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckMotion();
    }

    void CheckMotion()
    {

            

        //float tempInput = float.Parse(stream.ReadLine());

        //if (sp.IsOpen)
        //{
        tempInput = sp.ReadByte();  // Get input from Serial Port.
        tempInput -= 32; // I added 32 before sending it so, I have to subtract 32 now to get the real value.
        //}

        //if (tempInput >= minDistance && tempInput <= maxDistance)
        //if((tempInput >= minLeftDistance && tempInput <= maxLeftDistance) || (tempInput >= minRightDistance && tempInput <= maxRightDistance))  // If the input value is between the boundaries...
        //if ((tempInput >= minLeftDistance && tempInput <= maxLeftDistance) || (tempInput >= minRightDistance && tempInput <= maxRightDistance))  // If the input value is between the boundaries...
        if ((tempInput >= -32 && tempInput <= -2) || (tempInput >= 2 && tempInput <= 32))  // If the input value is between the boundaries...
            {
                recentValues[arrIn] = tempInput;    //...Add the input to the array.
            
            if (arrSize < 10)
            {
                arrSize++;
            }

            GetDirection();

            arrIn++;    // Increment array's counter.
            if (arrIn > 9)  // If the end of the array is reached...
            {
                arrIn = 0;  //...Go to the first cell again.
            }
        }
        else   // Else, if the input is out of bounds...
        {
            DeleteRecentValues();   //... Call DeleteRecentValues() to delete all array's values.
        }
    }


    void GetDirection()
    {
        //for (int i = 0; i < recentValues.Length; i++)
        recentAverage = 0;
        //for (int i = 0; i <= arrIn; i++)
        for(int i = 0; i<arrSize; i++)
        {
            recentAverage += recentValues[i];
        }

        recentAverage = recentAverage / arrSize;

        if (recentValues[arrIn] > recentAverage)
        {
            Debug.Log("Back");
            goBack = true;
            goForward = false;
        }

        if (recentValues[arrIn] < recentAverage)
        {
            Debug.Log("Forward");
            goForward = true;
            goBack = false;
        }

        if (goForward)
            this.transform.position = new Vector3(this.transform.position.x+1, this.transform.position.y, this.transform.position.z);

        if (goBack)
            this.transform.position = new Vector3(this.transform.position.x-1, this.transform.position.y, this.transform.position.z);

        if (goForward || goBack)
        {
            Debug.Log("Recent input: " + recentValues[arrIn] + " & Recent average: " + recentAverage);
        }
    }

    void DeleteRecentValues()
    {
        //for(int i=0; i<9; i++)
        //{
        //    recentValues[i] = 0;
        //}
        arrIn = 0;
        arrSize = 0;
        recentAverage = 0;
    }

}
