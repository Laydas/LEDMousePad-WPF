String input;
String process = "";

void setup() {
  // put your setup code here, to run once:
  pinMode(13, OUTPUT);
  Serial.begin(9600);
  DDRD = B11111111; // set PORTD (digital 7-0) to outputs
}

byte controlPins1[] = {B00000000,
  B10000000,
  B01000000,
  B11000000,
  B00100000,
  B10100000,
  B01100000,
  B11100000,
  B00010000,
  B10010000,
  B01010000,
  B11010000,
  B00110000,
  B10110000,
  B01110000,
  B11110000 };

void setPin(int outputPin)
{
  PORTD = controlPins1[outputPin];
}
  
void loop() {

for(int i = 0; i < 16; i++)
{
  setPin(i);
  delay(250);
} 
  // Get the commands from Windows here
  if(Serial.available()){
    while(Serial.available() > 0){
      char c = Serial.read();
      process += (c);
    }
  }
  if(process[process.length() -1] == '~')
  {
    String command = process.substring(0,process.length() -1);
    if(command == "HEY")
    {
      Serial.println("84652345shae");
    }
    if(command == "1")
    {
      digitalWrite(13, HIGH);  
    }
    process = "";
  }
  if (process == "0")
  {
    digitalWrite(13, LOW);
    process = "";
  }
}
