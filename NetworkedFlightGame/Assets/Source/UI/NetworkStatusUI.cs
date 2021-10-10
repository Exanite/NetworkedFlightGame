using TMPro;
using UnityEngine;

namespace Source.UI
{
    public class NetworkStatusUI : MonoBehaviour
    {
        [Header("Dependencies")]
        public GameManager gameManager;

        public TMP_Text clientText;
        public TMP_Text serverText;
        public TMP_Text pingText;

        [Header("Settings")]
        public string pingPlaceholder = "---";
        public Color successColor = Color.green;
        public Color failureColor = Color.red;

        public bool IsClientConnected => gameManager.client?.network.IsReady ?? false;
        public bool IsServerRunning => gameManager.server?.network.IsReady ?? false;

        private void Update()
        {
            clientText.color = IsClientConnected ? successColor : failureColor;
            serverText.color = IsServerRunning ? successColor : failureColor;

            pingText.text = GetPing();
        }

        public string GetPing()
        {
            if (!IsClientConnected)
            {
                return pingPlaceholder;
            }

            return gameManager.client.network.Server.Ping.ToString();
        }
    }
}