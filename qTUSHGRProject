//Quad Tilted Ultrasonic Sensor - Hand Gesture Recognition Project ("Book Flipping" - "Slapping" Gestures)
//_________________________Variable Definition:_________________________
// BL --> LL
// BR --> LR
// TL --> RL
// TR --> RR
const int trigLLPin = 4;   // Trigger pin of Left Left Ultrasonic Ranging Module HC-SR04.
const int echoLLPin = 5;   // Echo pin of Left Left Ultrasonic Ranging Module HC-SR04.
const int trigLRPin = 7;   // Trigger pin of Left Right Ultrasonic Ranging Module HC-SR04.
const int echoLRPin = 8;   // Echo pin of Left Right Ultrasonic Ranging Module HC-SR04.

const int trigRLPin = 10;   // Trigger pin of Right Left Ultrasonic Ranging Module HC-SR04.
const int echoRLPin = 11;   // Echo pin of Right Left Ultrasonic Ranging Module HC-SR04.
const int trigRRPin = 12;   // Trigger pin of Right Right Ultrasonic Ranging Module HC-SR04.
const int echoRRPin = 13;   // Echo pin of Right Right Ultrasonic Ranging Module HC-SR04.

const int standbyLedPin = 2;
const int flashingLedPin = 3;

float durationLL;           // Time duration between emiting and receiving signal from the left left sensor.
float durationLR;           // Time duration between emiting and receiving signal from the left right sensor.
float distanceLL;           // Distance calulated based on durationLL.
float distanceLR;           // Distance calulated based on durationLR.
int distLLInt;              // Integer value of float distanceLL.
int distLRInt;              // Integer value of float distanceLR.

float durationRL;           // Time duration between emiting and receiving signal from the Right left sensor.
float durationRR;           // Time duration between emiting and receiving signal from the Right right sensor.
float distanceRL;           // Distance calulated based on durationRL.
float distanceRR;           // Distance calulated based on durationRR.
int distRLInt;              // Integer value of float distanceRL.
int distRRInt;              // Integer value of float distanceRR.

int gesturotation;     // Contains the value that is beeing sent to Unity. == -1 when right-to-left gesture was recognised, == 1 when left-to-right gesture was recognised, ==0 when no gesture was recognised.

int distLLOLD = 0;     // Previous distance calculated by Left Left sensor.
int distLROLD = 0;     // Previous distance calculated by Left Right sensor.
int flagLL = 0;        // ==1 when something is detected in front of Left Left Sensor. Keeps its value for a specific amount of time.
int flagLR = 0;        // ==1 when something is detected in front of Left Right Sensor. Keeps its value for a specific amount of time.

int distRLOLD = 0;     // Previous distance calculated by Right Left sensor.
int distRROLD = 0;     // Previous distance calculated by Right Right sensor.
int flagRL = 0;        // ==1 when something is detected in front of Right Left Sensor. Keeps its value for a specific amount of time.
int flagRR = 0;        // ==1 when something is detected in front of Right Right Sensor. Keeps its value for a specific amount of time.

int avgDistL = -1; // Average distance value from the Left Sensors. ==-1 means there were not any distances from Left sensors in this timestep, so could not calculate average distance.
int flagL = 0;     // ==1 when something is detected in front of Left Right Sensor. Keeps its value for a specific amount of time.
int prevFlagL = 0; // Stores the previous value of variable flagL. 

int avgDistR = -1; // Average distance value from the Right Sensors. ==-1 means there were not any distances from Right sensors in this timestep, so could not calculate average distance.
int flagR = 0;     // ==1 when something is detected in front of Left Right Sensor. Keeps its value for a specific amount of time.
int prevFlagR = 0; // Stores the previous value of variable flagL.

int timestepsALL = 0;  // Timesteps since something arrived in the area in front of Left Left Sensor.   (Arrived to Left Left sensor)
int timestepsALR = 0;  // Timesteps since something arrived in the area in front of Left Right Sensor.  (Arrived to Left Right sensor)
int timestepsLLL = 0;  // Timesteps since something left the area in front of Left Left Sensor.         (Left from Left Left sensor)
int timestepsLLR = 0;  // Timesteps since something left the area in front of Left Right Sensor.        (Left from Left Right sensor)

int timestepsARL = 0;  // Timesteps since something arrived in the area in front of Right Left Sensor.   (Arrived to Right Left sensor)
int timestepsARR = 0;  // Timesteps since something arrived in the area in front of Right Right Sensor.  (Arrived to Right Right sensor)
int timestepsLRL = 0;  // Timesteps since something left the area in front of Right Left Sensor.         (Left from Right Left sensor)
int timestepsLRR = 0;  // Timesteps since something left the area in front of Right Right Sensor.        (Left from Right Right sensor)

int timestepsAL = 0;   // Timesteps since something arrived in the area in front of at least one of the Left Sensors.    (Arrived to Left sensor(s))
int timestepsLL = 0;   // Timesteps since something left the area in front of Left Sensors.                              (Left from Left sensors)
int timestepsAR = 0;   // Timesteps since something arrived in the area in front of at least one of the Right Sensors.   (Arrived to Right sensor(s))
int timestepsLR = 0;   // Timesteps since something left the area in front of Right Sensors.                             (Left from Right sensors)

int timestepsFSRG = -1; // Timesteps since First Sensors (top or bottom) Recognised Gesture. Used to handle the case of "simultaneous" recognition of gestures. (simultaneous == within a period of ~250ms). (==-1 means it is "disabled" so it doesn't increment).

int topGesture = 0;    // Gesture recognised from top layer sensors. ==0 means no gesture recognised. ==1 means left-to-right gesture recognised. ==-1 means right-to-left gesture recognised.
int botGesture = 0;    // Gesture recognised from bottom layer sensors. ==0 means no gesture recognised. ==1 means left-to-right gesture recognised. ==-1 means right-to-left gesture recognised.

int sensorSwitch = 0;  // 0== Switch Left Sensors ON, 1== Switch Right Sensors ON.

int space = 0; // DELETE THIS AFTER TESTING!


//_________________________SETUP():_________________________
void setup() {
  Serial.begin(9600);
  pinMode(trigLLPin, OUTPUT);
  pinMode(trigLRPin, OUTPUT);
  pinMode(echoLLPin, INPUT);
  pinMode(echoLRPin, INPUT);

  pinMode(trigRLPin, OUTPUT);
  pinMode(trigRRPin, OUTPUT);
  pinMode(echoRLPin, INPUT);
  pinMode(echoRRPin, INPUT);

  pinMode(standbyLedPin, OUTPUT);
  pinMode(flashingLedPin, OUTPUT);
}


//_________________________LOOP():_________________________
void loop() {
  
  // Emiting signal from sensors, calculating distances:
  if(sensorSwitch == 0){                    // When sensorSwitch == 0, LEFT SENSORS emit and receive signal.
    digitalWrite(trigLLPin, LOW);
    delayMicroseconds(2);                   // Delay, to let sensor switch trigLLPin to LOW.
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigLLPin, HIGH);          // Emiting signal.
    delayMicroseconds(10);                  // Delay, to let sensor emit signal.
    digitalWrite(trigLLPin, LOW);
    digitalWrite(flashingLedPin, LOW);
    durationLL = pulseIn(echoLLPin, HIGH);  // Receiving signal and saving its duracion.
    distanceLL = durationLL/58.2;           // Calculating distance in centimeters, based on the speed of sound.
    distLLInt = distanceLL;                 // Converting float distance value to integer.
    

    digitalWrite(trigRLPin, LOW);
    delayMicroseconds(2);                   // Delay, to let sensor switch trigRLPin to LOW.
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigRLPin, HIGH);          // Emiting signal.
    delayMicroseconds(10);                  // Delay, to let sensor emit signal.
    digitalWrite(trigRLPin, LOW);
    digitalWrite(flashingLedPin, LOW);
    durationRL = pulseIn(echoRLPin, HIGH);  // Receiving signal and saving its duracion.
    distanceRL = durationRL/58.2;           // Calculating distance in centimeters, based on the speed of sound.
    distRLInt = distanceRL;                 // Converting float distance value to integer.
    
      
    sensorSwitch = 1;                       // In the next loop, switch Right sensors On.
  }
  else{                                     // Else, when sensorSwitch == 1, RIGHT SENSORS emit and receive signal.
    digitalWrite(trigLRPin, LOW);
    delayMicroseconds(2);                   // Delay, to let sensor switch trigLRPin to LOW.
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigLRPin, HIGH);          // Emiting signal.
    delayMicroseconds(10);                  // Delay, to let sensor emit signal.
    digitalWrite(trigLRPin, LOW);
    digitalWrite(flashingLedPin, LOW);
    durationLR = pulseIn(echoLRPin, HIGH);  // Receiving signal and saving its duracion.
    distanceLR = durationLR/58.2;           // Calculating distance in centimeters, based on the speed of sound.
    distLRInt = distanceLR;                 // Converting float distance value to integer.
    

    digitalWrite(trigRRPin, LOW);
    delayMicroseconds(2);                   // Delay, to let sensor switch trigRRPin to LOW.
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigRRPin, HIGH);          // Emiting signal.
    delayMicroseconds(10);                  // Delay, to let sensor emit signal.
    digitalWrite(trigRRPin, LOW);
    digitalWrite(flashingLedPin, LOW);
    durationRR = pulseIn(echoRRPin, HIGH);  // Receiving signal and saving its duracion.
    distanceRR = durationRR/58.2;           // Calculating distance in centimeters, based on the speed of sound.
    distRRInt = distanceRR;                 // Converting float distance value to integer.
    

    sensorSwitch = 0;                       // In the next loop, switch Right sensors On.
  }

/*
//Book Flipping (Delete this after debugging).
  if(distLLInt<=30 || distLRInt<=30 || distRLInt<=30 || distRRInt<=30){
    
    //Serial.print("LL:");
    if(distLLInt>30){
      //Serial.print(22);
      Serial.print(". ");
    }
    else{
      Serial.print(distLLInt); Serial.print(" ");
    }
  
    //Serial.print("        LR:");
    if(distLRInt>30){
      //Serial.print(22);
      Serial.print(". ");
    }
    else{
      Serial.print(distLRInt); Serial.print(" ");
    }
  
    //Serial.print("    RL:");
    if(distRLInt>30){
      //Serial.print(22);
      Serial.print(". ");
    }
    else{
      Serial.print(distRLInt); Serial.print(" ");
    }
  
    //Serial.print("    RR:");
    if(distRRInt>30){
      //Serial.print(22);
      Serial.print(". ");
    }
    else{
      Serial.print(distRRInt); Serial.print(" ");
    }
    
    Serial.print("\n");
    space = 0;
  }
  else{
    if(space == 0){
      Serial.print("\n\n");
      space = 1;
    }
  }
*/

//- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Setting flags & Incrementing timesteps:

  if(timestepsAL < 10){    // If my hand Arrived in front of Left sensor(s) less than 10 timesteps ago,
    timestepsAL++;         // then, increment those timesteps.
  }// I use these timesteps to determine the hand gesture's speed (according to the amount of time it took my hand to get from the first to the second sensor).
  
  if(timestepsAR < 10){    // If my hand Arrived in front of Right sensor(s) less than 10 timesteps ago,
    timestepsAR++;         // then, increment those timesteps.
  }// I use these timesteps to determine the hand gesture's speed (according to the amount of time it took my hand to get from the first to the second sensor).
  
  if(flagL == 1 && timestepsLL <3){     // If something was detected in front of the left sensors AND it was detected at maximum 2 timesteps ago...
      timestepsLL++;                       // ...then, increment timestepsLL.
  }
  else{                                   // Else, if nothing was detected in front of the sensors OR something was detected but more than 3 timesteps ago...
    flagL = 0;                           // ...then, flagL = 0, which roughly means "nothing was detected in front of the Left sensors".
  }
  
  if(flagR == 1 && timestepsLR <3){     // If something was detected in front of the right sensors AND it was detected at maximum 2 timesteps ago...
    timestepsLR++;                       // ...then, increment timestepsLR.
  }
  else{                                   // Else, if nothing was detected in front of the sensor OR something was detected but more than 3 timesteps ago...
    flagR = 0;                           // ...then, flagR = 0, which roughly means "nothing was detected in front of the Right sensors".
  }


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
// Calculating average distances for left and right breadboard, setting new values to flagL and flagR.

  if(distLLInt <= 30 || distLRInt <= 30 || distRLInt <= 30 || distRRInt <= 30){ // If at least one of the Sensors of the Left or Right Breadboard detects something...
      
    //LEFT BREADBOARD
    if(distLLInt <= 30 || distLRInt <= 30){
      if(distLLInt <= 30 && distLRInt <= 30){ // If both Left Sensors detect something at the same time...
        avgDistL = (distLLInt + distLRInt)/2; //... avgDist =  M.O. of the two Left distances
      }
      else if(distLLInt <= 30){ // Else, if only LL Sensor detects something...
        avgDistL = distLLInt;
      }
      else if(distLRInt <= 30){ //  Else, if only LR Sensor detects something...
        avgDistL = distLRInt;
      }
    
      if(flagL == 0){ 
        timestepsAL = 0;     // If flagL was 0, that means it is the "first-after-nothing-was-detected" time, that something is beeing detected in front of Left Sensor(s), so we set timestepsAL to 0.
        prevFlagL = 0;       // Keeping previous value of flagL before changing it to 1.
      }
      else{
        prevFlagL = 1;         // Keeping previous value of flagL before changing it to 1.
      }
      flagL = 1;  // ==1 means something was detected from Left Sensor(s) at maximum 3 timesteps ago.
      timestepsLL=0; // timestepsLL = 0 because something is CURRENTLY in front of the sensors. When it is no longer detected, timestepsLL will increment.
    }
    //else{
    //  avgDistL = -1;
    //}
    
    //RIGHT BREADBOARD
    if(distRLInt <= 30 || distRRInt <= 30){ // If at least one of the Sensors of the Right Breadboard detects something...
      if(distRLInt <= 30 && distRRInt <= 30){ // If both Right Sensors detect something at the same time...
        avgDistR = (distRLInt + distRRInt)/2; //... avgDist =  M.O. of the two Right distances
      }
      else if(distRLInt <= 30){ // Else, if only RL Sensor detects something...
        avgDistR = distRLInt;
      }
      else if(distRRInt <= 30){ //  Else, if only RR Sensor detects something...
        avgDistR = distRRInt;
      }
    
      if(flagR == 0){
        timestepsAR = 0;     // If flagR was 0, that means it is the "first-after-nothing-was-detected" time, that something is beeing detected in front of Right Sensor(s), so we set timestepsAR to 0. 
        prevFlagR = 0;       // Keeping previous value of flagR before changing it to 1.
      }
      else{
        prevFlagR = 1;       // Keeping previous value of flagR before changing it to 1.
      }
      flagR = 1;  // ==1 means something was detected from Right Sensor(s) at maximum 3 timesteps ago.
      timestepsLR=0; // timestepsLR = 0 because something is CURRENTLY in front of the sensors. When it is no longer detected, timestepsLR will increment.
            
    }
    //else{
    //  avgDistR = -1;
    //}
  }


// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// Comparing distances, recognising gestures for each set of sensors:

  if(flagL == 1 && flagR == 1){
    if(prevFlagL == 0 && prevFlagR == 1){
      //Serial.print("- - - MOVE RIGHT - - -");
      //Serial.print(" timestepsAR: ");
      //Serial.print(timestepsAR);
      //Serial.print("\n\n\n");
      timestepsAR = 10 + timestepsAR;  //...increment timestepsAR's value by 10. (I do this so the value I send to Unity is 1-10 for right-to-left gestures and 11-20 for left-to-right gestures).
      Serial.write(timestepsAR);       //...send the value of timestepsAR which moves the cube to the right.
      Serial.flush();
      

    }
    else if(prevFlagR == 0 && prevFlagL ==1){
      //Serial.print("- - - MOVE LEFT - - -");
      //Serial.print(" timestepsAL: ");
      //Serial.print(timestepsAL);
      Serial.print("\n\n\n");
      Serial.write(timestepsAL); //...send the value of timestepsAL which moves the cube to the left.
      Serial.flush();
    }   
  }
  else{
    //Serial.print("0\n");
    Serial.write(0);                        // ...send value "0" to Unity which doesn't move the cube.
    Serial.flush();
  }

  
  delay(50);   // Delay between each time that either left or right sensor are "enabled".
}
