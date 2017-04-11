void setup(){
  Serial.begin(9600);
}

void loop(){
	if(Serial.readString() == "Hello From Pc"){
		Serial.println("Hello From Arduino");	
	}
	delay(100);
}
