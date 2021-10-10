using System.Net;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.UI
{
    public class ConnectionUI : MonoBehaviour
    {
        [Header("Dependencies")]
        public GameManager gameManager;

        public CustomInputField playerNameField;
        public CustomInputField addressField;
        public CustomInputField portField;

        public Button connectButton;
        public Button startServerButton;
        public Button startServerAndConnectButton;

        [Header("Values")]
        public string playerNameString;
        public string addressString;
        public string portString;

        private void Start()
        {
            playerNameField.BindOnChanged(PlayerName_OnChanged, playerNameString);
            addressField.BindOnChanged(Address_OnChanged, addressString);
            portField.BindOnChanged(Port_OnChanged, portString);

            connectButton.onClick.AddListener(Connect_OnClick);
            startServerButton.onClick.AddListener(StartServer_OnClick);
            startServerAndConnectButton.onClick.AddListener(StartServerAndConnect_OnClick);
        }

        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        public void ShowWarningMessage(string message)
        {
            Debug.LogWarning(message);
        }

        private void Port_OnChanged(string value)
        {
            portString = value;
        }

        private void Address_OnChanged(string value)
        {
            addressString = value;
        }

        private void PlayerName_OnChanged(string value)
        {
            playerNameString = value;
        }

        private void Connect_OnClick()
        {
            if (!TryParseInput(out var input, out var message))
            {
                ShowWarningMessage(message);

                return;
            }

            gameManager.CreateClientAndConnect(input.EndPoint, input.PlayerName)
                .ContinueWith(x =>
                {
                    var isConnectSuccess = x;

                    if (!isConnectSuccess)
                    {
                        SetVisibility(true);
                    }
                });

            SetVisibility(false);
        }

        private void StartServer_OnClick()
        {
            if (!TryParseInput(out var input, out var message))
            {
                ShowWarningMessage(message);

                return;
            }

            gameManager.CreateServer(input.Port);

            SetVisibility(false);
        }

        private void StartServerAndConnect_OnClick()
        {
            if (!TryParseInput(out var input, out var message))
            {
                ShowWarningMessage(message);

                return;
            }

            gameManager.CreateServer(input.Port);
            gameManager.CreateClientAndConnect(new IPEndPoint(IPAddress.Loopback, input.Port), input.PlayerName);

            SetVisibility(false);
        }

        private bool TryParseInput(out ParsedInput input, out string message)
        {
            Debug.Log("Validating input...");

            input = new ParsedInput();
            message = string.Empty;

            var playerName = playerNameString.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                message = "Player name cannot be blank";

                return false;
            }

            if (!IPAddress.TryParse(addressString.Trim(), out var address))
            {
                message = "Address is invalid";

                return false;
            }

            if (!int.TryParse(portString.Trim(), out var port))
            {
                message = "Port needs to be a number";

                return false;
            }

            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                message = $"Port needs to be within {IPEndPoint.MinPort} and {IPEndPoint.MaxPort}";

                return false;
            }

            input.PlayerName = playerName;
            input.Address = address;
            input.Port = port;

            return true;
        }

        private struct ParsedInput
        {
            public string PlayerName;

            public IPAddress Address;
            public int Port;

            public IPEndPoint EndPoint => new IPEndPoint(Address, Port);
        }
    }

    public static class UIExtensions
    {
        public static void BindOnChanged(this CustomInputField field, UnityAction<string> onChanged, string defaultValue = null)
        {
            field.inputField.BindOnChanged(onChanged, defaultValue);
        }

        public static void BindOnChanged(this TMP_InputField field, UnityAction<string> onChanged, string defaultValue = null)
        {
            if (defaultValue != null)
            {
                field.text = defaultValue;
            }

            field.onValueChanged.AddListener(onChanged);

            onChanged(field.text);
        }
    }
}