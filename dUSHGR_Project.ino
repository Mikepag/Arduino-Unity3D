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

int distLOLD = 0;
int distROLD = 0;
int flagL = 0;
int flagR = 0;
int timestepsL = 0;
int timestepsR = 0;

int sensorSwitch = 0; // 0 == Left Sensor, 1 == Right Sensor.


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
  
    Serial.print("L ");
    Serial.print(distLInt);          // Printing distance on IDE's Serial Monitor. (COMMENT THIS LINE OUT WHEN SENDING DATA TO UNITY).
    //Serial.write(distLInt);              // Passing distance value to Unity.
    //Serial.flush();                      // Waits for the transmission of outgoing serial data to complete.
  
    ledSwitchON(distanceL);              // Switching leds ON, according to distance.
    sensorSwitch = 1;
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
  

    Serial.print("    R ");
    Serial.println(distRInt);          // Printing distance on IDE's Serial Monitor. (COMMENT THIS LINE OUT WHEN SENDING DATA TO UNITY).
    //Serial.write(distRInt);              // Passing distance value to Unity.
    //Serial.flush();                      // Waits for the transmission of outgoing serial data to complete.
  
    ledSwitchON(distanceR);              // Switching leds ON, according to distance.
    sensorSwitch = 0;
  }

  if(flagL == 1 && timestepsL <3){
    timestepsL++;
  }
  else{
    flagL = 0;
  }

  if(flagR == 1 && timestepsR <3){
    timestepsR++;
  }
  else{
    flagR = 0;
  }
  
  
  if(abs(distLOLD-distLInt) < abs(distROLD-distRInt) && (distLInt < 20)){
    //something is in front of L.
    flagL = 1;
    timestepsL = 0;
    if(flagR == 1){
      Serial.print("\nMOVE LEFT\n");
    }
    flagR = 0;
  }
  if(abs(distLOLD-distLInt) > abs(distROLD-distRInt) && (distRInt < 20)){
    //something is in front of R.
    flagR = 1;
    timestepsR = 0;
    if(flagL == 1){
      Serial.print("\nMOVE RIGHT\n");
    }
    flagL = 0;
  }

  //////////////////////if(flagL)
    
  
  delay(100);
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
