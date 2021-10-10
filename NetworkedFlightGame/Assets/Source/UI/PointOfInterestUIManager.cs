using System.Collections.Generic;
using UnityEngine;

namespace Source.UI
{
    public class PointOfInterestUIManager : MonoBehaviour
    {
        [Header("Dependencies")]
        public Camera targetCamera;
        public PointOfInterestMarkerUI markerUIPrefab;

        [Header("Debug")]
        public List<PointOfInterestMarkerUI> markerUIs;

        public void Register(PointOfInterestMarker marker)
        {
            var markerUI = Instantiate(markerUIPrefab);
            markerUI.transform.SetParent(transform);
            markerUI.targetCamera = targetCamera;
            markerUI.marker = marker;
            
            markerUIs.Add(markerUI);
        }

        public void Remove(PointOfInterestMarker marker)
        {
            var markerUI = markerUIs.Find(x => x.marker ==marker);

            if (markerUI)
            {
                markerUIs.Remove(markerUI);
                Destroy(markerUI.gameObject);
            }
        }
    }
}