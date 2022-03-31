using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DMS.Extensions
{
    public static class Helpers
    {
        private static Camera _mainCamera;
        /*
	public static Camera MainCamera
    {
        get
        {
            if (_mainCamera == null) _mainCamera = Camera.main;
            return _mainCamera;
        }
    }
    */

        public static Camera MainCamera => _mainCamera ??= Camera.main;

        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }

        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _results;

        public static bool IsOverUI()
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
            return _results.Count > 0;
        }

        public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, MainCamera, out var result);
            return result;
        }

        public static void DeleteChildren(this Transform t)
        {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }

        public static void SetGlobalScale(Transform toSet, Vector3 globalScale, bool autoDiviveBySeven)
        {
            if (toSet.lossyScale == Vector3.zero)
            {
                return;
            }
            if (toSet == null)
            {
                return;
            }
            if (toSet.gameObject == null)
            {
                return;
            }
            toSet.localScale = Vector3.one;
            toSet.localScale = new Vector3(globalScale.x / toSet.lossyScale.x, globalScale.y / toSet.lossyScale.y, globalScale.z / toSet.lossyScale.z);

            //  if (dividingAllowed)
            //{
            //     if (autoDiviveBySeven) { toSet.localScale = toSet.localScale / 7; }
            //}
        }

        public static Vector3 GetClosestPointOnLine(Vector3 origin, Vector3 end, Vector3 point)
        {
            Vector3 heading = (end - origin);
            float magnitudeMax = heading.magnitude;
            heading.Normalize();

            Vector3 lhs = point - origin;
            float dotP = Vector3.Dot(lhs, heading);
            dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);

            return origin + heading * dotP;
        }
    }
}
