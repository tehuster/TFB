int infoReceived = 0;

int motorMode[8] = {0, 0, 0, 0, 0, 0, 0, 0};
int motorSpeed[8] = {0, 0, 0, 0, 0, 0, 0, 0};

typedef struct pwmPins {
  int pin;
  int pwmValue;
  bool pinState;
  int pwmTickCount;
} pwmPin;

// variables for software PWM
unsigned long currentMicros = micros();
unsigned long previousMicros = 0;
// this is the frequency of the sw PWM
// frequency = 1/(2 * microInterval)
unsigned long microInterval = 10;
 
 byte pwmMax = 100;

 int pinCount = 8;
 int pins[8] = {4,5,6,7,8,9,10,11};

 pwmPins myPWMpins[8];

void setupPWMpins() {
  for (int index=0; index < pinCount; index++) {
    myPWMpins[index].pin = pins[index];
  
    // mix it up a little bit
    // changes the starting pwmValue for odd and even
    if (index % 2)
      myPWMpins[index].pwmValue = 25;
    else
      myPWMpins[index].pwmValue = 75;
  
    myPWMpins[index].pinState = HIGH;
    myPWMpins[index].pwmTickCount = 0;
  
    // unlike analogWrite(), this is necessary
    pinMode(pins[index], OUTPUT);
  }
}

void handlePWM() {
  currentMicros = micros();
  // check to see if we need to increment our PWM counters yet
    if (currentMicros - previousMicros >= microInterval) {
    // Increment each pin's counter
    for (int index=0; index < pinCount; index++) {
    // each pin has its own tickCounter
      myPWMpins[index].pwmTickCount++;
 
    // determine if we're counting HIGH or LOW time
      if (myPWMpins[index].pinState == HIGH) {
        // see if we hit the desired HIGH percentage
        // not as precise as 255 or 1024, but easier to do math
        if (myPWMpins[index].pwmTickCount >= myPWMpins[index].pwmValue) {
          myPWMpins[index].pinState = LOW;
        }
      } else {
        // if it isn't HIGH, it is LOW
        if (myPWMpins[index].pwmTickCount >= pwmMax) {
          myPWMpins[index].pinState = HIGH;
          myPWMpins[index].pwmTickCount = 0;
        }
      }
      // could probably use some bitwise optimizatiHIGH here, digitalWrite()
      // really slows things down after 10 pins.
      digitalWrite(myPWMpins[index].pin, myPWMpins[index].pinState);
    }
    // reset the micros() tick counter.
    digitalWrite(13, !digitalRead(13));
    previousMicros = currentMicros;
  }
}

void setup() {
  Serial.begin(57600);
  setupPWMpins();
}

void loop() {
   if (Serial.available() == 17)
  { // If data comes in from XBee, send it out to serial monitor
    String serialBuffer;
    serialBuffer = Serial.readStringUntil(';');
 //   Serial.println(serialBuffer);
    for (int i = 0; i < 8; i ++) {
      motorSpeed[i] = serialBuffer.substring(i * 2 + 0, i * 2 + 2).toInt();
//      Serial.print(i*2);
//      Serial.print("  ");
//      Serial.print(i * 2 + 2);
//      Serial.print("  ");
//      Serial.println(motorSpeed[i]);
      myPWMpins[i].pwmValue = motorSpeed[i];      
    }  
 //   Serial.flush();
  }else{
 //   Serial.flush();
  }
  handlePWM();
}
