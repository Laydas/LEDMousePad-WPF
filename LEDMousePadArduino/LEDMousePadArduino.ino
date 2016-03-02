String input;
String process = "";

void setup() {
  // put your setup code here, to run once:
  pinMode(13, OUTPUT);
  Serial.begin(9600);
}

void loop() {
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
