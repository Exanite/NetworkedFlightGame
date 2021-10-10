using UnityEngine;
using UnityEngine.Serialization;

namespace Source.UI
{
    public class PointOfInterestMarker : MonoBehaviour
    {
        [FormerlySerializedAs("name")]
        [Header("Settings")]
        public string markerName;

        [Header("Debug")]
        public PointOfInterestUIManager pointOfInterestUIManager;

        private void OnEnable()
        {
            pointOfInterestUIManager = FindObjectOfType<PointOfInterestUIManager>();
            pointOfInterestUIManager.Register(this);
        }

        private void OnDisable()
        {
            pointOfInterestUIManager.Remove(this);
        }
    }
}