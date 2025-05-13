using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MQTT
{
    public partial class MitZertifikat : Form
    {
        private const string url = "test.mosquitto.org";    //URL des MQTT-Brokers
        private const int port = 8886;                      //Port des MQTT-Brokers

        private IMqttClient mqttClient;
        private IMqttClientOptions mqttOptions;
        private X509Certificate clientCert;

        public MitZertifikat()
        {
            InitializeComponent();

            //Clientzertifikat, welches zur Authentifizierung verwendet wird
            clientCert = new X509Certificate(@"Zertifikate\client.pfx", "Beispiel");
        }

        #region Methoden

        private void InitializeMqttClient()
        {
            //MqttClient initialisieren
            MqttFactory mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();

            //Einstellungen für die TLS-Verbindung
            MqttClientOptionsBuilderTlsParameters tlsOptions = new MqttClientOptionsBuilderTlsParameters
            {
                UseTls = true,
                Certificates = new List<X509Certificate> { clientCert }
            };

            mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(url, port)
                .WithTls(tlsOptions)    //Die Verbindung erfolgt über TLS, um das Zertifikat zu verwenden
                .Build();

            //Wird aufgerufen, wenn der Client eine Verbindung hergestellt hat
            mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(e =>
            {
                if (txtLogPublish.InvokeRequired)  //Wird verwendet, da der Aufruf nicht vom UI-Thread erfolgt
                {
                    txtLogPublish.Invoke((MethodInvoker)delegate
                    {
                        txtLogPublish.AppendText("Verbunden mit dem Broker.\r\n\r\n");
                    });
                }
                else
                {
                    txtLogPublish.AppendText("Verbunden mit dem Broker.\r\n\r\n");
                }
            });

            //Wird aufgerufen, wenn der Client die Verbindung getrennt hat
            mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(e =>
            {
                if (txtLogPublish.InvokeRequired)  //Wird verwendet, da der Aufruf nicht vom UI-Thread erfolgt
                {
                    txtLogPublish.Invoke((MethodInvoker)delegate
                    {
                        txtLogPublish.AppendText("Verbindung zum Broker getrennt.\r\n\r\n");
                    });
                }
                else
                {
                    txtLogPublish.AppendText("Verbindung zum Broker getrennt.\r\n\r\n");
                }
                return Task.CompletedTask;
            });
        }

        //Abonniert das angegebene Thema
        private async void SubscribeToTopic(string topic)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                await mqttClient.SubscribeAsync(new MQTTnet.Client.Subscribing.MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(topic)
                    .Build());

                InitializeMessageHandler();

                txtLogSubscribe.AppendText($"Thema '{topic}' abonniert.\r\n\r\n");
            }
            else
            {
                txtLogSubscribe.AppendText("Client ist nicht verbunden. Abonnement fehlgeschlagen.\r\n\r\n");
            }
        }

        //Erstellt einen ApplicationMessageReceivedHandler, um die Empfangenen Nachrichten in die Textbox zu schreiben
        private void InitializeMessageHandler()
        {
            mqttClient.ApplicationMessageReceivedHandler = new MQTTnet.Client.Receiving.MqttApplicationMessageReceivedHandlerDelegate(e =>
            {
                if (e.ApplicationMessage.Topic != null && e.ApplicationMessage.Payload != null)
                {
                    string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    string topic = e.ApplicationMessage.Topic;

                    if (txtLogSubscribe.InvokeRequired)
                    {
                        txtLogSubscribe.Invoke((MethodInvoker)delegate
                        {
                            txtLogSubscribe.AppendText($"Nachricht empfangen:\r\nThema: {topic}\r\nNachricht: {payload}\r\n\r\n");
                        });
                    }
                    else
                    {
                        txtLogSubscribe.AppendText($"Nachricht empfangen:\r\nThema: {topic}\r\nNachricht: {payload}\r\n\r\n");
                    }
                }
            });
        }

        //Deabonniert das angegebene Thema
        private async void UnsubscribeFromTopic(string topic)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                await mqttClient.UnsubscribeAsync(topic);
                if (txtLogSubscribe.InvokeRequired)
                {
                    txtLogSubscribe.Invoke((MethodInvoker)delegate
                    {
                        txtLogSubscribe.AppendText($"Thema '{topic}' abbestellt.\r\n\r\n");
                    });
                }
                else
                {
                    txtLogSubscribe.AppendText($"Thema '{topic}' abbestellt.\r\n\r\n");
                }
            }
            else
            {
                if (txtLogSubscribe.InvokeRequired)
                {
                    txtLogSubscribe.Invoke((MethodInvoker)delegate
                    {
                        txtLogSubscribe.AppendText("Client ist nicht verbunden. Abbestellung fehlgeschlagen.\r\n\r\n");
                    });
                }
                else
                {
                    txtLogSubscribe.AppendText("Client ist nicht verbunden. Abbestellung fehlgeschlagen.\r\n\r\n");
                }
            }
        }

        //Veröffentlicht die angegebene Nachricht, zum angegebenen Thema
        private async void PublishMessage(string topic, string message)
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                MqttApplicationMessage mqttMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build();

                await mqttClient.PublishAsync(mqttMessage);
                if (txtLogPublish.InvokeRequired)
                {
                    txtLogPublish.Invoke((MethodInvoker)delegate
                    {
                        txtLogPublish.AppendText($"Nachricht an '{topic}' gesendet: {message}\r\n\r\n");
                    });
                }
                else
                {
                    txtLogPublish.AppendText($"Nachricht an '{topic}' gesendet: {message}\r\n\r\n");
                }
            }
            else
            {
                if (txtLogPublish.InvokeRequired)
                {
                    txtLogPublish.Invoke((MethodInvoker)delegate
                    {
                        txtLogPublish.AppendText("Client ist nicht verbunden. Nachricht konnte nicht gesendet werden.\r\n\r\n");
                    });
                }
                else
                {
                    txtLogPublish.AppendText("Client ist nicht verbunden. Nachricht konnte nicht gesendet werden.\r\n\r\n");
                }
            }
        }

        #endregion

        #region Events

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (mqttClient == null)
            {
                InitializeMqttClient();
            }

            if (!mqttClient.IsConnected)
            {
                try
                {
                    await mqttClient.ConnectAsync(mqttOptions);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.Write(ex.Message);
                }
            }
            else
            {
                txtLogPublish.AppendText("Client ist bereits verbunden.\r\n\r\n");
            }
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (mqttClient.IsConnected)
            {
                await mqttClient.DisconnectAsync();
            }
            else
            {
                txtLogPublish.AppendText("Client ist bereits getrennt.\r\n\r\n");
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLogSubscribe.Text = String.Empty;
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            SubscribeToTopic("/beispiel/temperatur");
        }

        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            UnsubscribeFromTopic("/beispiel/temperatur");
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            PublishMessage("/beispiel/temperatur", "5.4°C");
        }

        #endregion
    }
}