using TMPro;
using UnityEngine;

namespace Source.UI
{
    public class PointOfInterestMarkerUI : MonoBehaviour
    {
        [Header("Dependencies")]
        public Camera targetCamera;
        public PointOfInterestMarker marker;

        [Header("Settings")]
        public GameObject visuals;
        public TMP_Text labelText;
        public float pixelsDistanceFromEdgeOfScreen = 50;

        private void Update()
        {
            var position = targetCamera.WorldToScreenPoint(marker.transform.position);
            position.z = 0;
            position.x = Mathf.Clamp(position.x, pixelsDistanceFromEdgeOfScreen, Screen.width - pixelsDistanceFromEdgeOfScreen);
            position.y = Mathf.Clamp(position.y, pixelsDistanceFromEdgeOfScreen, Screen.height - pixelsDistanceFromEdgeOfScreen);

            transform.position = position;
            
            visuals.SetActive(targetCamera.transform.InverseTransformPoint(marker.transform.position).z > 0);
        }
    }
}