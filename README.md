# Arduino-Unity3D
#### Arduino - Unity3D integration project. 
---

# Project 1
##### Distance Calculation using an Ultrasonic Sensor - Unity's Cube Movement.
###### Related Files: [distanceCalcUSS.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/distanceCalcUSS.ino), [cubeMovement.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/cubeMovement.cs), [distanceCalcUSS_circuitSchema.png](https://github.com/Mikepag/Arduino-Unity3D/commits/master/distanceCalcUSS_circuitSchema.png)

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
###### Related Files: [dUSHGR_Project.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/dUSHGR_Project.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/cubeRotation.cs), [dUSHGRPCircuitSchema.JPG](https://github.com/Mikepag/Arduino-Unity3D/blob/master/dUSHGRPCircuitSchema.JPG)

## dUSHGR_Project.ino
**--> C++ code uploaded to the Arduino.**
* Uses two (Left and Right) Ultrasonic Sensors (hc-sr04) to calculate two distances and detect whether something (a human hand) is in front of left or right sensor.
* When something is detected in front of one sensor, it saves that information for a specific amount of time.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left hand gestures and determine their speed.

## cubeRotation.cs
**--> C Sharp script attached to a cube in Unity.**
* Receives rotation value from Arduino as a number between 0 and 20.
* Rotates cube:
   * to the Left if the received value is between 1 and 10.
   * to the Right if the received value is between 11 and 20.
   * does not rotate the cube if the received value is equal to 0.
* Each number between 1-10 and 11-20 describes the speed of the recognised gesture and also specifies cube's rotational speed as shown below:
   * 1 == Really Fast right-to-left rotation.
   * 10 == Really Slow right-to-left rotation.
   * 11 == Really Fast left-to-right rotation.
   * 20 == Really Slow left-to-right rotation.

## dUSHGRPCircuitSchema.JPG
**--> Arduino's Circuit Schema.**
___

# Project 3 (QUSHGRProject)
##### *Q*uad *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project*
###### Related Files: [qUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/qUSHGRProject.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/cubeRotation.cs), [qUSHGRPCircuitSchema.JPG](https://github.com/Mikepag/Arduino-Unity3D/blob/master/qUSHGRPCircuitSchema.JPG)

## qUSHGRProject.ino
**--> C++ code uploaded to the Arduino.**
* Uses four (Top Left, Top Right, Bottom Left, Bottom Right) Ultrasonic Sensors (hc-sr04) to calculate four distances and detect whether something (a human hand) is in front of each sensor.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left hand gestures and determine their speed.
* With the added top layer of sensors it is now more likely for a hand to be detected in front of the sensors and as a result, there is a bigger chance that a hand gesture will be recognised.

## cubeRotation.cs
**--> C Sharp script attached to a cube in Unity.**
* Receives rotation value from Arduino as a number between 0 and 20.
* Rotates cube:
   * to the Left if the received value is between 1 and 10.
   * to the Right if the received value is between 11 and 20.
   * does not rotate the cube if the received value is equal to 0.
* Each number between 1-10 and 11-20 describes the speed of the recognised gesture and also specifies cube's rotational speed as shown below:
   * 1 == Really Fast right-to-left rotation.
   * 10 == Really Slow right-to-left rotation.
   * 11 == Really Fast left-to-right rotation.
   * 20 == Really Slow left-to-right rotation.

## qUSHGRPCircuitSchema.JPG
**--> Arduino's Circuit Schema.**
___

# Project 3.1 (QUSHGRProject_BF)
##### *Q*uad *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project* ("*B*ook *F*lipping" Gestures)
###### Related Files: [qUSHGRP_BF.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/qUSHGRP_BF.ino), [qUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/qUSHGRProject.ino) [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/cubeRotation.cs)

## qUSHGRP_BF.ino
**--> C++ code uploaded to the Arduino.**
* Uses four (Top Left, Top Right, Bottom Left, Bottom Right) Ultrasonic Sensors (hc-sr04) to calculate four distances and detect whether something (a human hand) is in front of each sensor.
* Prints the distances to the IDE's Serial Monitor so I can detect possible "patterns" if they exist.
* It sends nothing to Unity. It was used just for testing which gestures the sensors can detect, by observing the distances displayed on the Serial Monitor.

## qUSHGRProject.ino && cubeRotation.cs
**--> C++ code uploaded to the Arduino.**
**--> C Sharp script attached to a cube in Unity.**
* They were used together without any changes to their code (exactly the same as it is in Project 3), to test how the new gestures are recognised.
___
# Project 4 (QTUSHGRProject)
##### *Q*uad *T*ilted *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project*
###### Related Files: [qTUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/qTUSHGRProject.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/cubeRotation.cs)

## qTUSHGRProject.ino
**--> C++ code uploaded to the Arduino.**
* Uses four (Top Left, Top Right, Bottom Left, Bottom Right) Ultrasonic Sensors (hc-sr04) to calculate four distances and detect whether something (a human hand) is in front of each sensor.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left "book flipping" hand gestures and determine their speed.
* The difference from previous projects is that now the sensors are not parallel but at an angle of 90 degrees to each other.
* In addition, the two breadboars are placed one next to each other instead off one above the other.
* The "book flipping" gestures are more user-friendly than the previous recognisable gestures.

## cubeRotation.cs
**--> C Sharp script attached to a cube in Unity.**
* It was used without any changes to its code (exactly the same as it is in Project 3), to rotate the cube in Unity.
* A minnor error was found and fixed, so now the cube rotates even in successive or too slow gestures.
___

# Project 4.1 (QTUSHGRProject2)
##### *Q*uad *T*ilted *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project 2*
###### Related Files: [qTUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/qTUSHGRProject.ino), [compassRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/compassRotation.cs), [timer.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/timer.cs), [restart.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/restart.cs)

## qTUSHGRProject.ino
**--> C++ code uploaded to the Arduino.**
* It was used without any changes to its code (exactly the same as it is in Project 4)

## compassRotation.cs
**--> C Sharp script attached to a cylinder named "Compass" in Unity.**
* **It is used to rotate the game object "Compass"**
* Similar to cubeRotate.cs but in this there is extra functionality.
* It gets the value of variables resBtnClicked, unfinCD and didCntdown from the restart.cs and timer.cs scripts.
* If the restart button gets clicked:
  * Compass gets rotated to a random angle.
* For as long as the the goal is not reached yet, the countdown is not currently taking place and a countdown already took place in the current round:
  * The RotateObject() coroutine is called to rotate the compass.
* After the compass has stopped rotating, I check whether its angle is between 175° and 185°.
  If it is:
  * The goal has been successfully reached (for this round).
  * Compass stops rotating for recognised gestures.
  
## timer.cs
**--> C Sharp script attached to a cylinder named "Compass" in Unity.**
* **It is used to print a stopwatch on the screen.**
* Time is updated once per frame.
* Format is 00:00:00 (Minutes : Seconds : Miliseconds).
* It gets the value of variables goalReached, resBtnClicked and roundNum from the compassRotation.cs and restart.cs scripts.
* If the goal has been reached and the restart button has not been clicked yet:
  * Time stops counting.
  * Timer text becomes red.
  * Time is printed to Times.txt.
* If the restart button gets clicked:
  * Timer text becomes black again.
  * The countdown starts.
* When the countdown ends, the timer (stopwatch) starts.

## restart.cs
**--> C Sharp script attached to a cylinder named "Compass" in Unity.**
* **It is used to control the restart button and print the current round's number on the screen.**
* It gets the value of variable goalReached from the compassRotation.cs script.
* If the goal has been reached:
  * The restart button becomes visible.
  * Round text becomes green.
* If the restart button gets clicked:
  * The restart button becomes non-visible.
  * Τhe next round starts.
  * If it gets clicked in the final round, the application shuts down.
