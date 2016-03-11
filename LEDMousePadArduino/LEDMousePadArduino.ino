const int redPin = 9;
const int greenPin = 5;
const int bluePin = 11;
int cLED[] = {2, 3, 4, 6, 7, 8, 10, 12, A0, A1, A2, A3, A4, A5};
byte color[16][4] = {
  {0, 255, 255, 0},
  {0, 195, 255, 0},
  {0, 125, 255, 0},
  {0, 65, 255, 0},
  {0, 0, 255, 1}, 
  {65, 0, 255, 1},
  {125, 0, 255, 1},
  {195, 0, 255, 1},
  {255, 0, 255, 2},
  {255, 0, 195, 2},
  {255, 0, 125, 2},
  {255, 0, 65, 2},
  {255, 0, 0, 3},
  {255, 65, 0, 3}};
  
int interval = 10;
unsigned long previousMillis = 0;
bool breath = false;
bool up = false;
byte rV, gV, bV;
int currentC = 0;
bool rainbow = false;
bool swirl = false;
int inc = 5;
String process = "";
int redMax, greenMax, blueMax, breathCount;

void SerialInput() {
  while(Serial.available() > 0){
    char c = Serial.read();
    process += (c);
  }

  if(process[process.length() -1] == '~')
  {
    String command = process.substring(0,process.length() -1);
    process = "";
    if(command == "HEY")
    {
      Serial.println("84652345shae");
    }
    if(command.substring(0,3) == "00S"){
      int rVint, gVint, bVint;
      rVint = command.substring(3,6).toInt();
      gVint = command.substring(6,9).toInt();
      bVint = command.substring(9,12).toInt();
      interval = command.substring(12,15).toInt();
      rV = rVint;
      gV = gVint;
      bV = bVint;
      beginSolid();
    }
    
    if(command.substring(0,3) == "0SB"){
      int rVint, gVint, bVint;
      rVint = command.substring(3,6).toInt();
      gVint = command.substring(6,9).toInt();
      bVint = command.substring(9,12).toInt();
      interval = command.substring(12,15).toInt();
      redMax = rVint;
      greenMax = gVint;
      blueMax = bVint;
      beginBreath();
    }
    
    if(command.substring(0,3) == "0SR"){
      interval = 50;
      beginSolidRainbow();
    }
    
    if(command.substring(0,3) == "0RR"){
      int rVint, gVint, bVint;
      rVint = command.substring(3,6).toInt();
      gVint = command.substring(6,9).toInt();
      bVint = command.substring(9,12).toInt();
      interval = command.substring(12,15).toInt();
      rV = rVint;
      gV = gVint;
      bV = bVint;
      beginSwirl();
    }
  }
  process.trim();
}

void beginSolid(){
  breath = false;
  rainbow = false;
  swirl = false;
  for(int i = 0; i < 14; i++){
    color[i][0] = rV;
    color[i][1] = gV;
    color[i][2] = bV;
  }
}

void beginSolidRainbow(){
  breath = false;
  rainbow = true;
  swirl = false;
  interval = 50;
  rV = 0;
  gV = 255;
  bV = 255;
  for(int i = 0; i < 14; i++){
    color[i][0] = 0;
    color[i][1] = 255;
    color[i][2] = 255;
  }
}

void beginBreath() {
  breath = true;
  up = true;
  rainbow = false;
  swirl = false;
  interval = 50;
  breathCount = 0;
}

void beginSwirl(){
  interval = 10;
  color[0][4] = 0, 255, 255, 0;
  color[1][4] = 0, 195, 255, 0;
  color[2][4] = 0, 125, 255, 0,
  color[3][4] = 0, 65, 255, 0,
  color[4][4] = 0, 0, 255, 1, 
  color[5][4] = 65, 0, 255, 1,
  color[6][4] = 125, 0, 255, 1,
  color[7][4] = 195, 0, 255, 1,
  color[8][4] = 255, 0, 255, 2,
  color[9][4] = 255, 0, 195, 2,
  color[10][4] = 255, 0, 125, 2,
  color[11][4] = 255, 0, 65, 2,
  color[12][4] = 255, 0, 0, 3,
  color[13][4] = 255, 65, 0, 3;
  swirl = true;
  rainbow = false;
  breath = false;
}

void SolidBreath() {
  
  if(up == true){
    breathCount++;
    if(breathCount == 100){
      up = false;
    }
  }
  else{
    breathCount--;
    if(breathCount == 0){
      up = true;
    }
  }
  
  rV = (((redMax / 100) * breathCount) -255) * -1;
  gV = (((greenMax / 100) * breathCount) - 255) * -1;
  bV = (((blueMax / 100) * breathCount) -255) * -1;
  //Serial.println(rV);
  for(int i = 0; i < 14; i++){
    color[i][0] = rV;
    color[i][1] = gV;
    color[i][2] = bV;
  }
}

void Swirl() {
  for(int i = 0; i< 14; i++){
    switch(color[i][3]){
      // red -> yellow
      case 0:
        color[i][1] = color[i][1] - inc;
        if(color[i][1] <= 0){color[i][1] = 0; color[i][3] = 1;}
      break;
      // yellow -> green
      case 1:
        color[i][0] = color[i][0] + inc;
        if(color[i][0] >= 255){color[i][0] = 255; color[i][3] = 2;}
      break;
      // green -> cyan
      case 2:
        color[i][2] = color[i][2] - inc;
        if(color[i][2] <= 0){color[i][2] = 0; color[i][3] = 3;}
      break;
      // cyan -> blue
      case 3:
        color[i][1] = color[i][1] + inc;
        if(color[i][1] >= 255){color[i][1] = 255; color[i][3] = 4;}
      break;
      // blue -> purple
      case 4:
        color[i][0] = color[i][0] - inc;
        if(color[i][0] <= 0){color[i][0] = 0; color[i][3] = 5;}
      break;
      // purple -> red
      case 5:
        color[i][2] = color[i][2] + inc;
        if(color[i][2] >= 255){color[i][2] = 255; color[i][3] = 0;}
      break;
    }
  }
}

void SolidRainbow(){
  switch (currentC){
    // Brighten Green
    case 0:
      gV = gV - 5;
      if(gV <= 0){gV = 0; currentC = 1;}
    break;
    // Dim Red
    case 1:
      rV = rV + 5;
      if(rV >= 255){rV = 255; currentC = 2;}
    break;
    // Brighten Blue
    case 2:
      bV = bV - 5;
      if(bV <= 0){bV = 0; currentC = 3;}
    break;
    // Dim Green
    case 3:
      gV = gV + 5;
      if(gV >= 255){gV = 255; currentC = 4;}
    break;
    // Brighten Red
    case 4:
      rV = rV - 5;
      if(rV <= 0){rV = 0; currentC = 5;}
    break;
    // Dim Blue
    case 5:
      bV = bV + 5;
      if(bV >= 255){bV = 255; currentC = 0;}
    break;
  }
  for(int i = 0; i < 14; i++){
    color[i][0] = rV;
    color[i][1] = gV;
    color[i][2] = bV;
  }
}
void setup() {
  Serial.begin(9600);
  // put your setup code here, to run once:
  pinMode(redPin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  for(int i = 0; i < 14; i++){
    pinMode(cLED[i], OUTPUT);
  }
  digitalWrite(cLED[0], HIGH);
  digitalWrite(cLED[1], LOW);
}


int led = 0;
int pled = 1;

void loop() {

if(Serial.available()){
  SerialInput();
}
  unsigned long currentMillis = millis();
  if(currentMillis - previousMillis >= interval){
    
    previousMillis = currentMillis;
    if(breath == true) {
      SolidBreath();
    }

    if(rainbow == true){
      SolidRainbow();
    }

    if(swirl == true){
      Swirl();
    }
  }
  analogWrite(redPin, color[led][0]);
  analogWrite(greenPin, color[led][1]);
  analogWrite(bluePin, color[led][2]);
  
  digitalWrite(cLED[led], HIGH);
  digitalWrite(cLED[pled], LOW);
  if(led == 0){delayMicroseconds(10);}

  delayMicroseconds(20);

  pled = led;
  led = (led + 1) % 14;
  //delayMicroseconds(10);
}
