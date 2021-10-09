using TMPro;
using UnityEngine;

namespace Source.UI
{
    [ExecuteAlways]
    public class CustomInputField : MonoBehaviour
    {
        [Header("Dependencies")]
        public TMP_Text nameText;
        public TMP_Text placeholderText;
        public TMP_InputField inputField;

        [Header("Settings")]
        public string fieldName = "Name";
        public string placeholder = "Placeholder";
        public string defaultValue = string.Empty;

        private void Start()
        {
            SetValues();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (!Application.isPlaying)
            {
                SetValues();
            }
        }
#endif

        private void SetValues()
        {
            if (nameText)
            {
                nameText.text = $"{fieldName}:";
            }

            if (placeholderText)
            {
                placeholderText.text = placeholder;
            }

            if (inputField)
            {
                inputField.text = defaultValue;
            }
        }
    }
}