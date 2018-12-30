# Arduino-Unity3D
#### Arduino - Unity3D integration project. 
---

# Project 1
##### Distance Calculation using an Ultrasonic Sensor - Unity's Cube Movement.
###### Related Files: [distanceCalcUSS.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/distanceCalcUSS.ino), [cubeMovement.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/cubeMovement.cs), [distanceCalcUSS_circuitSchema.png](https://github.com/Mikepag/Arduino-Unity3D/commits/master/distanceCalcUSS_circuitSchema.png).

## distanceCalcUSS.ino
**--> C++ code uploaded to the Arduino.**
* Uses an Ultrasonic Sensor (hc-sr04) to calculate a distance.
* Sends that distance to Unity.
* Switches on and off some LEDs depending on how great the distance is.

## cubeMovement.cs
**--> C Sharp script attached to a cube in Unity.**
* Receives distance from Arduino.
* Moves cube:
    * closer to the camera as the distance decreases.
    * further away from the camera as the distance increases.

## distanceCalcUSS_circuitSchema.png
**--> Arduino's Circuit Schema.**
___

# Project 2 (DUSHGRProject)
##### *D*ual *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project*
###### Related Files: [dUSHGR_Project.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/dUSHGR_Project.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/cubeRotation.cs), [dUSHGRPCircuitSchema.JPG](https://github.com/Mikepag/Arduino-Unity3D/blob/master/dUSHGRPCircuitSchema.JPG).

## dUSHGR_Project.ino
**--> C++ code uploaded to the Arduino.**
* Uses two (Left and Right) Ultrasonic Sensors (hc-sr04) to calculate two distances and detect whether something (a human hand) is in front of left or right sensor.
* When something is detected in front of one sensor, it saves that information for a specific amount of time.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left hand gestures and determine their speed.

## cubeRotation.cs
**--> C Sharp script attached to a cube in Unity.**
* Receives rotation value from Arduino.
* Rotates cube:
   * to the Left if the received value is between 1-10.
   * to the Right if the received value is between 11-20.
   * does not rotate the cube if the received value is equal to 0.
* Each number between 1-10 and 11-20 describes the speed of the recognised gesture and also specifies cube's rotational speed as shown below:
   * 1 == Really Fast right-to-left rotation.
   * 10 == Really Slow left-to-right rotation.
   * 11 == Really Fast right-to-left rotation.
   * 20 == Really Slow left-to-right rotation.

## dUSHGRPCircuitSchema.JPG
**--> Arduino's Circuit Schema.**
___

# Project 3 (QUSHGRProject)
##### *Q*uad *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project*
###### Related Files: [qUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/qUSHGRProject.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/cubeRotation.cs), [qUSHGRPCircuitSchema.JPG](https://github.com/Mikepag/Arduino-Unity3D/blob/master/qUSHGRPCircuitSchema.JPG).

## qUSHGRProject.ino
**--> C++ code uploaded to the Arduino.**
* Uses four (Top Left, Top Right, Bottom Left, Bottom Right) Ultrasonic Sensors (hc-sr04) to calculate four distances and detect whether something (a human hand) is in front of each sensor.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left hand gestures and determine their speed.
* With the added top layer of sensors it is now more likely for a hand to be detected in front of the sensors and as a result, there is a bigger chance that a hand gesture will be recognised.

## cubeRotation.cs
**--> C Sharp script attached to a cube in Unity.**
* Receives rotation value from Arduino as a number between 1 and 20.
* Rotates cube:
   * to the Left if the received value is between 1 and 10.
   * to the Right if the received value is between 11 and 20.
   * does not rotate the cube if the received value is equal to 0.
* Each number between 1-10 and 11-20 describes the speed of the recognised gesture and also specifies cube's rotational speed as shown below:
   * 1 == Really Fast right-to-left rotation.
   * 10 == Really Slow left-to-right rotation.
   * 11 == Really Fast right-to-left rotation.
   * 20 == Really Slow left-to-right rotation.

## dUSHGRPCircuitSchema.JPG
**--> Arduino's Circuit Schema.**
