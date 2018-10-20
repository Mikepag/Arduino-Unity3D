# Arduino-Unity3D
#### Arduino - Unity3D integration project. 
---

## **distanceCalcUSS.ino**
**--> C++ code uploaded to the Arduino.**
* Uses an Ultrasonic Sensor (hc-sr04) to calculate a distance.
* Sends that distance to Unity.
* Switches on and off some LEDs depending on how great the distance is.
---

## **cubeMovement.cs**
**--> C Sharp script attached to a cube in Unity.**
* Receives distance from Arduino.
* Moves cube:
    * closer to the camera as the distance decreases.
    * further away from the camera as the distance increases.
* *Doesn't work as it should yet.*
