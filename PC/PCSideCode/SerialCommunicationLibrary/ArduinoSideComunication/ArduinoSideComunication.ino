#define StepPin 2
#define DirPin 3

int MotorsPins[3] = {4, 5, 6}; //Motor Pins(A4988 Pololu Enable pin)  //ENABLE
int StepMotor;
int TCRTSDiapasone[3] = {200, 130, 105};
int MaterialPullingDistances[3] = {7100, 6800, 7000}; //Distance from device to the 3D printer
int TCRTIFS[3] = {A0, A1, A2}; //Analog TCRTs's pin

void setup(){
  Serial.begin(9600);
 
  for(int i = 2; i <= 7; i++){
    pinMode(i,OUTPUT); 
  }
  
  DisableAllMotors();
  
  InitStepPins();

  InitTCRTS();
}

void InitStepPins(){
  for(int i = 0; i <= 2; i++){
    digitalWrite(MotorsPins[i], HIGH);
  }
}

void InitTCRTS(){
  for(int i = 0; i <= 2; i++){
    pinMode(TCRTIFS[i], INPUT);
  }
}

int CheckTCRTIR(){

	int distance = analogRead(StepMotor);
  //Serial.println(distance);
  if(StepMotor == 0){
    if(distance > 200){
      return 1;
    }else{
      return 0;
    }
  }else{
    if(distance < TCRTSDiapasone[StepMotor]){
      return 1;
    }else{
      return 0;
    }
  }
}

void FillamentNotFound(){
  Serial.println("No Fillament");
}

void PullInMaterial(){
  //Придърпва точно определена дължина

	if(CheckTCRTIR()){
    RotateMotor(HIGH);    
	}else{
    FillamentNotFound();
	}
  Serial.println("Ready for printing"); 
}

void RotateMotor(int direction){
  //Serial.println(StepMotor);  
	digitalWrite(MotorsPins[StepMotor], LOW);
	digitalWrite(DirPin, direction); 
	
	for(int i = 0; i < MaterialPullingDistances[StepMotor]; i++) 
	{
		digitalWrite(StepPin, HIGH);
		delay(1);
		digitalWrite(StepPin, LOW);
    delay(1);
	}
 
 digitalWrite(MotorsPins[StepMotor], HIGH);
}

void PullOutMaterial(){
	
	if(CheckTCRTIR()){
	  RotateMotor(LOW);	
	}else{
    FillamentNotFound();
	}
  Serial.println("Ready with pulling out"); 
}

void DisableAllMotors(){
  for(int i = 0; i <= 2; i++){
    digitalWrite(MotorsPins[i] , HIGH);
  }
}

void loop(){

  switch(Serial.readString()[0]){
    case '0' : Serial.println("Hello From Arduino"); break;
    case '1' : StepMotor = 0; PullInMaterial(); break;
    case '2' : StepMotor = 1; PullInMaterial(); break;
    case '3' : StepMotor = 2; PullInMaterial(); break;
	  case '4' : PullOutMaterial(); break;
  }

  delay(100);
} 
