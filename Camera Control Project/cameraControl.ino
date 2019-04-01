//_________________________Variable Declaration:_________________________
const int trigLLPin = 4;    // Trigger pin of Left Left Ultrasonic Ranging Module HC-SR04.
const int echoLLPin = 5;    // Echo pin of Left Left Ultrasonic Ranging Module HC-SR04.
const int trigLRPin = 7;    // Trigger pin of Left Right Ultrasonic Ranging Module HC-SR04.
const int echoLRPin = 8;    // Echo pin of Left Right Ultrasonic Ranging Module HC-SR04.

const int trigRLPin = 10;   // Trigger pin of Right Left Ultrasonic Ranging Module HC-SR04.
const int echoRLPin = 11;   // Echo pin of Right Left Ultrasonic Ranging Module HC-SR04.
const int trigRRPin = 12;   // Trigger pin of Right Right Ultrasonic Ranging Module HC-SR04.
const int echoRRPin = 13;   // Echo pin of Right Right Ultrasonic Ranging Module HC-SR04.

const int flagLedPin = 2;
const int flashingLedPin = 3;

int distLLInt;            // Integer value of float distanceLL.
int distLRInt;            // Integer value of float distanceLR.
int distRLInt;            // Integer value of float distanceRL.
int distRRInt;            // Integer value of float distanceRR.

int avgDistL = -1;        // Average distance value from the Left Sensors. ==-1 means there were not any distances from Left sensors in this timestep, so could not calculate average distance.
int avgDistR = -1;        // Average distance value from the Right Sensors. ==-1 means there were not any distances from Right sensors in this timestep, so could not calculate average distance.

int sensorSwitch = 0;     // 0== Switch Left Sensors ON, 1== Switch Right Sensors ON.

int totalAvgDistance = 0; // Value sent to Unity. Takes a value between [-32, +32]. ==0 When something's just in the middle between Left and Right sensors, ==-32 or ==32 when on the left or right end.


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
  if(sensorSwitch == 0){                                        // When sensorSwitch == 0, LEFT SENSORS emit and receive signal.
    distLLInt = distCalc(trigLLPin, echoLLPin, flashingLedPin); // Calling distCalc() for Left Left sensor.
    distLRInt = distCalc(trigLRPin, echoLRPin, flashingLedPin); // Calling distCalc() for Left Right sensor.
    sensorSwitch = 1;                                           // In the next loop, switch Right sensors On.
  }
  else{                                                         // When sensorSwitch == 0, RIGHT SENSORS emit and receive signal.
    distRLInt = distCalc(trigRLPin, echoRLPin, flashingLedPin); // Calling distCalc() for Right Left sensor.
    distRRInt = distCalc(trigRRPin, echoRRPin, flashingLedPin); // Calling distCalc() for Right Right sensor.
    sensorSwitch = 0;                                           // In the next loop, switch Left sensors On.
  }

  // Calculating average distances for left and right breadboard.
  avgDistL = avgDistCalc(distLLInt, distLRInt); // Calling avgDistCalc() to calcualte the average distance for the Left sensors.
  avgDistR = avgDistCalc(distRLInt, distRRInt); // Calling avgDistCalc() to calcualte the average distance for the Right sensors.

  /*// Printing all the distances while debugging.
  Serial.print("LL: ");
  Serial.print(distLLInt);
  Serial.print("  LR: ");
  Serial.print(distLRInt);
  Serial.print(" RL: ");
  Serial.print(distRLInt);
  Serial.print("  RR: ");
  Serial.print(distRRInt);
  
  Serial.print("\nLEFT: ");
  Serial.print(avgDistL);
  Serial.print("  RIGHT: ");
  Serial.print(avgDistR);
  Serial.print("\n\n");
  */

  // Calculating total average distance and sending it to Unity.
  totalAvgDistance = totalAvgDistCalc(avgDistL, avgDistR);  // Calling totalAvgDistCalc() to calculate the total average distance from all four sensors.
  
  //Serial.print("\nTotal Avg Distance: ");
  //Serial.print(totalAvgDistance);
  //Serial.print("\n");

  Serial.write(totalAvgDistance); // Send totalAvgDistance to Unity.
  Serial.flush();                 // Wait for the data to be sent successfully to Unity.
  delay(100);                     // Delay between each time that either left or right sensor are "enabled".
  
  digitalWrite(flagLedPin, LOW);
}


//_________________________FUNCTIONS:_________________________
int distCalc(int trigPin, int echoPin, int flashingLedPin){ // Gets the sensor's trigger and echo pin and flashingLed's pin, calculates and returns a distance.
  float duration;                     // Time duration between emiting and receiving signal from the sensor.
  float distance;                     // Distance calulated based on duration.
  int distInt;                        // Integer value of float distance.

  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);               // Delay, to let sensor switch trigPin to LOW.
  digitalWrite(flashingLedPin, HIGH);
  digitalWrite(trigPin, HIGH);        // Emiting signal.
  delayMicroseconds(10);              // Delay, to let sensor emit signal.
  digitalWrite(trigPin, LOW);
  digitalWrite(flashingLedPin, LOW);
  duration = pulseIn(echoPin, HIGH);  // Receiving signal and saving its duracion.
  distance = duration/58.2;           // Calculating distance in centimeters, based on the speed of sound.
  distInt = distance;                 // Converting float distance value to integer.
  return distInt;                     // Return the calculated distance.
}

int avgDistCalc(int distLInt, int distRInt){  // Gets two distance values and calculates and returns their average value.
  //int avgDist = 0;                      // Default value. Switch to this after debugging.
  int avgDist = -1;                       // For debugging reasons. It is converted to 0 before calculating totalAvgDistance.
  
  if(distLInt <= 30 || distRInt <= 30){   // If at least one of the Sensors of the Breadboard detects something...
    digitalWrite(flagLedPin, HIGH);
    if(distLInt <= 30 && distRInt <= 30){ // If both Sensors detect something at the same time...
      avgDist = (distLInt + distRInt)/2;  //... avgDist = average value of the two distances.
    }
    else if(distLInt <= 30){              // Else, if only L Sensor detects something...
      avgDist = distLInt;                 //... avgDist = distance calculated by L sensor.
    }
    else if(distRInt <= 30){              //  Else, if only R Sensor detects something...
      avgDist = distRInt;                 //... avgDist = distance calculated by R sensor.
    }
  }
  return avgDist;                         // Return the calculated average distance.
}

int totalAvgDistCalc(int avgDistL, int avgDistR){ // Gets the left and right average distances and calculates and returns the position of what is beeing detected by the sensors.
  int totalAvgDistance = 0;

  if(avgDistL == -1){                     //For debugging reasons. Remove it afterwards.
    avgDistL = 0;
  }
  if(avgDistR == -1){                     //For debugging reasons. Remove it afterwards.
    avgDistR = 0;
  }
  
  totalAvgDistance = avgDistL - avgDistR; // Position is equal to avgL - avgR. It takes values between in [-32,32]
  totalAvgDistance += 32;                 // I want it to take values in [0,64] because negative values can not be sent to Unity.
  return totalAvgDistance;                // Return the calculated total average distance from the middle of the two breadboards.
}
