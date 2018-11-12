//Dual Ultrasonic Sensor - Hand Gesture Recognition Project
//_________________________Variable Definition:_________________________
const int trigLPin = 4;   // Trigger pin of Left Ultrasonic Ranging Module HC-SR04.
const int echoLPin = 5;   // Echo pin of Left Ultrasonic Ranging Module HC-SR04.
const int trigRPin = 7;   // Trigger pin of Right Ultrasonic Ranging Module HC-SR04.
const int echoRPin = 8;   // Echo pin of Right Ultrasonic Ranging Module HC-SR04.

const int standbyLedPin = 2;
const int flashingLedPin = 3;

const int led1Pin = 13;
const int led2Pin = 12;
const int led3Pin = 11;
const int led4Pin = 10;
const int led5Pin = 9;

float durationL;
float durationR;
float distanceL;
float distanceR;
int distLInt;
int distRInt;

int rotation; // contains the value that is beeing sent to Unity. ==0 to rotate Left, ==1 to rotate Right, ==-1 to not rotate.

int distLOLD = 0;
int distROLD = 0;
int flagL = 0;        // ==1 when something is detected in front of left Sensor. Keeps its value for a specific amount of time.
int flagR = 0;        // ==1 when something is detected in front of right Sensor. Keeps its value for a specific amount of time.
int timestepsL = 0;
int timestepsR = 0;

int sensorSwitch = 0; // 0== Switch Left Sensor ON, 1== Switch Right Sensor ON.


//_________________________SETUP():_________________________
void setup() {
  Serial.begin(9600);
  pinMode(trigLPin, OUTPUT);
  pinMode(trigRPin, OUTPUT);
  pinMode(echoLPin, INPUT);
  pinMode(echoRPin, INPUT);

  pinMode(standbyLedPin, OUTPUT);
  pinMode(flashingLedPin, OUTPUT);
  
  pinMode(led1Pin, OUTPUT);
  pinMode(led2Pin, OUTPUT);
  pinMode(led3Pin, OUTPUT);
  pinMode(led4Pin, OUTPUT);
  pinMode(led5Pin, OUTPUT);
}


//_________________________LOOP():_________________________
void loop() {
  ledSwitchOFF();                        // Switching every led OFF().

  if(sensorSwitch == 0){                 // When sensorSwitch == 0, LEFT SENSOR emits and receives signal.
    digitalWrite(trigLPin, LOW);
    delayMicroseconds(2);                // Delay, to let sensor switch trigLPin to LOW.
  
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigLPin, HIGH);        // Emiting signal.
    delayMicroseconds(10);               // Delay, to let sensor emit signal.
    digitalWrite(trigLPin, LOW);
    digitalWrite(flashingLedPin, LOW);
  
    durationL = pulseIn(echoLPin, HIGH); // Receiving signal and saving its duracion.
    distanceL = durationL/58.2;          // Calculating distance in centimeters, based on the speed of sound.
    distLInt = distanceL;                // Converting float distance value to integer.
  
    ////Serial.print("L ");
    ////Serial.print(distLInt);              // Printing distance on IDE's Serial Monitor. (COMMENT THIS LINE OUT WHEN SENDING DATA TO UNITY).
    //Serial.write(distLInt);            // Passing distance value to Unity.
    //Serial.flush();                    // Waits for the transmission of outgoing serial data to complete.
  
    ledSwitchON(distanceL);              // Switching leds ON, according to distance.
    sensorSwitch = 1;                    // In the next loop, switch Right sensor On.
  }
  else{                                  // Else, when sensorSwitch == 1, RIGHT SENSOR emits and receives signal.
    digitalWrite(trigRPin, LOW);
    delayMicroseconds(2);                // Delay, to let sensor switch trigLPin to LOW.
  
    digitalWrite(flashingLedPin, HIGH);
    digitalWrite(trigRPin, HIGH);        // Emiting signal.
    delayMicroseconds(10);               // Delay, to let sensor emit signal.
    digitalWrite(trigRPin, LOW);
    digitalWrite(flashingLedPin, LOW);
  
    durationR = pulseIn(echoRPin, HIGH); // Receiving signal and saving its duracion.
    distanceR = durationR/58.2;          // Calculating distance in centimeters, based on the speed of sound.
    distRInt = distanceR;                // Converting float distance value to integer.
  

    ////Serial.print("    R ");
    ////Serial.println(distRInt);            // Printing distance on IDE's Serial Monitor. (COMMENT THIS LINE OUT WHEN SENDING DATA TO UNITY).
    //Serial.write(distRInt);            // Passing distance value to Unity.
    //Serial.flush();                    // Waits for the transmission of outgoing serial data to complete.
  
    ledSwitchON(distanceR);              // Switching leds ON, according to distance.
    sensorSwitch = 0;                    // In the next loop, switch Right sensor On.
  }

//- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

  if(flagL == 1 && timestepsL <3){        // If something was detected in front of the left sensor AND it was detected at maximum 2 timesteps ago...
    timestepsL++;                         // ...then, increment timestepsL.
  }
  else{                                   // Else, if nothing was detected in front of the sensor OR something was detected but more than 3 timesteps ago...
    flagL = 0;                            // ...then, flagL = 0, which roughly means "nothing was detected in front of the left sensor".
  }

  if(flagR == 1 && timestepsR <3){        // If something was detected in front of the right sensor AND it was detected at maximum 2 timesteps ago...
    timestepsR++;                         // ...then, increment timestepsR.
  }
  else{                                   // Else, if nothing was detected in front of the sensor OR something was detected but more than 3 timesteps ago...
    flagR = 0;                            // ...then, flagR = 0, which roughly means "nothing was detected in front of the right sensor".
  }

  rotation = -1;  // That value does not rotate the cube in Unity.
  
  if(abs(distLOLD-distLInt) < abs(distROLD-distRInt) && (distLInt < 20)){   // If the absolute value of the difference between the two last Left sensor distances are smaller than the absolute value of the difference between the two last Right sensor distances...
                                                                            // ...AND the last detected distance is smaller than 20cm...
    flagL = 1;                                                              // ...then, something is in front of the Left Sensor, so flagL = 1.
    timestepsL = 0;                                                         // timestepsL = 0 because something is CURRENTLY in front of the sensor. When it is no longer detected, timestepsL will increment.
    if(flagR == 1){                     // If something was also detected in front of the Right sensor in the last 3 timesteps...
      //Serial.print("\nMOVE LEFT\n");  // ...that means something moved from Right to Left! Gesture recognised successfully.
      rotation = 0;                     // That value rotates the cube in Unity to the Left.
    }
    flagR = 0;                          // Since a gesture to-the-left was recognised, we need to set flagR=0. Otherwise, if in the next timestep something is still detected in front of the left sensor, another (false) gesture to-the-left will be recognised.
  }
  if(abs(distLOLD-distLInt) > abs(distROLD-distRInt) && (distRInt < 20)){   // If the absolute value of the difference between the two last Left sensor distances are greater than the absolute value of the difference between the two last Right sensor distances...
                                                                            // ...AND the last detected distance is smaller than 20cm...
    flagR = 1;                                                              // ...then, something is in front of the Right Sensor, so flagR = 1.
    timestepsR = 0;                                                         // timestepsR = 0 because something is CURRENTLY in front of the sensor. When it is no longer detected, timestepsR will increment.
    if(flagL == 1){                     // If something was also detected in front of the Left sensor in the last 3 timesteps...
      //Serial.print("\nMOVE RIGHT\n"); // ...that means something moved from Left to Right! Gesture recognised successfully.
      rotation = 1;                     // That value rotates the cube in Unity to the Right.
    }
    flagL = 0;                          // Since a gesture to-the-right was recognised, we need to set flagL=0. Otherwise, if in the next timestep something is still detected in front of the right sensor, another (false) gesture to-the-right will be recognised.
  }

  Serial.write(rotation);            // Passing rotation value to Unity.
  Serial.flush();                    // Waits for the transmission of outgoing serial data to complete.
  
  delay(100);   // Delay between each time that either left or right sensor are "enabled".
}


//_________________________FUNCTIONS:_________________________

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
