//_________________________Variable Definition:_________________________
const int trigPin = 7;   // Trigger pin of Ultrasonic Ranging Module HC-SR04
const int echoPin = 8;   // Echo pin of Ultrasonic Ranging Module HC-SR04

const int standbyLedPin = 2;
const int flashingLedPin = 3;

const int led1Pin = 13;
const int led2Pin = 12;
const int led3Pin = 11;
const int led4Pin = 10;
const int led5Pin = 9;

long duration;
long distance;


//_________________________SETUP():_________________________
void setup() {
  Serial.begin(9600);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);

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
  //switch off everything
  digitalWrite(trigPin, LOW);
  
  digitalWrite(standbyLedPin, LOW);
  digitalWrite(flashingLedPin, LOW);

  digitalWrite(led1Pin, LOW);
  digitalWrite(led2Pin, LOW);
  digitalWrite(led3Pin, LOW);
  digitalWrite(led4Pin, LOW);
  digitalWrite(led5Pin, LOW);

  delayMicroseconds(2);                // To let sensor switch trigPin to LOW.

  //emit signal
  digitalWrite(flashingLedPin, HIGH);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);               // To let sensor emit signal.
  digitalWrite(trigPin, LOW);
  digitalWrite(flashingLedPin, LOW);
  
  duration = pulseIn(echoPin, HIGH);   // Recieving signal and calculating duracion.
  distance = duration/58.2;            // This gives distance in centimeters based on the speed of sound.

  Serial.println(distance);            // Printing distance in IDE's Serial Monitor

  ledSwitch(distance);                 //Switching LEDs on, according to distance:
  
  delay(500);
}


//_________________________FUNCTIONS:_________________________

void ledSwitch(int distance){
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
