using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class cubeRotation: MonoBehaviour {
	public GameObject Cube;

	//Recieving data from the arduino:
	SerialPort sp = new SerialPort("COM3", 9600);

	public int directionSteps;
	public int timesteps;



	// Use this for initialization
	void Start () {
		sp.Open ();
	}



	// Update is called once per frame
	void Update ()
	{
		if (sp.IsOpen) {
			directionSteps = sp.ReadByte ();
			//RotateObject (direction);

			//StopCoroutine (RotateObject(direction));
			StartCoroutine (RotateObject(directionSteps));
		}
	}
		


	IEnumerator RotateObject (int directionSteps){
		if (directionSteps >0 && directionSteps <= 10) {
			directionSteps = (11 - directionSteps) * 10;

			timesteps = 0;
			while (timesteps < 10) {
				transform.Rotate (Vector3.up * directionSteps * Time.deltaTime);
				yield return new WaitForSeconds (0.0001F);	// Suspends the coroutine execution for the given amount of seconds using scaled time.
				timesteps++;
			}
		} else if (directionSteps >10) {
			directionSteps = (11 -(directionSteps - 10)) * 10;
			timesteps = 0;
			while (timesteps < 10) {
				transform.Rotate (Vector3.down * directionSteps * Time.deltaTime);
				yield return new WaitForSeconds (0.0001F);	//Suspends the coroutine execution for the given amount of seconds using scaled time.
				timesteps++;
			}
		}
	}
}
