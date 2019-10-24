using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class cubeMovement : MonoBehaviour {
	public GameObject Cube;

	//Recieving data from the arduino:
	SerialPort sp = new SerialPort("COM3", 9600);

	public int distInt;
	public float distance;
	public float previousPos = 0;
	public float dist2Move;
	public int direction;


	// Use this for initialization
	void Start () {
		sp.Open ();
	}
		


	// Update is called once per frame
	void Update ()  {
		if (sp.IsOpen) {
			distInt = sp.ReadByte();
			distance = (float) distInt / 100; 
			dist2Move = previousPos - distance;
			if (dist2Move < 0) {
				dist2Move = dist2Move * (-1);
				direction = 0;
			} else {
				direction = 1;
			}
			MoveObject (dist2Move, direction);
			previousPos = distance;
		}
	}



	void MoveObject(float dist2Move, int direction){
		
		if (direction == 0) {
			transform.Translate (Vector3.forward * dist2Move, Space.World);
		} 
		else {
			transform.Translate (Vector3.back * dist2Move, Space.World);
		}
	}

}
