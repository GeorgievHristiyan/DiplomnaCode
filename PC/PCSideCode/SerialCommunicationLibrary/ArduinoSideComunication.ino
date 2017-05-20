#define StepPin 5
#define DirPin 4

int MotorsPins[3] = {6,7,8};
int MaterialPullingLengths = [];
int MaterialLenghtIndex = 0;

void setup(){
  Serial.begin(9600);
  for(int i = 4; i <= 7; i++){
    pinMode(i,OUTPUT); 
  }
  
  for(int i = 0; i <= 2; i++){
  digitalWrite(MotorsPins[i], HIGH);
  }
}

void PullInMaterial(){
//Придърпва точно определена дължина

	RotateMotor(HIGH);
}

void RotateMotor(int direction){

	digitalWrite(MotorsPins[StepMotor], LOW);
	digitalWrite(DirPin, direction); 
	
	for(int i = 0; i < MaterialPullingLengths[StepMotor]; i++) 
	{
		digitalWrite(StepPin, HIGH);
		delay(10);
		digitalWrite(StepPin, LOW);
	}
}

void PullOutMaterial(){

	RotateMotor(LOW);
}

void DisableAllMotors(){
  for(int i = 0; i <= 2; i++){
    digitalWrite(MotorsPins[i] , HIGH);
  }
}


void loop(){
  DisableAllMotors();

  switch(Serial.readString()[0]){
    case '0' : Serial.println("Hello From Arduino"); break;
    case '1' : StepMotor = 0; PullInMaterial(); Serial.println("Ready for printing"); break;
    case '2' : StepMotor = 1; PullInMaterial(); Serial.println("Ready for printing"); break;
    case '3' : StepMotor = 2; PullInMaterial(); Serial.println("Ready for printing"); break;
	case '4' : PullOutMaterial();
  }

  delay(100);
} 
