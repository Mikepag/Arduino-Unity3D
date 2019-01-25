using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class compassRotation : MonoBehaviour
{
    public GameObject Compass;
    public Text angleText;      // Declaring a public Text called angleText

    //Recieving data from the arduino:
    SerialPort sp = new SerialPort("COM3", 9600);

    private int directionSteps;
    private int timesteps;
    public int stopRotating;
    private int angle;



    // Use this for initialization
    void Start()
    {
        sp.Open();
        angleText.color = Color.black;
        stopRotating = 0;
    }



    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            directionSteps = sp.ReadByte();
            //RotateObject (direction);

            angle = (int)Compass.transform.localRotation.eulerAngles.y;
            angleText.text = "Angle: " + angle.ToString() + "°" ;
            //if (angle >= 95 && angle <= 105)
            //{
            //    angleText.color = Color.green;
            //    stopRotating = 1;
            //}

            if (stopRotating != 1)
            {
                //StopCoroutine (RotateObject(direction));
                StartCoroutine(RotateObject(directionSteps));
            }
        }
    }



    IEnumerator RotateObject(int directionSteps)
    {  
        if (directionSteps > 0 && directionSteps <= 10)
        {
            timesteps = 10 - directionSteps;
            directionSteps = (11 - directionSteps) * 20;
            //timesteps = 0;
            while (timesteps < 10)
            {
                transform.Rotate(Vector3.up * directionSteps * Time.deltaTime);
                yield return new WaitForSeconds(0.00000000001F);    // Suspends the coroutine execution for the given amount of seconds using scaled time.
                timesteps++;
            }
        }
        else if (directionSteps > 10)
        {
            timesteps = 10 - (directionSteps - 10);
            directionSteps = (11 - (directionSteps - 10)) * 20;
            //timesteps = 0;
            while (timesteps < 10)
            {
                transform.Rotate(Vector3.down * directionSteps * Time.deltaTime);
                yield return new WaitForSeconds(0.00000000001F);    //Suspends the coroutine execution for the given amount of seconds using scaled time.
                timesteps++;
            }
        }
        if (angle >= 95 && angle <= 105)
        {
            angleText.color = Color.green;
            stopRotating = 1;
        }
    }
}
