using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class cubeRotation: MonoBehaviour {
	public GameObject Cube;

	//Recieving data from the arduino:
	SerialPort sp = new SerialPort("COM3", 9600);

	public int direction;
	public int timesteps;



	// Use this for initialization
	void Start () {
		sp.Open ();
	}



	// Update is called once per frame
	void Update ()
	{
		if (sp.IsOpen) {
			direction = sp.ReadByte ();
			//RotateObject (direction);

			//StopCoroutine (RotateObject(direction));
			StartCoroutine (RotateObject(direction));
		}
	}



	IEnumerator RotateObject (int direction){
		if (direction == 0) {
			timesteps = 0;
			while (timesteps < 10) {
				transform.Rotate (Vector3.up * 15 * Time.deltaTime);
				yield return new WaitForSeconds (0.001F);	// Suspends the coroutine execution for the given amount of seconds using scaled time.
				timesteps++;
			}
		} else if (direction == 1) {
			timesteps = 0;
			while (timesteps < 10) {
				transform.Rotate (Vector3.down * 15 * Time.deltaTime);
				yield return new WaitForSeconds (0.001F);	//Suspends the coroutine execution for the given amount of seconds using scaled time.
				timesteps++;
			}
		}
	}

}
