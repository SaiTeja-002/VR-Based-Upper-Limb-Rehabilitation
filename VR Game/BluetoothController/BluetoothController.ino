//Includes
#include "Wire.h"
#include <MPU6050_light.h>
#include <SoftwareSerial.h> 

//Macros
#define TX 12
#define RX 13

//Objects
SoftwareSerial BluetoothDevice(TX,RX);
MPU6050 mpu(Wire);

//Global Variables
unsigned long timer = 0;
 
void setup() 
{
  //Initializing Communcation Protocols
  Serial.begin(9600);
  BluetoothDevice.begin(9600);
  Wire.begin();

  //Checking MPU6050 Status
  byte status = mpu.begin();
  Serial.print(F("MPU6050 Connection Status: "));
  Serial.println(status);
  while(status!=0){ }
 
  //Calibrating MPU
  Serial.println(F("Calibrating MPU6050"));
  delay(1000);
  mpu.calcOffsets();
  Serial.println("Calibration Complete");
}

void loop() 
{
  //Updating MPUData
  mpu.update();

//  Pumps Data Every 10ms
  if((millis()-timer)>10){

    //Collecting Data
    String gyroData = String(mpu.getAngleX()) + " ";
    gyroData += String(mpu.getAngleY()) + " ";
    gyroData += String(mpu.getAngleZ()) + " ";

    //Sending Formatted Data
    BluetoothDevice.println(gyroData);
    Serial.println(gyroData);

    //Timer Update
    timer = millis();  
  }
}
