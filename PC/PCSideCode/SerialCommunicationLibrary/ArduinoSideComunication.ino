#define EnablePin 6
#define StepPin 5
#define DirPin 4

void setup(){
  Serial.begin(9600);
  for(int i = 4; i <= 6; i++){
    pinMode(i,OUTPUT); 
  }
  
  digitalWrite(EnablePin,LOW); 
}

void PullMaterial(){
//Придърпва точно определена дължина
  digitalWrite(EnablePin, LOW);
  digitalWrite(DirPin, HIGH); 
  for(int i = 0; i < 200; i++) 
  {
    digitalWrite(StepPin, HIGH);
    delay(10);
    digitalWrite(StepPin, LOW);
  }
}


void loop(){
  
  switch(Serial.readString()[0]){
    case '0' : Serial.println("Hello From Arduino"); break;
    case '1' :
    case '2' :
    case '3' :
    case '4' : PullMaterial(); break;
  }

  

  delay(100);
}
