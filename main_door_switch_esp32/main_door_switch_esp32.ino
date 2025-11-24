#include <WiFi.h>
#include <PubSubClient.h>

// --- WiFi and MQTT settings ---
const char* ssid = "TP-Link_AA04";
const char* password = "12125951";
const char* mqtt_server = "192.168.0.2";

// --- GPIO configuration ---
#define SWITCH1 18
#define SWITCH2 19
#define SWITCH3 21
#define LED_PIN 2

// --- Globals ---
WiFiClient espClient;
PubSubClient client(espClient);
int activeGroup = 0;  // 0 = none, 1 = group1, 2 = group2, 3 = group3
unsigned long lastStatus = 0;

// --- Function declarations ---
void setup_wifi();
void reconnect();
void callback(char* topic, byte* payload, unsigned int length);
void blinkLED(int times);

void setup() {
  Serial.begin(115200);
  Serial.println();
  Serial.println("Starting ESP8266 Music Node...");

  pinMode(SWITCH1, INPUT_PULLUP);
  pinMode(SWITCH2, INPUT_PULLUP);
  pinMode(SWITCH3, INPUT_PULLUP);
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

  // --- Detect switch group selection ---
  if (digitalRead(SWITCH1) == LOW) activeGroup = 1;
  else if (digitalRead(SWITCH2) == LOW) activeGroup = 2;
  else if (digitalRead(SWITCH3) == LOW) activeGroup = 3;
  else activeGroup = 0;

  // --- Periodic status publish every 10 seconds ---

  if (activeGroup > 0) {

    char topic[32];
    sprintf(topic, "status/maindoor");

    String payload = String(activeGroup, 2);

    client.publish(topic, payload.c_str());
    Serial.print("[PUBLISH] ");
    Serial.print(topic);
    Serial.print(" -> ");
    Serial.println(payload);
  }
  if (millis() - lastStatus > 10000) {
    lastStatus = millis();

    char topic[32];
    sprintf(topic, "status/maindoor");

    String payload = String(0,2);

    client.publish(topic, payload.c_str());
    Serial.print("[PUBLISH] ");
    Serial.print(topic);
    Serial.print(" -> ");
    Serial.println(payload);
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
  if (activeGroup > 0) {
    char expectedTopic[32];
    sprintf(expectedTopic, "music/group%d", activeGroup);
    if (strcmp(topic, expectedTopic) == 0 && msg.equalsIgnoreCase("play")) {
      Serial.println("PLAY command received - blinking LED!");
      blinkLED(5);
    }
  }
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

