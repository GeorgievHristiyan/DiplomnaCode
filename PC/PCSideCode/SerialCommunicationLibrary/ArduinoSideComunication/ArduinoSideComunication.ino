#define StepPin 5
#define DirPin 4

int MotorsPins[3] = {6,7,8};
int StepMotor;
int MaterialPullingLengths[3] = {1000,2000,3000};
int TCRTIFS[3] = {17,18,19};


void setup(){
  Serial.begin(9600);
  for(int i = 4; i <= 7; i++){
    pinMode(i,OUTPUT); 
  }
  
  for(int i = 0; i <= 2; i++){
  digitalWrite(MotorsPins[i], HIGH);
  }
}

int CheckTCRTIR(){
	
	digitalWrite(TCRTIFS[StepMotor], HIGH);
	int SignalWithNoise = analogRead(A0);

	digitalWrite(TCRTIFS[StepMotor], LOW);
	int Noise = analogRead(A0);
	
	int Signal = SignalWithNoise - Noise;

	//Have to rework it!
	if(Signal > //Something){
		return -1;
	}
	return 0;
}

void PullInMaterial(){
//Придърпва точно определена дължина
	if(CheckTCRTIR()){
		RotateMotor(HIGH);
	}
}

void RotateMotor(int direction){
Serial.println(StepMotor);  
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
	if(CheckTCRTIR()){
		RotateMotor(LOW);
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
