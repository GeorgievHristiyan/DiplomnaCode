void setup(){

}

void loop(){
	if(Serial.readString() == "Hello From Pc"){
		Serial.println("Hello From Arduino");	
	}
	delay(100);
}