//_________________________Variable Declaration:_________________________
const int trigLLPin = 4;   // Trigger pin of Left Left Ultrasonic Ranging Module HC-SR04.
const int echoLLPin = 5;   // Echo pin of Left Left Ultrasonic Ranging Module HC-SR04.
const int trigLRPin = 7;   // Trigger pin of Left Right Ultrasonic Ranging Module HC-SR04.
const int echoLRPin = 8;   // Echo pin of Left Right Ultrasonic Ranging Module HC-SR04.

const int trigRLPin = 10;   // Trigger pin of Right Left Ultrasonic Ranging Module HC-SR04.
const int echoRLPin = 11;   // Echo pin of Right Left Ultrasonic Ranging Module HC-SR04.
const int trigRRPin = 12;   // Trigger pin of Right Right Ultrasonic Ranging Module HC-SR04.
const int echoRRPin = 13;   // Echo pin of Right Right Ultrasonic Ranging Module HC-SR04.

const int flagLedPin = 2;
const int flashingLedPin = 3;

float durationLL;       // Time duration between emiting and receiving signal from the left left sensor.
float durationLR;       // Time duration between emiting and receiving signal from the left right sensor.
float distanceLL;       // Distance calulated based on durationLL.
float distanceLR;       // Distance calulated based on durationLR.
int distLLInt;          // Integer value of float distanceLL.
int distLRInt;          // Integer value of float distanceLR.

float durationRL;       // Time duration between emiting and receiving signal from the Right left sensor.
float durationRR;       // Time duration between emiting and receiving signal from the Right right sensor.
float distanceRL;       // Distance calulated based on durationRL.
float distanceRR;       // Distance calulated based on durationRR.
int distRLInt;          // Integer value of float distanceRL.
int distRRInt;          // Integer value of float distanceRR.

int avgDistL = -1;      // Average distance value from the Left Sensors. ==-1 means there were not any distances from Left sensors in this timestep, so could not calculate average distance.
int avgDistR = -1;      // Average distance value from the Right Sensors. ==-1 means there were not any distances from Right sensors in this timestep, so could not calculate average distance.

int sensorSwitch = 0;  // 0== Switch Left Sensors ON, 1== Switch Right Sensors ON.

//_________________________SETUP():_________________________
void setup() {
  Serial.begin(9600);
  pinMode(trigLLPin, OUTPUT);
  pinMode(echoLLPin, INPUT);

  pinMode(flagLedPin, OUTPUT);
  pinMode(flashingLedPin, OUTPUT);
}

//_________________________LOOP():_________________________
void loop() {
  // Calculating distances:
  if(sensorSwitch == 0){                    // When sensorSwitch == 0, LEFT SENSORS emit and receive signal.
    distLLInt = distCalc(trigLLPin, echoLLPin, flashingLedPin);

    distLRInt = distCalc(trigLRPin, echoLRPin, flashingLedPin);
    sensorSwitch = 1;                       // In the next loop, switch Right sensors On.
  }
  else{
    //distRLInt = distCalc(trigRLPin, echoRLPin, flashingLedPin);
    //distRRInt = distCalc(trigRRPin, echoRRPin, flashingLedPin);
    sensorSwitch = 0;                       // In the next loop, switch Left sensors On.
  }

  // Calculating average distances for left and right breadboard.
  avgDistL = -1;
  avgDistR = -1;
  
  if(distLLInt <= 30 || distLRInt <= 30 || distRLInt <= 30 || distRRInt <= 30){ // If at least one of the Sensors of the Left or Right Breadboard detects something...
    //LEFT BREADBOARD
    avgDistL = avgDistCalc(distLLInt, distLRInt);
  
    //RIGHT BREADBOARD
    avgDistR = avgDistCalc(distRLInt, distRRInt);

  

  //Serial.println(distLLInt);
  Serial.write(distLLInt);                // Send the value of timestepsAR which moves the cube to the right.
  Serial.flush();                           // Wait for the data to be sent successfully to Unity.
  delay(100);   // Delay between each time that either left or right sensor are "enabled".
  
  digitalWrite(flagLedPin, LOW);
}



//_________________________FUNCTIONS:_________________________
int distCalc(int trigPin, int echoPin, int flashingLedPin){
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TODO: DELETE DISTANCES AND DURATIONS FROM TOP!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
  float duration;
  float distance;
  int distInt;

  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);                   // Delay, to let sensor switch trigPin to LOW.
  digitalWrite(flashingLedPin, HIGH);
  digitalWrite(trigPin, HIGH);          // Emiting signal.
  delayMicroseconds(10);                  // Delay, to let sensor emit signal.
  digitalWrite(trigPin, LOW);
  digitalWrite(flashingLedPin, LOW);
  duration = pulseIn(echoPin, HIGH);  // Receiving signal and saving its duracion.
  distance = duration/58.2;           // Calculating distance in centimeters, based on the speed of sound.
  distInt = distance;                 // Converting float distance value to integer.
  return distInt;
}

int avgDistCalc(int distLInt, int distRInt){
  //!!!!!!!!!!!!!!!!!!!!!SAME HERE MAYBE!!!!!!!!!!!!!!!!!!!
  int avgDist = -1;

  if(distLInt <= 30 || distRInt <= 30){     // If at least one of the Sensors of the Breadboard detects something...
    digitalWrite(flagLedPin, HIGH);
    if(distLInt <= 30 && distRInt <= 30){   // If both Sensors detect something at the same time...
      avgDist = (distLInt + distRInt)/2;   //... avgDist = average value of the two distances.
    }
    else if(distLInt <= 30){                 // Else, if only L Sensor detects something...
      avgDist = distLInt;                   //... avgDist = distance calculated by L sensor.
    }
    else if(distRInt <= 30){                 //  Else, if only R Sensor detects something...
      avgDist = distRInt;                   //... avgDist = distance calculated by R sensor.
    }
    digitalWrite(flagLedPin, LOW);
  }
  return avgDist;
}
