//Using one Ultrasonic Sensor, moving camera in Unity forwards and backwards.

const int trigLLPin = 4;   // Trigger pin of Left Left Ultrasonic Ranging Module HC-SR04.
const int echoLLPin = 5;   // Echo pin of Left Left Ultrasonic Ranging Module HC-SR04.

const int flagLedPin = 2;
const int flashingLedPin = 3;

float durationLL;       // Time duration between emiting and receiving signal from the left left sensor.

float distanceLL;       // Distance calulated based on durationLL.

int distLLInt;          // Integer value of float distanceLL.


void setup() {
  Serial.begin(9600);
  pinMode(trigLLPin, OUTPUT);
  pinMode(echoLLPin, INPUT);

  pinMode(flagLedPin, OUTPUT);
  pinMode(flashingLedPin, OUTPUT);
}

void loop() {
  // Emiting signal from sensors, calculating distances:
  
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
 

  if(distLLInt > 2 && distLLInt < 32){
        digitalWrite(flagLedPin, HIGH);
  }

  //Serial.println(distLLInt);
  Serial.write(distLLInt);                // Send the value of timestepsAR which moves the cube to the right.
  Serial.flush();                           // Wait for the data to be sent successfully to Unity.
  delay(100);   // Delay between each time that either left or right sensor are "enabled".
  
  digitalWrite(flagLedPin, LOW);
}
