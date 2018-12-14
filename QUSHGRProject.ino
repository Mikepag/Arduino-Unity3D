 //Quad Ultrasonic Sensor - Hand Gesture Recognition Project
//_________________________Variable Definition:_________________________
const int trigBLPin = 4;   // Trigger pin of Bottom Left Ultrasonic Ranging Module HC-SR04.
const int echoBLPin = 5;   // Echo pin of Bottom Left Ultrasonic Ranging Module HC-SR04.
const int trigBRPin = 7;   // Trigger pin of Bottom Right Ultrasonic Ranging Module HC-SR04.
const int echoBRPin = 8;   // Echo pin of Bottom Right Ultrasonic Ranging Module HC-SR04.

const int trigTLPin = 10;   // Trigger pin of Top Left Ultrasonic Ranging Module HC-SR04.
const int echoTLPin = 11;   // Echo pin of Top Left Ultrasonic Ranging Module HC-SR04.
const int trigTRPin = 12;   // Trigger pin of Top Right Ultrasonic Ranging Module HC-SR04.
const int echoTRPin = 13;   // Echo pin of Top Right Ultrasonic Ranging Module HC-SR04.

const int standbyLedPin = 2;
const int flashingLedPin = 3;

float durationBL;
float durationBR;
float distanceBL;
float distanceBR;
int distBLInt;
int distBRInt;

float durationTL;
float durationTR;
float distanceTL;
float distanceTR;
int distTLInt;
int distTRInt;

int gesturotation;     // Contains the value that is beeing sent to Unity. == -1 when right-to-left gesture was recognised, == 1 when left-to-right gesture was recognised, ==0 when no gesture was recognised.

int distBLOLD = 0;     // Previous distance calculated by Bottom Left sensor.
int distBROLD = 0;     // Previous distance calculated by Bottom Right sensor.
int flagBL = 0;        // ==1 when something is detected in front of Bottom Left Sensor. Keeps its value for a specific amount of time.
int flagBR = 0;        // ==1 when something is detected in front of Bottom Right Sensor. Keeps its value for a specific amount of time.

int distTLOLD = 0;     // Previous distance calculated by Top Left sensor.
int distTROLD = 0;     // Previous distance calculated by Top Right sensor.
int flagTL = 0;        // ==1 when something is detected in front of Top Left Sensor. Keeps its value for a specific amount of time.
int flagTR = 0;        // ==1 when something is detected in front of Top Right Sensor. Keeps its value for a specific amount of time.

int timestepsABL = 0;  // Timesteps since something arrived in the area in front of Bottom Left Sensor.   (Arrived to Bottom Left sensor)
int timestepsABR = 0;  // Timesteps since something arrived in the area in front of Bottom Right Sensor.  (Arrived to Bottom Right sensor)
int timestepsLBL = 0;  // Timesteps since something left the area in front of Bottom Left Sensor.         (Left from Bottom Left sensor)
int timestepsLBR = 0;  // Timesteps since something left the area in front of Bottom Right Sensor.        (Left from Bottom Right sensor)

int timestepsATL = 0;  // Timesteps since something arrived in the area in front of Top Left Sensor.   (Arrived to Top Left sensor)
int timestepsATR = 0;  // Timesteps since something arrived in the area in front of Top Right Sensor.  (Arrived to Top Right sensor)
int timestepsLTL = 0;  // Timesteps since something left the area in front of Top Left Sensor.         (Left from Top Left sensor)
int timestepsLTR = 0;  // Timesteps since something left the area in front of Top Right Sensor.        (Left from Top Right sensor)

int topGesture = 0;    // Gesture recognised from top layer sensors. ==0 means no gesture recognised. ==1 means left-to-right gesture recognised. ==-1 means right-to-left gesture recognised.
int botGesture = 0;    // Gesture recognised from bottom layer sensors. ==0 means no gesture recognised. ==1 means left-to-right gesture recognised. ==-1 means right-to-left gesture recognised.

int sensorSwitch = 0; // 0== Switch Left Sensors ON, 1== Switch Right Sensors ON.


//_________________________SETUP():_________________________
void setup() {
  Serial.begin(9600);
  pinMode(trigBLPin, OUTPUT);
  pinMode(trigBRPin, OUTPUT);
  pinMode(echoBLPin, INPUT);
  pinMode(echoBRPin, INPUT);

  pinMode(trigTLPin, OUTPUT);
  pinMode(trigTRPin, OUTPUT);
  pinMode(echoTLPin, INPUT);
  pinMode(echoTRPin, INPUT);

  pinMode(standbyLedPin, OUTPUT);
  pinMode(flashingLedPin, OUTPUT);
}


//_________________________LOOP():_________________________
void loop() {
  //ledSwitchOFF();                         // Switching every led OFF().

  if(sensorSwitch == 0){                    // When sensorSwitch == 0, LEFT SENSORS emit and receive signal.
    digitalWrite(trigBLPin, LOW);
    delayMicroseconds(2);                   // Delay, to let sensor switch trigBLPin to LOW.
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigBLPin, HIGH);          // Emiting signal.
    delayMicroseconds(10);                  // Delay, to let sensor emit signal.
    digitalWrite(trigBLPin, LOW);
    digitalWrite(flashingLedPin, LOW);
    durationBL = pulseIn(echoBLPin, HIGH);  // Receiving signal and saving its duracion.
    distanceBL = durationBL/58.2;           // Calculating distance in centimeters, based on the speed of sound.
    distBLInt = distanceBL;                 // Converting float distance value to integer.

    digitalWrite(trigTLPin, LOW);
    delayMicroseconds(2);                   // Delay, to let sensor switch trigBLPin to LOW.
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigTLPin, HIGH);          // Emiting signal.
    delayMicroseconds(10);                  // Delay, to let sensor emit signal.
    digitalWrite(trigTLPin, LOW);
    digitalWrite(flashingLedPin, LOW);
    durationTL = pulseIn(echoTLPin, HIGH);  // Receiving signal and saving its duracion.
    distanceTL = durationTL/58.2;           // Calculating distance in centimeters, based on the speed of sound.
    distTLInt = distanceTL;                 // Converting float distance value to integer.


    ////Serial.print("L ");
    ////Serial.print(distBLInt);      // Printing distance on IDE's Serial Monitor. (COMMENT THIS LINE OUT WHEN SENDING DATA TO UNITY).
    //Serial.write(distBLInt);        // Passing distance value to Unity.
    //Serial.flush();                 // Waits for the transmission of outgoing serial data to complete.
  
    sensorSwitch = 1;                       // In the next loop, switch Right sensors On.
  }
  else{                                     // Else, when sensorSwitch == 1, RIGHT SENSORS emit and receive signal.
    digitalWrite(trigBRPin, LOW);
    delayMicroseconds(2);                   // Delay, to let sensor switch trigBLPin to LOW.
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigBRPin, HIGH);          // Emiting signal.
    delayMicroseconds(10);                  // Delay, to let sensor emit signal.
    digitalWrite(trigBRPin, LOW);
    digitalWrite(flashingLedPin, LOW);
    durationBR = pulseIn(echoBRPin, HIGH);  // Receiving signal and saving its duracion.
    distanceBR = durationBR/58.2;           // Calculating distance in centimeters, based on the speed of sound.
    distBRInt = distanceBR;                 // Converting float distance value to integer.

    digitalWrite(trigTRPin, LOW);
    delayMicroseconds(2);                   // Delay, to let sensor switch trigBLPin to LOW.
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigTRPin, HIGH);          // Emiting signal.
    delayMicroseconds(10);                  // Delay, to let sensor emit signal.
    digitalWrite(trigTRPin, LOW);
    digitalWrite(flashingLedPin, LOW);
    durationTR = pulseIn(echoTRPin, HIGH);  // Receiving signal and saving its duracion.
    distanceTR = durationTR/58.2;           // Calculating distance in centimeters, based on the speed of sound.
    distTRInt = distanceTR;                 // Converting float distance value to integer.


    ////Serial.print("    R ");
    ////Serial.println(distBRInt);    // Printing distance on IDE's Serial Monitor. (COMMENT THIS LINE OUT WHEN SENDING DATA TO UNITY).
    //Serial.write(distBRInt);        // Passing distance value to Unity.
    //Serial.flush();                 // Waits for the transmission of outgoing serial data to complete.
  
    //ledSwitchON(distanceBR);              // Switching leds ON, according to distance.
    sensorSwitch = 0;                       // In the next loop, switch Right sensors On.
  }

//- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

  if(timestepsABL < 10){
    timestepsABL++;
  }
  if(timestepsABR < 10){
    timestepsABR++;
  }
  if(timestepsATL < 10){
    timestepsATL++;
  }
  if(timestepsATR < 10){
    timestepsATR++;
  }

  if(flagBL == 1 && timestepsLBL <3){     // If something was detected in front of the left sensor AND it was detected at maximum 2 timesteps ago...
    timestepsLBL++;                       // ...then, increment timestepsLBL.
  }
  else{                                   // Else, if nothing was detected in front of the sensor OR something was detected but more than 3 timesteps ago...
    flagBL = 0;                           // ...then, flagBL = 0, which roughly means "nothing was detected in front of the bottom left sensor".
  }

  if(flagBR == 1 && timestepsLBR <3){     // If something was detected in front of the bottom right sensor AND it was detected at maximum 2 timesteps ago...
    timestepsLBR++;                       // ...then, increment timestepsLBR.
  }
  else{                                   // Else, if nothing was detected in front of the sensor OR something was detected but more than 3 timesteps ago...
    flagBR = 0;                           // ...then, flagBR = 0, which roughly means "nothing was detected in front of the bottom right sensor".
  }

  if(flagTL == 1 && timestepsLTL <3){     // If something was detected in front of the top left sensor AND it was detected at maximum 2 timesteps ago...
    timestepsLTL++;                       // ...then, increment timestepsLTL.
  }
  else{                                   // Else, if nothing was detected in front of the sensor OR something was detected but more than 3 timesteps ago...
    flagTL = 0;                           // ...then, flagTL = 0, which roughly means "nothing was detected in front of the top left sensor".
  }

  if(flagTR == 1 && timestepsLTR <3){     // If something was detected in front of the top right sensor AND it was detected at maximum 2 timesteps ago...
    timestepsLTR++;                       // ...then, increment timestepsLTR.
  }
  else{                                   // Else, if nothing was detected in front of the sensor OR something was detected but more than 3 timesteps ago...
    flagTR = 0;                           // ...then, flagTR = 0, which roughly means "nothing was detected in front of the top right sensor".
  }

  gesturotation = 0;                      // This value makes the cube in Unity to stop rotating and stay still.
  topGesture = 0;
  botGesture = 0;

//____________________________BOTTOM SENSORS____________________________

  if(abs(distBLOLD-distBLInt) < abs(distBROLD-distBRInt) && (distBLInt < 20)){   // If the absolute value of the difference between the two last Bottom Left sensor distances is smaller than the absolute value of the difference between the two last Bottom Right sensor distances...
                                                                            // ...AND the last detected distance is smaller than 20cm, then something is in front of the Bottom Left Sensor...
    if(flagBL == 0){
      timestepsABL = 0;                                                      // If flagBL was 0, that means it is the "first-after-nothing-was-detected" time, that something is beeing detected in front of Bottom Left Sensor, so we set timestepsABL to 0. 
    }
    flagBL = 1;                                                              // ...so flagBL = 1.
    timestepsLBL = 0;                                                        // timestepsLBL = 0 because something is CURRENTLY in front of the sensor. When it is no longer detected, timestepsLBL will increment.
    if(flagBR == 1){                     // If something was also detected in front of the Bottom Right sensor in the last 3 timesteps...
      //Serial.print("\nMOVE LEFT\n");    // ...that means something moved from Right to Left! Gesture recognised successfully.
      //Serial.print("timestepsABR = ");
      //Serial.println(-timestepsABR);

      //---> Serial.write(timestepsABR);
      //---> Serial.flush();

      botGesture = -1;
      
      gesturotation = -1;                // That value rotates the cube in Unity to the Left.
      flagBR = 0;                        // Since a gesture to-the-left was recognised, we need to set flagBR=0. Otherwise, if in the next timestep something is still detected in front of the bottom left sensor, another (false) gesture to-the-left will be recognised.
    }
  }
  if(abs(distBLOLD-distBLInt) > abs(distBROLD-distBRInt) && (distBRInt < 20)){   // If the absolute value of the difference between the two last Bottom Left sensor distances is greater than the absolute value of the difference between the two last Bottom Right sensor distances...
                                                                            // ...AND the last detected distance is smaller than 20cm, then something is in front of the Bottom Right Sensor...
    if(flagBR == 0){
      timestepsABR = 0;                                                      // If flagBR was 0, that means it is the "first-after-nothing-was-detected" time, that something is beeing detected in front of Bottom Right Sensor, so we set timestepsABR to 0. 
    }                                                        
    flagBR = 1;                                                              // ...so flagBR = 1.
    timestepsLBR = 0;                                                        // timestepsLBR = 0 because something is CURRENTLY in front of the sensor. When it is no longer detected, timestepsLBR will increment.
    if(flagBL == 1){                     // If something was also detected in front of the Bottom Left sensor in the last 3 timesteps...
      //Serial.print("\nMOVE RIGHT\n");   // ...that means something moved from Left to Right! Gesture recognised successfully.
      //Serial.print("timestepsABL = ");
      //Serial.println(timestepsABL + 10);

      //timestepsABL = 10 + timestepsABL;
      //---> Serial.write(timestepsABL);
      //---> Serial.flush();

      botGesture = 1;
      gesturotation = 1;                 // That value rotates the cube in Unity to the Right.
      flagBL = 0;                        // Since a gesture to-the-right was recognised, we need to set flagBL=0. Otherwise, if in the next timestep something is still detected in front of the bottom right sensor, another (false) gesture to-the-right will be recognised.
    }
  }




//____________________________TOP SENSORS____________________________


  if(abs(distTLOLD-distTLInt) < abs(distTROLD-distTRInt) && (distTLInt < 20)){   // If the absolute value of the difference between the two last Bottom Left sensor distances is smaller than the absolute value of the difference between the two last Bottom Right sensor distances...
                                                                            // ...AND the last detected distance is smaller than 20cm, then something is in front of the Bottom Left Sensor...
    if(flagTL == 0){
      timestepsATL = 0;                                                      // If flagBL was 0, that means it is the "first-after-nothing-was-detected" time, that something is beeing detected in front of Bottom Left Sensor, so we set timestepsABL to 0. 
    }
    flagTL = 1;                                                              // ...so flagBL = 1.
    timestepsLTL = 0;                                                        // timestepsLBL = 0 because something is CURRENTLY in front of the sensor. When it is no longer detected, timestepsLBL will increment.
    if(flagTR == 1){                     // If something was also detected in front of the Bottom Right sensor in the last 3 timesteps...
      //Serial.print("\nMOVE LEFT\n");    // ...that means something moved from Right to Left! Gesture recognised successfully.
      //Serial.print("timestepsATR = ");
      //Serial.println(-timestepsATR);
    
      //---> Serial.write(timestepsATR);
      //---> Serial.flush();

      topGesture = -1;
      gesturotation = -1;                // That value rotates the cube in Unity to the Left.
      flagTR = 0;                        // Since a gesture to-the-left was recognised, we need to set flagBR=0. Otherwise, if in the next timestep something is still detected in front of the bottom left sensor, another (false) gesture to-the-left will be recognised.
    }
  }
  if(abs(distTLOLD-distTLInt) > abs(distTROLD-distTRInt) && (distTRInt < 20)){   // If the absolute value of the difference between the two last Bottom Left sensor distances is greater than the absolute value of the difference between the two last Bottom Right sensor distances...
                                                                            // ...AND the last detected distance is smaller than 20cm, then something is in front of the Bottom Right Sensor...
    if(flagTR == 0){
      timestepsATR = 0;                                                      // If flagBR was 0, that means it is the "first-after-nothing-was-detected" time, that something is beeing detected in front of Bottom Right Sensor, so we set timestepsABR to 0. 
    }                                                        
    flagTR = 1;                                                              // ...so flagBR = 1.
    timestepsLTR = 0;                                                        // timestepsLBR = 0 because something is CURRENTLY in front of the sensor. When it is no longer detected, timestepsLBR will increment.
    if(flagTL == 1){                     // If something was also detected in front of the Bottom Left sensor in the last 3 timesteps...
      //Serial.print("\nMOVE RIGHT\n");   // ...that means something moved from Left to Right! Gesture recognised successfully.
      //Serial.print("timestepsATL = ");
      //Serial.println(timestepsATL + 10);

      //timestepsATL = 10 + timestepsATL;
      //---> Serial.write(timestepsABL);
      //---> Serial.flush();

      topGesture = 1;
      gesturotation = 1;                 // That value rotates the cube in Unity to the Right.
      flagTL = 0;                        // Since a gesture to-the-right was recognised, we need to set flagBL=0. Otherwise, if in the next timestep something is still detected in front of the bottom right sensor, another (false) gesture to-the-right will be recognised.
    }
  }

//________________________________________________________


  if(topGesture == 0 && botGesture == 0){
    //Serial.print("\nDONT MOVE\n");
  }
  else if(topGesture == 0 && botGesture == -1){
    Serial.print("\nMOVE LEFT (bottom)\n");
    Serial.print("timestepsABR = ");
    Serial.println(-timestepsABR);

    timestepsABL = 0;                  // Since a gesture was recognised, we set timestepsABL...
    timestepsABR = 0;                  // ...and timestepsABR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
  }
  else if(topGesture == 0 && botGesture == 1){
    Serial.print("\nMOVE RIGHT (bottom)\n");
    Serial.print("timestepsABL = ");
    Serial.println(timestepsABL + 10);
    timestepsABL = 0;                  // Since a gesture was recognised, we set timestepsABL...
    timestepsABR = 0;                  // ...and timestepsABR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
  }
  else if(topGesture == -1 && botGesture == 0){
    Serial.print("\nMOVE LEFT (top)\n");
    Serial.print("timestepsATR = ");
    Serial.println(-timestepsATR);
    timestepsATL = 0;                  // Since a gesture was recognised, we set timestepsATL...
    timestepsATR = 0;                  // ...and timestepsATR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
  }
  else if(topGesture == 1 && botGesture == 0){
    Serial.print("\nMOVE RIGHT (top)\n");
    Serial.print("timestepsATL = ");
    Serial.println(timestepsATL + 10);
    timestepsATL = 0;                  // Since a gesture was recognised, we set timestepsATL...
    timestepsATR = 0;                  // ...and timestepsATR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
  }
  else if(topGesture == -1 && botGesture == -1){
    Serial.print("\nMOVE LEFT (top+bottom)\n");
    Serial.print("(-timestepsABR - timestepsATR)/2 = ");
    Serial.println((-timestepsABR - timestepsATR)/2);
    timestepsATL = 0;                  // Since a gesture was recognised, we set timestepsATL...
    timestepsATR = 0;                  // ...and timestepsATR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
    timestepsABL = 0;                  // Since a gesture was recognised, we set timestepsABL...
    timestepsABR = 0;                  // ...and timestepsABR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
  }
  else if(topGesture == 1 && botGesture == 1){
    Serial.print("\nMOVE RIGHT (top+bottom)\n");
    Serial.print("(timestepsABL + timestepsATL)/2 = ");
    Serial.println((timestepsABL + timestepsATL)/2);
    timestepsATL = 0;                  // Since a gesture was recognised, we set timestepsATL...
    timestepsATR = 0;                  // ...and timestepsATR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
    timestepsABL = 0;                  // Since a gesture was recognised, we set timestepsATL...
    timestepsABR = 0;                  // ...and timestepsATR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
  }
  else if(topGesture == 1 && botGesture == -1){
    Serial.print("\n ERROR! MOVE RIGHT(top), MOVE LEFT(bottom)\n");
    timestepsATL = 0;                  // Since a gesture was recognised, we set timestepsATL...
    timestepsATR = 0;                  // ...and timestepsATR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
    timestepsABL = 0;                  // Since a gesture was recognised, we set timestepsABL...
    timestepsABR = 0;                  // ...and timestepsABR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
  }
  else if(topGesture == -1 && botGesture == 1){
    Serial.print("\n ERROR! MOVE LEFT(top), MOVE RIGHT(bottom)\n");
    timestepsATL = 0;                  // Since a gesture was recognised, we set timestepsATL...
    timestepsATR = 0;                  // ...and timestepsATR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
    timestepsABL = 0;                  // Since a gesture was recognised, we set timestepsABL...
    timestepsABR = 0;                  // ...and timestepsABR to 0, so they start counting timesteps for the next-to-be-recognised gesture.
  }



  /////if(gesturotation == 0){
    /////Serial.print("\nDONT MOVE\n");
    //---> Serial.write(gesturotation);    // Passing rotation value to Unity.
    //---> Serial.flush();                 // Waits for the transmission of outgoing serial data to complete.
  /////}
  
  delay(50);   // Delay between each time that either left or right sensor are "enabled".
}


//_________________________FUNCTIONS:_________________________

/*
void ledSwitchOFF(){
  digitalWrite(standbyLedPin, LOW);
  digitalWrite(flashingLedPin, LOW);

  digitalWrite(led1Pin, LOW);
  digitalWrite(led2Pin, LOW);
  digitalWrite(led3Pin, LOW);
  digitalWrite(led4Pin, LOW);
  digitalWrite(led5Pin, LOW);
}

void ledSwitchON(int distance){
  if(distance<10){
    digitalWrite(standbyLedPin, HIGH);
  }
  else if(distance<20){
    digitalWrite(led1Pin, HIGH);
  }
  else if(distance<30){
    digitalWrite(led1Pin, HIGH);
    digitalWrite(led2Pin, HIGH);
  }
  else if(distance<40){
    digitalWrite(led1Pin, HIGH);
    digitalWrite(led2Pin, HIGH);
    digitalWrite(led3Pin, HIGH);
  }
  else if(distance<50){
    digitalWrite(led1Pin, HIGH);
    digitalWrite(led2Pin, HIGH);
    digitalWrite(led3Pin, HIGH);
    digitalWrite(led4Pin, HIGH);
  }
  else if(distance<100){
    digitalWrite(led1Pin, HIGH);
    digitalWrite(led2Pin, HIGH);
    digitalWrite(led3Pin, HIGH);
    digitalWrite(led4Pin, HIGH);
    digitalWrite(led5Pin, HIGH);
  }
  else{
    digitalWrite(led5Pin, HIGH);
  }
}

*/
