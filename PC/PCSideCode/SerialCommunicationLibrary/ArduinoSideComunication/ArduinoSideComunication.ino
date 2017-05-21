#define StepPin 5
#define DirPin 4

int MotorsPins[3] = {6,7,8}; //Motor Pins(A4988 Pololu Enable pin)
int StepMotor;
int MaterialPullingDistances[3] = {1000,2000,3000}; //Distance from device to the 3D printer
int TCRTIFS[3] = {A0,A1,A2}; //Analog TCRTs's pin

void setup(){
  Serial.begin(9600);
  for(int i = 4; i <= 7; i++){
    pinMode(i,OUTPUT); 
  }
  
  for(int i = 0; i <= 2; i++){
    digitalWrite(MotorsPins[i], HIGH);
    pinMode(TCRTIFS[i], INPUT);
  }
}

int CheckTCRTIR(){

	int distance = analogRead(A0);

	//1000 ~= 3/4 cm
	if(distance < 1000){
		return 0;
	}
	return -1;
}

void NoFillamentFound(){
  Serial.println("No Fillament");
}

void PullInMaterial(){
//Придърпва точно определена дължина
	if(CheckTCRTIR()){
		RotateMotor(HIGH);
	}else{
    NoFillamentFound();
	}
}

void RotateMotor(int direction){
  //Serial.println(StepMotor);  
	digitalWrite(MotorsPins[StepMotor], LOW);
	digitalWrite(DirPin, direction); 
	
	for(int i = 0; i < MaterialPullingDistances[StepMotor]; i++) 
	{
		digitalWrite(StepPin, HIGH);
		delay(10);
		digitalWrite(StepPin, LOW);
	}
}

void PullOutMaterial(){
	if(CheckTCRTIR()){
		RotateMotor(LOW);
	}else{
    NoFillamentFound();
	}
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
	  case '4' : PullOutMaterial(); Serial.println("Ready with pulling out"); break;
  }

  delay(100);
} 
