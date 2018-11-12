using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class cubeRotation: MonoBehaviour {
	public GameObject Cube;

	//Recieving data from the arduino:
	SerialPort sp = new SerialPort("COM3", 9600);

	public int direction;



	// Use this for initialization
	void Start () {
		sp.Open ();
	}



	// Update is called once per frame
	void Update ()
	{
		if (sp.IsOpen) {
			direction = sp.ReadByte ();
			RotateObject (direction);
		}
	}



	void RotateObject (int direction){

		if (direction == 0) {
			transform.Rotate (Vector3.up * 15, Space.World);
		} 
		else if (direction == 1) {
			transform.Rotate (Vector3.down * 15, Space.World);
		}
	}

}
