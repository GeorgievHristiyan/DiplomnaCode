int signalWithNoise, noise, denoiseSignal;

void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
  for(int i = A0; i <= A2; i++){
    pinMode(i, INPUT);
  }

  pinMode(A2, INPUT);
  pinMode(A0, INPUT);
}
 
// the loop routine runs over and over again forever:
void loop() {

  delay(500);
  Serial.print((int) analogRead(A0));
  Serial.print( "  ");
    
  Serial.print((int)analogRead(A1));
  Serial.print("  ");
    
  Serial.print((int)analogRead(A2));
  Serial.println("  ");
  
  delay(1);        // delay in between reads for stability
}
