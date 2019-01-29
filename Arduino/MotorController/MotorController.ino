int infoReceived = 0;

int motorMode[8] = {0, 0, 0, 0, 0, 0, 0, 0};
int motorSpeed[8] = {0, 0, 0, 0, 0, 0, 0, 0};

void setup()
{
  Serial.begin(57600);
  pinMode(13, OUTPUT);
}

void loop()
{

  if (Serial.available())
  { // If data comes in from XBee, send it out to serial monitor
    digitalWrite(13, HIGH);
//    String serialBuffer;
//    serialBuffer = XBee.readStringUntil(';');
//    for (int i = 0; i < 8; i ++) {
//      motorMode[i] = serialBuffer.substring(i * 6, i * 6 + 2).toInt();
//      motorSpeed[i] = serialBuffer.substring(i * 6 + 2, i * 6 + 5).toInt();
//    }  
//    for (int i = 0; i < 8; i ++) {
//      int mSpeed = map(motorSpeed[i], 0, 100, 0, 750);
//      pwm.setPWM(i, 0, mSpeed);
//    }
  }else{
    digitalWrite(13, LOW);
  }


}
