/*  Example for Input-Output Lab 5
 * 
 *  Install the LSM6DS3 and the Madgwick libraries
 *  based on: https://itp.nyu.edu/physcomp/lessons/accelerometers-gyros-and-imus-the-basics/
*/

#include "Arduino_LSM6DS3.h"
#include "MadgwickAHRS.h"

// initialize a Madgwick filter:
Madgwick filter;

// Pinmode enum
enum pinModes {
  UNUSED_PIN = -1,
  DIGITAL_INPUT = 0,
  ANALOG_INPUT = 1,
  DIGITAL_OUTPUT = 2,
  ANALOG_OUTPUT = 3,
  SERVO = 4,
  DIGITAL_INPUT_PULLUP = INPUT_PULLUP
};

// values for orientation:
float yaw = 0.0;
float pitch = 0.0;
float roll = 0.0;
bool trigger = false;
bool ammo1 = false;
bool ammo2 = false;
bool switchAmmo = false;
bool special = false;

int triggerPin = 6;
int ammo1Pin = 8;
int ammo2Pin = 7;
int switchAmmoPin = 3;
int specialPin = 5;

void setup() {
  Serial.begin(57600);
  // attempt to start the IMU:
  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU");
    // stop here if you can't access the IMU:
    while (true);
  }

  pinMode(triggerPin, DIGITAL_INPUT);
  pinMode(ammo1Pin, DIGITAL_INPUT);
  pinMode(ammo2Pin, DIGITAL_INPUT);
  pinMode(switchAmmoPin, DIGITAL_INPUT);
  pinMode(specialPin, DIGITAL_INPUT);
  
  // start the filter to run at the sample rate of the IMU
  filter.begin(IMU.accelerationSampleRate());
}

void loop() {
  // values for acceleration and rotation:
  float xAcc, yAcc, zAcc;
  float xGyro, yGyro, zGyro;

  // check if the IMU is ready to read:
  if (IMU.accelerationAvailable() && IMU.gyroscopeAvailable()) {
    // read accelerometer &and gyrometer:
    IMU.readAcceleration(xAcc, yAcc, zAcc);
    IMU.readGyroscope(xGyro, yGyro, zGyro);

    // update the filter, which computes orientation:
    filter.updateIMU(xGyro, yGyro, zGyro, xAcc, yAcc, zAcc);

    // print the yaw (heading), pitch and roll
    yaw   = filter.getYaw();
    pitch = filter.getPitch();
    roll  = filter.getRoll();

    // get button states
    trigger = digitalRead(triggerPin);
    ammo1 = digitalRead(ammo1Pin);
    ammo2 = digitalRead(ammo2Pin);
    switchAmmo = digitalRead(switchAmmoPin);
    special = digitalRead(specialPin);
    
    //Serial.println("Orientation: ");
    Serial.print(yaw);
    Serial.print(",");
    Serial.print(pitch);
    Serial.print(",");
    Serial.print(roll);
    Serial.print(",");
    Serial.print(trigger ? 1:0);
    Serial.print(",");
    Serial.print(ammo1 ? 1:0);
    Serial.print(",");
    Serial.print(ammo2 ? 1:0);
    Serial.print(",");
    Serial.print(switchAmmo ? 1:0);
    Serial.print(",");
    Serial.println(special ? 1:0);
  }
}
