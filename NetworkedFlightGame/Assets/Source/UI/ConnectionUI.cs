using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.UI
{
    public class ConnectionUI : MonoBehaviour
    {
        [Header("Dependencies")]
        public CustomInputField playerNameField;
        public CustomInputField addressField;
        public CustomInputField portField;
        public Button connectButton;

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
        }

        private void Port_OnChanged(string value)
        {
            portString = value;
        }

        public void Address_OnChanged(string value)
        {
            addressString = value;
        }

        public void PlayerName_OnChanged(string value)
        {
            playerNameString = value;
        }

        public void Connect_OnClick()
        {
            Debug.Log("Validating input...");

            if (string.IsNullOrWhiteSpace(playerNameString.Trim()))
            {
                Debug.LogWarning("Player name cannot be blank");

                return;
            }

            if (!IPAddress.TryParse(addressString.Trim(), out var address))
            {
                Debug.LogWarning("Address is invalid");

                return;
            }

            if (!int.TryParse(portString.Trim(), out var port))
            {
                Debug.LogWarning("Port needs to be a number");

                return;
            }

            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                Debug.LogWarning($"Port needs to be within {IPEndPoint.MinPort} and {IPEndPoint.MaxPort}");

                return;
            }

            var endpoint = new IPEndPoint(address, port);

            Debug.Log($"Trying to connect to endpoint '{endpoint}' with player name '{playerNameString}'");
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