# Arduino-Unity3D
#### Arduino - Unity3D integration projects. 
---

# Index
#### [Project1](https://github.com/Mikepag/Arduino-Unity3D#project-1): Distance Calculation using an Ultrasonic Sensor - Unity's Cube Movement.
#### [Project2](https://github.com/Mikepag/Arduino-Unity3D#project-2-dushgrproject): Dual Ultrasonic Sensor - Hand Gesture Recognition Project
#### [Project3](https://github.com/Mikepag/Arduino-Unity3D#project-3-qushgrproject): Quad Ultrasonic Sensor - Hand Gesture Recognition Project
#### [Project3.1](https://github.com/Mikepag/Arduino-Unity3D#project-31-qushgrproject_bf): Quad Ultrasonic Sensor - Hand Gesture Recognition Project ("Book Flipping" Gestures)
#### [Project4](https://github.com/Mikepag/Arduino-Unity3D#project-4-qtushgrproject): Quad Tilted Ultrasonic Sensor - Hand Gesture Recognition Project
#### [Project4.1](https://github.com/Mikepag/Arduino-Unity3D#project-41-qtushgrproject2): Quad Tilted Ultrasonic Sensor - Hand Gesture Recognition (+ Extra Functionality)
#### [Project4.2](https://github.com/Mikepag/Arduino-Unity3D#project42-p1-disk-rotation-using-timesteps): P1: Disk Rotation Using Timesteps (Sub-Project of Project7)
#### [Project5](https://github.com/Mikepag/Arduino-Unity3D#project-5-camera-control-project): Camera Control (Experimental Project)
#### [Project6](https://github.com/Mikepag/Arduino-Unity3D#project-6-disk-rotation): Disk Rotation (Combination of Project4.1 & Project5)
#### [Project6.1](https://github.com/Mikepag/Arduino-Unity3D#project61-p2-disk-rotation-using-average-distance): P2: Disk Rotation Using Average Distance (Sub-Project of Project7)
#### [Project7](https://github.com/Mikepag/Arduino-Unity3D#project7-disk-rotation-integration): Disk Rotation Integration (Comparison of Project4.2 & Project6.1 (& [Pepper's_Ghost](VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVINSERTURLHEREVVVVVVVVVVVVVVVVVVVVVVVV))

# Project 1
##### Distance Calculation using an Ultrasonic Sensor - Unity's Cube Movement.
###### Related Files: [distanceCalcUSS.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project1/distanceCalcUSS.ino), [cubeMovement.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project1/cubeMovement.cs), [distanceCalcUSS_circuitSchema.png](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project1/distanceCalcUSS_circuitSchema.png)

## distanceCalcUSS.ino
**&#x27BD; C++ code uploaded to the Arduino.**
* Uses an Ultrasonic Sensor (hc-sr04) to calculate a distance.
* Sends that distance to Unity.
* Switches on and off some LEDs depending on how great the distance is.

## cubeMovement.cs
**&#x27BD; C Sharp script attached to a cube in Unity.**
* Receives distance from Arduino.
* Moves cube:
    * closer to the camera as the distance decreases.
    * further away from the camera as the distance increases.

## distanceCalcUSS_circuitSchema.png
**&#x27BD; Arduino's Circuit Schema.**
___

# Project 2 (DUSHGRProject)
##### *D*ual *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project*
###### Related Files: [dUSHGR_Project.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project2/dUSHGR_Project.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project2/cubeRotation.cs), [dUSHGRPCircuitSchema.JPG](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project2/dUSHGRPCircuitSchema.JPG)

## dUSHGR_Project.ino
**&#x27BD; C++ code uploaded to the Arduino.**
* Uses two (Left and Right) Ultrasonic Sensors (hc-sr04) to calculate two distances and detect whether something (a human hand) is in front of left or right sensor.
* When something is detected in front of one sensor, it saves that information for a specific amount of time.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left hand gestures and determine their speed.

## cubeRotation.cs
**&#x27BD; C Sharp script attached to a cube in Unity.**
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
**&#x27BD; Arduino's Circuit Schema.**
___

# Project 3 (QUSHGRProject)
##### *Q*uad *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project*
###### Related Files: [qUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project3/qUSHGRProject.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project2/cubeRotation.cs), [qUSHGRPCircuitSchema.JPG](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project3/qUSHGRPCircuitSchema.JPG)

## qUSHGRProject.ino
**&#x27BD; C++ code uploaded to the Arduino.**
* Uses four (Top Left, Top Right, Bottom Left, Bottom Right) Ultrasonic Sensors (hc-sr04) to calculate four distances and detect whether something (a human hand) is in front of each sensor.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left hand gestures and determine their speed.
* With the added top layer of sensors it is now more likely for a hand to be detected in front of the sensors and as a result, there is a bigger chance that a hand gesture will be recognised.

## cubeRotation.cs
**&#x27BD; C Sharp script attached to a cube in Unity.**
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
**&#x27BD; Arduino's Circuit Schema.**
___

# Project 3.1 (QUSHGRProject_BF)
##### *Q*uad *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project* ("*B*ook *F*lipping" Gestures)
###### Related Files: [qUSHGRP_BF.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project3.1/qUSHGRP_BF.ino), [qUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project3/qUSHGRProject.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project2/cubeRotation.cs), [qUSHGRPCircuitSchema.JPG](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project3/qUSHGRPCircuitSchema.JPG)

## qUSHGRP_BF.ino
**&#x27BD; C++ code uploaded to the Arduino.**
* Uses four (Top Left, Top Right, Bottom Left, Bottom Right) Ultrasonic Sensors (hc-sr04) to calculate four distances and detect whether something (a human hand) is in front of each sensor.
* Prints the distances to the IDE's Serial Monitor so I can detect possible "patterns" if they exist.
* It sends nothing to Unity. It was used just for testing which gestures the sensors can detect, by observing the distances displayed on the Serial Monitor.

## qUSHGRProject.ino && cubeRotation.cs
**&#x27BD; C++ code uploaded to the Arduino.**
**&#x27BD; C Sharp script attached to a cube in Unity.**
* They were used together without any changes to their code (exactly the same as it is in Projects 2 & 3), to test how the new gestures are recognised.

## qUSHGRPCircuitSchema.JPG
**&#x27BD; Arduino's Circuit Schema.**
___

# Project 4 (QTUSHGRProject)
##### *Q*uad *T*ilted *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project*
###### Related Files: [qTUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/qTUSHGRProject.ino), [cubeRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project2/cubeRotation.cs), [quadTiltedUS.png](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/quadTiltedUS.png)

## qTUSHGRProject.ino
**&#x27BD; C++ code uploaded to the Arduino.**
* Uses four (Left-Left, Left-Right, Right-Left, Right-Right) Ultrasonic Sensors (hc-sr04) to calculate four distances and detect whether something (a human hand) is in front of each sensor.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left "book flipping" hand gestures and determine their speed.
* The difference from previous projects is that now the sensors are not parallel but at an angle of 90 degrees to each other and the two breadboars are placed one next to each other instead off one above the other.
* The "book flipping" gestures are more user-friendly than the previous recognisable gestures.

## cubeRotation.cs
**&#x27BD; C Sharp script attached to a cube in Unity.**
* It was used without any changes to its code (exactly the same as it is in Projects 2 & 3), to rotate the cube in Unity.
* A minnor error was found and fixed, so now the cube rotates even in successive or too slow gestures.

## quadTiltedUS.png
**&#x27BD; Arduino's Circuit Schema.**
___

# Project 4.1 (QTUSHGRProject2)
##### *Q*uad *T*ilted *U*ltrasonic *S*ensor - *H*and *G*esture *R*ecognition *Project 2*
###### Related Files: [qTUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/qTUSHGRProject.ino), [compassRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4.1/compassRotation.cs), [timer.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4.1/timer.cs), [restart.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4.1/restart.cs), [quadTiltedUS.png](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/quadTiltedUS.png)

## qTUSHGRProject.ino
**&#x27BD; C++ code uploaded to the Arduino.**
* It was used without any changes to its code (exactly the same as it is in Project 4)

## compassRotation.cs
**&#x27BD; C Sharp script attached to a cylinder named "Compass" in Unity.**
* **It is used to rotate the game object "Compass"**
* Similar to Project2/cubeRotation.cs but with extra functionality.
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
**&#x27BD; C Sharp script attached to a cylinder named "Compass" in Unity.**
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
**&#x27BD; C Sharp script attached to a cylinder named "Compass" in Unity.**
* **It is used to control the restart button and print the current round's number on the screen.**
* It gets the value of variable goalReached from the compassRotation.cs script.
* If the goal has been reached:
  * The restart button becomes visible.
  * Round text becomes green.
* If the restart button gets clicked:
  * The restart button becomes non-visible.
  * Τhe next round starts.
  * If it gets clicked in the final round, the application shuts down.
  
## quadTiltedUS.png
**&#x27BD; Arduino's Circuit Schema.**
___

# Project4.2 (P1: Disk Rotation Using Timesteps)
##### This project is an evolution of Project4.1 with which only has minnor differences.
###### For more details see the full project's description under the Main Project7 here: [Project7/Project4.2](https://github.com/Mikepag/Arduino-Unity3D#project7-project42-p1-disk-rotation-using-timesteps).
___

# Project 5 (Camera Control Project)
##### This project was just an experiment and its code will be used to improve previous projects that did not work very well.
###### Related Files: [cameraControl.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project5/cameraControl.ino), [cameraControl.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project5/cameraControl.cs), [quadTiltedUS.png](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/quadTiltedUS.png)

## cameraControl.ino
**&#x27BD; C++ code uploaded to the Arduino. Performs Position Detection in 1D.**
* **Calculates an average distance value of all four Ultrasonic Sensors and sends it to Unity via the Serial Port.**
* At the begining it was only using one sensor to calculate one distance.
* It now uses all four sensors to calculate four distances.
* Two average distances are calculated, one for the left breadboard (avgDistL) and one for the right breadboard (avgDistR).
* Finally, the value of the variable totalAvgDist is calculated based on the average value of avgDistL & avgDistR. This value gets sent to Unity.

## cameraControl.cs
**&#x27BD; C Sharp script attached to the Main Camera in Unity.**
* **Receives the average value from the Serial Port, saves it in an array, calculates the average value of all array's values, compares the latest input with that average value and moves the Main camera left/right accordingly.**
* The original script (by V.K) was unable to run.
* I made changes in order to make the script able to run, added functions and also corrected some logic errors that I found regarding the calculation of the average value.
* If the recieved value (tempInput) is between the range [-32,-2]υ[2,32]:
* tempInput's value is added to an array (recentValues[]).
* The array's average value (recentAverage) is calculated.
   * If tempInput < recentAverage &#x279C; The camera moves Right-to-Left.
   * If tempInput > recentAverage &#x279C; The camera moves Left-to-Right.
* Using the DeleteRecentValues() function to empty the array when the tempInput gets out-of-bounds.

## quadTiltedUS.png
**&#x27BD; Arduino's Circuit Schema.**
___

# Project 6 (Disk Rotation)
##### This project combines the Position Detection of Project5/cameraControl.ino, the algorithm for moving/rotating a GameObject of Project5/cameraControl.cs and the extra functionality of having countdown, timer, restart button, round's number, goal achievement, log files etc. of Project4.1/compassRotation.cs, Project4.1/timer.cs and Project4.1/restart.cs.
###### Related Files: [cameraControl.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project5/cameraControl.ino), [diskRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project6/diskRotation.cs), [timer.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project6/timer.cs), [restart.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project6/restart.cs), [quadTiltedUS.png](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/quadTiltedUS.png)

## cameraControl.ino
**&#x27BD; C++ code uploaded to the Arduino. Performs Position Detection in 1D.**
* **Calculates an average distance value of all four Ultrasonic Sensors and sends it to Unity via the Serial Port.**
* It was used without any changes to its code (exactly the same as it is in Project5).
* An error was found and fixed in Setup(). Now all sensors are able to calculate distances and the arduino's lag has been eliminated.

## diskRotation.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **Receives the average value from the Serial Port, saves it in an array, calculates the average value of all array's values, compares the latest input with that average value and rotates the Disk clockwise/counterclockwise accordingly.**
* It gets the value of variables resBtnClicked, unfinCD and didCntdown from the restart.cs and timer.cs scripts.
* If the (re)start button gets clicked:
  * The Disk gets rotated to a random angle.
* For as long as the the goal is not reached yet, the countdown is not currently taking place and a countdown already took place in the current round:
   * If the recieved value (tempInput) is between the range [-32,-2]υ[2,32]:
   * tempInput's value is added to an array (recentValues[]).
   * The array's average value (recentAverage) is calculated.
      * If tempInput < recentAverage &#x279C; The Disk rotates Counterclockwise.
      * If tempInput > recentAverage &#x279C; The Disk rotates Clockwise.
   * **SMOOTH ROTATION:**
      * angleToRotate gets a value in [-maxATR, maxATR] == [-25, 25] based on the difference between the latest Input value and array's average value. The value expresses the direction and total degrees of rotation angle:
         * **angleToRotate = (float)(MaxATR * (recentValues[arrIn] - recentAverage)) / 64;**
      * If 0<angleToRotate<1, I set angleToRotate back to 1. If -1<angleToRotate<0, I set angleToRotate back to -1. That helps by rotating the disk even for very small hand gestures and improves accuracy.
      * When angleToRotate reaches 0, I programmed the disk to continue rotating (really slow) in the same direction for as long as my hand is detected inside the boundaries.
      * The DeleteRecentValues() function is used to empty the array when the tempInput gets out-of-bounds.
* After the Disk has stopped rotating, I check whether its angle is between 175° and 185°.
  If it is:
  * The goal has been successfully reached (for this round).
  * Disk stops rotating for recognised gestures.
  
## timer.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **It is used to print a stopwatch on the screen.**
* It was used without any changes to its code (exactly the same as it is in Project4.1).
* Time is updated once per frame.
* Format is 00:00:00 (Minutes : Seconds : Miliseconds).
* It gets the value of variables goalReached, resBtnClicked and roundNum from the diskRotation.cs and restart.cs scripts.
* If the goal has been reached and the restart button has not been clicked yet:
  * Time stops counting.
  * Timer text becomes red.
  * Time is printed to Times.txt.
* If the restart button gets clicked:
  * Timer text becomes black again.
  * The countdown starts.
* When the countdown ends, the timer (stopwatch) starts.

## restart.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **It is used to control the restart button and print the current round's number on the screen.**
* It was used without any changes to its code (exactly the same as it is in Project4.1).
* It gets the value of variable goalReached from the diskRotation.cs script.
* If the goal has been reached:
  * The restart button becomes visible.
  * Round text becomes green.
* If the restart button gets clicked:
  * The restart button becomes non-visible.
  * Τhe next round starts.
  * If it gets clicked in the final round, the application shuts down.
  
## quadTiltedUS.png
**&#x27BD; Arduino's Circuit Schema.**
___

# Project6.1 (P2: Disk Rotation Using Average Distance)
##### This project is an evolution of Project6 with which only has minnor differences.
###### For more details see the full project's description under the Main Project7 here: [Project7/Project6.1](https://github.com/Mikepag/Arduino-Unity3D#project7-project61-p2-disk-rotation-using-average-distance).
___
vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

# Project7 (Disk Rotation Integration)
##### This project is consisted of two individual projects (Project4.2 & Project6.1) and aims in the comparison of them to find which is the most preferable of the two, regarding human-computer interaction. The projects are almost identical as they both rotate a disk object in Unity using an Arduino Uno and four Ultrasonic Sensors to recognise the user's hand gestures. The only difference is the way the hand gestures are recognised.
##### - Another sub-project (Pepper's_Ghost) was added which rotates a Pepper's Ghost Hologram. Its purpose is to help new users to get used to hand gesture recognition in an interesting way.
###### For more details see: [Project4.2 (P1)](https://github.com/Mikepag/Arduino-Unity3D#project7-project42-p1-disk-rotation-using-timesteps), [Project6.1 (P2)](https://github.com/Mikepag/Arduino-Unity3D#project7-project61-p2-disk-rotation-using-average-distance), [Pepper's_Ghost](VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVINSERTURLHEREVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV).
___

# Project7/ Pepper's_Ghost (Pepper's Ghost Hologram Rotation)
##### In this project a Pepper's Ghost Hologram is rotated with two different methods. 
###### Related Files: [ghostRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project7/Pepper's_Ghost/ghostRotation.cs), [joystickGhostRotation.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project7/Pepper's_Ghost/joystickGhostRotation.cs).
###### Unity Asset for Creating Peppers Ghost Hologram Pyramids: [KainosSoftwareLtd/PeppersGhostPyramid](https://github.com/KainosSoftwareLtd/PeppersGhostPyramid).

## ghostRotation.cs
**&#x27BD; C# script responsible for rotating the Ghost Game Object in the Pepper's Ghost Scene.**
* **Receives the average distacne value from the Serial Port, saves it in an array, calculates the average value of all array's values, compares the latest input with that average value and rotates the Ghost clockwise/counterclockwise accordingly.**
* The code is from Project7/Project6.1/diskRotationP2.cs with minnor changes.
* If the recieved value (tempInput) is between the range [-32,-2]υ[2,32]:
* tempInput's value is added to an array (recentValues[]).
* The array's average value (recentAverage) is calculated.
   * If tempInput < recentAverage &#x279C; The Ghost rotates Counterclockwise.
   * If tempInput > recentAverage &#x279C; The Ghost rotates Clockwise.
* **SMOOTH ROTATION:**
   * angleToRotate gets a value in [-maxATR, maxATR] == [-25, 25] based on the difference between the latest Input value and array's average value. The value expresses the direction and total degrees of rotation angle:
      * **angleToRotate = (float)(MaxATR * (recentValues[arrIn] - recentAverage)) / 64;**
   * If 0<angleToRotate<1, I set angleToRotate back to 1. If -1<angleToRotate<0, I set angleToRotate back to -1. That helps by rotating the Ghost even for very small hand gestures and improves accuracy.
   * When angleToRotate reaches 0, I programmed the Ghost to continue rotating (really slow) in the same direction for as long as my hand is detected inside the boundaries.
   * The DeleteRecentValues() function is used to empty the array when the tempInput gets out-of-bounds.

## joystickGhostRotation.cs
**&#x27BD; C# script responsible for rotating the Ghost Game Object in the Pepper's Ghost Scene.**
* **Creates a virtual slider which can be controled by the position of the hand detected in front of the sensors.**
* Rotates the Ghost object in Pepper's Ghost Scene using the average distance value sent to Unity from the Arduino.
* When the distance is < 0 (The hand is detected in the LEFT side of the sensors):
  * The ghost is rotated clockwise.
  * The smaller the distance is, the faster the rotation becomes.
* When the distance is > 0 (The hand is detected in the RIGHT side of the sensors):
  * The ghost is rotated counterclockwise.
  * The greater the distance is, the faster the rotation becomes.




^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
___

# Project7/ Project4.2 (P1: Disk Rotation Using Timesteps)
##### This project is an evolution of Project4.1 with which only has minnor differences.
###### Related Files: [qTUSHGRProject.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/qTUSHGRProject.ino), [diskRotationP1.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project7/Project4.2/diskRotationP1.cs), [restartP1.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project7/Project4.2/restartP1.cs), [timerP1.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project7/Project4.2/timerP1.cs), [quadTiltedUS.png](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/quadTiltedUS.png).

## qTUSHGRProject.ino
**&#x27BD; C++ code uploaded to the Arduino.**
* **Uses timesteps to recognise hand gestures and determine their direction and speed.**
* It was used without any changes to its code (exactly the same as it is in Project 4)
* Uses four (Left-Left, Left-Right, Right-Left, Right-Right) Ultrasonic Sensors (HC-SR04) to calculate four distances and detect whether something (a human hand) is in front of each sensor.
* By comparing the time that something was detected by each sensor, it can recognise left-to-right and right-to-left "book flipping" hand gestures and determine their speed.
* The sensors are not parallel but at an angle of 90 degrees to each other and the two breadboars are placed one next to each other.

## diskRotationP1.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **It rotates a Disk object in Unity by recognising hand gestures using the timesteps required for the hand to go from the one pair of Ultrasonic Sensors to the other.**
* diskRotationP1.cs is the code from Project4.1/compassRotation.cs with some changes and adjustments.
* It gets the value of variables resBtnClicked, unfinCD and didCntdown from the restartP1.cs and timerP1.cs scripts.
* I use the previous_resBtnClicked variable to know when the restart button gets clicked by compairing it to the current resBtnClicked as shown below:
  * if ((resBtnClicked - previous_resBtnClicked) == 1)  // If it's True --> The restart button got clicked.
* If the restart button gets clicked:
  * Disk gets rotated to a random angle.
* For as long as the the goal is not reached yet, the countdown is not currently taking place and a countdown already took place in the current round:
  * The RotateObject() coroutine is called to rotate the Disk.
* After the Disk has stopped rotating, I check whether its angle is between 175° and 185°.
  If it is:
  * The goal has been successfully reached (for this round).
  * The Disk stops rotating for recognised gestures.

## restartP1.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **The script is responsible for the restart button's functionality.**
* In more detail:
  * Enabling/Disabling the restart button whenever necessary.
  * "Informing" the other scripts when the restart button gets clicked.
  * Changing the restart button's text and text's colour.
  * Changing the UI's roundText text and text's colour.
  * Returning to Menu when the 10th round is over and the button gets clicked.
* restartP1.cs has the code from Project4.1/restart.cs with some changes and adjustments.
* Because of having issues in passing public variables' values between the scripts (when loading the P1 Scene from the Menu Scene for unknown reasons), changes had to be made in the Update() function regarding the activation/deactivation of the restart button and the round number's text colour. After the changes, the function was created from the start, this time having much better organized if statements.
  * When the restart button gets clicked, the resButClicked variable gets the current round's number.
* After the 10th round is over, the button opens the Menu Scene instead of quiting the application.

## timerP1.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **The script is responsible for the countdown and stopwatch functionality.**
* In more detail:
  * Displaying a countdown timer before each round begins.
  * Calculating and displaying a stopwatch (elapsed time) which starts when a new round begins and stops when the goal gets reached.
  * Saving the elapsed time of each round to an external file (LogTimes_P1.txt).
* timerP1.cs has the code from Project4.1/timer.cs with some changes and adjustments.
* I use the previous_resBtnClicked variable to know when the restart button gets clicked by compairing it to the current resBtnClicked as shown below:
  * if ((resBtnClicked - previous_resBtnClicked) == 1)  // If it's True --> The restart button got clicked.

## quadTiltedUS.png
**&#x27BD; Arduino's Circuit Schema.**
___

# Project7/ Project6.1 (P2: Disk Rotation Using Average Distance)
##### This project is an evolution of Project6 with which only has minnor differences.
###### Related Files: [cameraControl.ino](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project5/cameraControl.ino), [diskRotationP2.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project7/Project6.1/diskRotationP2.cs), [restartP2.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project7/Project6.1/restartP2.cs), [timerP2.cs](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project7/Project6.1/timerP2.cs), [quadTiltedUS.png](https://github.com/Mikepag/Arduino-Unity3D/blob/master/Project4/quadTiltedUS.png).

## cameraControl.ino
**&#x27BD; C++ code uploaded to the Arduino. Performs Position Detection in 1D.**
* **Calculates an average distance value of all four Ultrasonic Sensors and sends it to Unity via the Serial Port.**
* Project5/cameraControl.ino code.
* It uses the four Ultrasonic Sensors to calculate four distances.
* Two average distances are calculated, one for the left breadboard (avgDistL) and one for the right breadboard (avgDistR).
* Finally, the value of the variable totalAvgDist is calculated based on the average value of avgDistL & avgDistR. This value gets sent to Unity.

## diskRotationP2.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **Receives the average value from the Serial Port, saves it in an array, calculates the average value of all array's values, compares the latest input with that average value and rotates the Disk clockwise/counterclockwise accordingly.**
* Project6/diskRotation.cs code was used as a base.
* It gets the value of variables resBtnClicked, unfinCD and didCntdown from the restartP2.cs and timerP2.cs scripts.
* I use the previous_resBtnClicked variable to know when the restart button gets clicked by compairing it to the current resBtnClicked as shown below:
  * if ((resBtnClicked - previous_resBtnClicked) == 1)  // If it's True --> The restart button got clicked.
* If the restart button gets clicked:
  * The Disk gets rotated to a random angle.
* For as long as the the goal is not reached yet, the countdown is not currently taking place and a countdown already took place in the current round:
   * If the recieved value (tempInput) is between the range [-32,-2]υ[2,32]:
   * tempInput's value is added to an array (recentValues[]).
   * The array's average value (recentAverage) is calculated.
      * If tempInput < recentAverage &#x279C; The Disk rotates Counterclockwise.
      * If tempInput > recentAverage &#x279C; The Disk rotates Clockwise.
   * **SMOOTH ROTATION:**
      * angleToRotate gets a value in [-maxATR, maxATR] == [-25, 25] based on the difference between the latest Input value and array's average value. The value expresses the direction and total degrees of rotation angle:
         * **angleToRotate = (float)(MaxATR * (recentValues[arrIn] - recentAverage)) / 64;**
      * If 0<angleToRotate<1, I set angleToRotate back to 1. If -1<angleToRotate<0, I set angleToRotate back to -1. That helps by rotating the disk even for very small hand gestures and improves accuracy.
      * When angleToRotate reaches 0, I programmed the disk to continue rotating (really slow) in the same direction for as long as my hand is detected inside the boundaries.
      * The DeleteRecentValues() function is used to empty the array when the tempInput gets out-of-bounds.
* After the Disk has stopped rotating, I check whether its angle is between 175° and 185°.
  If it is:
  * The goal has been successfully reached (for this round).
  * Disk stops rotating for recognised gestures.

## restartP2.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **The script is responsible for the restart button's functionality.**
* In more detail:
  * Enabling/Disabling the restart button whenever necessary.
  * "Informing" the other scripts when the restart button gets clicked.
  * Changing the restart button's text and text's colour.
  * Changing the UI's roundText text and text's colour.
  * Returning to Menu when the 10th round is over and the button gets clicked.
* restartP2.cs has the code from Project6/restart.cs with some changes and adjustments.
* Because of having issues in passing public variables' values between the scripts (when loading the P1 Scene from the Menu Scene for unknown reasons), changes had to be made in the Update() function regarding the activation/deactivation of the restart button and the round number's text colour. After the changes, the function was created from the start, this time having much better organized if statements.
  * When the restart button gets clicked, the resButClicked variable gets the current round's number.
* After the 10th round is over, the button opens the Menu Scene instead of quiting the application.

## timerP2.cs
**&#x27BD; C Sharp script attached to a Game Object called "Disk".**
* **The script is responsible for the countdown and stopwatch functionality.**
* In more detail:
  * Displaying a countdown timer before each round begins.
  * Calculating and displaying a stopwatch (elapsed time) which starts when a new round begins and stops when the goal gets reached.
  * Saving the elapsed time of each round to an external file (LogTimes_P1.txt).
* timerP2.cs has the code from Project6/timer.cs with some changes and adjustments.
* I use the previous_resBtnClicked variable to know when the restart button gets clicked by compairing it to the current resBtnClicked as shown below:
  * if ((resBtnClicked - previous_resBtnClicked) == 1)  // If it's True --> The restart button got clicked.

## quadTiltedUS.png
**&#x27BD; Arduino's Circuit Schema.**
