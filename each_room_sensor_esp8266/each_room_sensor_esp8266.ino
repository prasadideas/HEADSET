#include <ESP8266WiFi.h>
#include <PubSubClient.h>

// --- WiFi and MQTT settings ---
const char* ssid = "TP-Link_AA04";
const char* password = "12125951";
const char* mqtt_server = "192.168.0.2";
int roomNo = 1; //change this based on scary number

// --- GPIO configuration ---
#define SWITCH1 D1
#define LED_PIN LED_BUILTIN
#define BATTERY_PIN A0

// --- Globals ---
WiFiClient espClient;
PubSubClient client(espClient);
unsigned long lastStatus = 0;

// --- Function declarations ---
void setup_wifi();
void reconnect();
void callback(char* topic, byte* payload, unsigned int length);
void blinkLED(int times);
float readBatteryVoltage();

void setup() {
  Serial.begin(115200);
  Serial.println();
  Serial.println("Starting ESP8266 Music Node...");

  pinMode(SWITCH1, INPUT_PULLUP);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, HIGH);  // LED off (active low)

  setup_wifi();
  client.setServer(mqtt_server, 1883);
  client.setCallback(callback);
}

void loop() {
  if (!client.connected()) {
    reconnect();
  }
  client.loop();


  if(digitalRead(SWITCH1) == LOW)  {

    char topic[32];
    sprintf(topic, "status/eachgate");

    String payload = String(roomNo, 2);

    client.publish(topic, payload.c_str());
    Serial.print("[PUBLISH] ");
    Serial.print(topic);
    Serial.print(" -> ");
    Serial.println(payload);
    while(digitalRead(SWITCH1) == LOW);
  }
}

// --- WiFi setup ---
void setup_wifi() {
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println();
  Serial.print("WiFi connected. IP: ");
  Serial.println(WiFi.localIP());
  Serial.print("MAC: ");
  Serial.println(WiFi.macAddress());
}

// --- MQTT reconnect ---
void reconnect() {
  while (!client.connected()) {
    Serial.print("Attempting MQTT connection...");
    String clientId = "ESP8266-" + WiFi.macAddress();
    if (client.connect(clientId.c_str())) {
      Serial.println("connected");
      // Subscribe based on group switches
      client.subscribe("music/group1");
      client.subscribe("music/group2");
      client.subscribe("music/group3");
      Serial.println("Subscribed to music/group1,2,3");
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      delay(5000);
    }
  }
}

// --- MQTT message handler ---
void callback(char* topic, byte* payload, unsigned int length) {
  String msg;
  for (unsigned int i = 0; i < length; i++) msg += (char)payload[i];
  msg.trim();

  Serial.print("[RECV] ");
  Serial.print(topic);
  Serial.print(" -> ");
  Serial.println(msg);

  // Check if topic matches active group and message is "play"
 
}

// --- Blink LED function ---
void blinkLED(int times) {
  for (int i = 0; i < times; i++) {
    digitalWrite(LED_PIN, LOW);  // LED on
    delay(200);
    digitalWrite(LED_PIN, HIGH); // LED off
    delay(200);
  }
}

// --- Read battery voltage ---
float readBatteryVoltage() {
  int raw = analogRead(BATTERY_PIN);
  float voltage = (raw / 1023.0) * 3.3;  // Adjust if using voltage divider
  voltage = ((voltage/3.3)*100);
  return voltage;
}
