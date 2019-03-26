using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;




public class UltraSonicCameraControl : MonoBehaviour {


    float posX = 0;
    float posY = 0;
    float posZ = 0;

    float minDistance = 2;
    float maxDistance = 32;

    bool goForward = false;
    bool goBack = false;

    float [] recentValues = new float[10];
    float recentAverage = 0;
    int recentCounter = 0;

    System.IO.Ports.SerialPort stream = new System.IO.Ports.SerialPort("COM7", 9600); 
    //System.IO.Ports.SerialPort stream = new System.IO.Ports.SerialPort("\\\\.\\COM11", 9600);

    // Use this for initialization
    void Start ()
    {
        stream.Open();

        posX = this.transform.position.x;
        posY = this.transform.position.y;
        posZ = this.transform.position.z;

    }
	
	// Update is called once per frame
	void Update ()
    {

        //Debug.Log(stream.ReadLine());
      

       /*
       string value = stream.ReadLine();
       Debug.Log(value);

       this.transform.position = new Vector3(posX + float.Parse(value), this.transform.position.y, this.transform.position.z);
       */

        checkMotion();
    }

    void checkMotion()
    {
        if (recentCounter > 9)
            recentCounter = 0;

        float tempInput = float.Parse(stream.ReadLine());

        if (tempInput >= minDistance && tempInput <= maxDistance)
        {
            recentValues[recentCounter] = tempInput;
            getDirection();

            recentCounter++;
        }
    }


    void getDirection()
    {
        //for (int i = 0; i < recentValues.Length; i++)
        recentAverage = 0;
        for (int i = 0; i <= recentCounter; i++)
        {
            recentAverage += recentValues[i];
        }

        recentAverage = recentAverage / (recentCounter+1);

        if (recentValues[recentCounter] > recentAverage)
        {
            Debug.Log("Back");
            goBack = true;
            goForward = false;
        }

        if (recentValues[recentCounter] < recentAverage)
        {
            Debug.Log("Forward");
            goForward = true;
            goBack = false;
        }

        if (goForward)
            this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);

        if (goBack)
            this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);

        if (goForward || goBack)
        {
            Debug.Log("Recent input: " + recentValues[recentCounter] + " & Recent average: " + recentAverage);
        }
    }

}
