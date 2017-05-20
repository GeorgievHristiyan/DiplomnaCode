int signalWithNoise, noise, denoiseSignal;

void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
  pinMode(6, OUTPUT);
}
 
// the loop routine runs over and over again forever:
void loop() {

  digitalWrite(6, HIGH);

  signalWithNoise = analogRead(A0);
  Serial.println("Signal + Noise");
  Serial.println(signalWithNoise);
  
  digitalWrite(6, LOW);
  noise = analogRead(A0);
  Serial.println("Noise");
  Serial.println(noise);
  
  denoiseSignal = signalWithNoise - noise;
  // print out the value you read:

  Serial.println("Signal");
  Serial.println(denoiseSignal);
  delay(1);        // delay in between reads for stability
}
