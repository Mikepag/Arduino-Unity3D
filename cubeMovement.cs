using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class cubeMovement : MonoBehaviour {
	public GameObject Cube;

	//Recieving data from the arduino:
	SerialPort sp = new SerialPort("COM3", 9600);

	public int distance;
	public int previousPos = 50;
	public int dist2Move;
	public int direction;


	// Use this for initialization
	void Start () {
		sp.Open ();
	}
		


	// Update is called once per frame
	void Update ()  {
		if (sp.IsOpen) {
			distance = sp.ReadByte();
			dist2Move = previousPos - distance;
			if (dist2Move < 0) {
				dist2Move = dist2Move * (-1);
				direction = 0;
			} else {
				direction = 1;
			}
			MoveObject (dist2Move, direction);
			previousPos = distance;





			//MoveObject(distance);

			//SetTransformZ(distance);	//THIS (almost) WORKS!

			//transform.Translate (0, 0, distance);	
	
		}
	}


	/*	//THIS (almost) WORKS!
	void SetTransformZ(int distance){
		transform.position = new Vector3 (transform.position.x , transform.position.y, distance);
	}
	*/



	void MoveObject(int dist2Move, int direction){
		if (direction == 0) {
			transform.Translate (Vector3.forward * dist2Move, Space.World);
		} 
		else {
			transform.Translate (Vector3.back * dist2Move, Space.World);
		}


		//transform.position.y = distance;
		//Cube.transform.position += Vector3.forward * distance;
	
	}


}
