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

    public int goalReached;

    private int directionSteps;
    private int timesteps;
    private int isStationary;
    private int angle;

    private int resBtnClicked;
    private int unfinCD;
    private int didCntdown;
    private int randomAngle;


    // Use this for initialization
    void Start()
    {
        sp.Open();
        angleText.color = Color.black;
        isStationary = 1;
        goalReached = 0;
    }



    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            if (isStationary == 1)
            {
                directionSteps = sp.ReadByte();
                //RotateObject (direction);
            }


            angle = (int)Compass.transform.localRotation.eulerAngles.y;
            angleText.text = "Angle: " + angle.ToString() + "°" ;
            //if (angle >= 95 && angle <= 105)
            //{
            //    angleText.color = Color.green;
            //    stopRotating = 1;
            //}

            resBtnClicked = Compass.GetComponent<restart>().resBtnClicked;  // Getting the value of resBtnClicked from the restart.cs script.
            unfinCD = Compass.GetComponent<timer>().unfinCD;  // Getting the value of unfinCD from the timer.cs script.
            didCntdown = Compass.GetComponent<timer>().didCntdown; // Getting the value of didCntdown from the timer.cs script.

            if (resBtnClicked == 1)
            {

                randomAngle = Random.Range(180, 360);   // Return a random integer number between 180 and 359.

                //transform.Rotate(Vector3.down * 100);
                transform.localRotation = Quaternion.Euler(0, randomAngle, 0);  // Set compass's rotation of Y axis to the randomAngle.
                angleText.color = Color.black;
                goalReached = 0;
            }
            
            
            if (goalReached == 0 && unfinCD == 0 && didCntdown == 1)    // If the the goal is Not reached yet AND the countdown is not taking place AND there already was a countdown...
            {
                //StopCoroutine (RotateObject(direction));
                StartCoroutine(RotateObject(directionSteps));   // Call RotateObject() to rotate the compass.
            }
        }
    }



    IEnumerator RotateObject(int directionSteps)
    {  
        if (directionSteps > 0 && directionSteps <= 10)
        {
            timesteps = 10 - directionSteps;
            directionSteps = (11 - directionSteps) * 7;
            //timesteps = 0;
            while (timesteps < 10)
            {
                isStationary = 0;
                transform.Rotate(Vector3.up * directionSteps * Time.deltaTime);
                yield return new WaitForSeconds(0.05F);    // Suspends the coroutine execution for the given amount of seconds using scaled time.
                timesteps++;
                if(timesteps >= 10)
                {
                    isStationary = 1;
                }
            }
        }
        else if (directionSteps > 10)
        {
            timesteps = 10 - (directionSteps - 10);
            directionSteps = (11 - (directionSteps - 10)) * 7;
            //timesteps = 0;
            while (timesteps < 10)
            {
                isStationary = 0;
                transform.Rotate(Vector3.down * directionSteps * Time.deltaTime);
                yield return new WaitForSeconds(0.05F);    //Suspends the coroutine execution for the given amount of seconds using scaled time.
                timesteps++;
                if (timesteps >= 10)
                {
                    isStationary = 1;
                }
            }
        }

        
        if (isStationary == 1 && angle >= 95 && angle <= 105 && resBtnClicked == 0)
        {
            angleText.color = Color.green;
            goalReached = 1;
        }
        
    }
}
