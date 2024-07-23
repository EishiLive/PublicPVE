using UnityEngine;

#nullable enable

namespace EscapeFromTarkovCheat.Data
{

    public static class CameraExtensions
    {
        private static readonly LayerMask _layerMask = 0b0010_00100_0101_0001_1000_0000_0000;

        public static Vector2 WorldPointToScreenPoint(this Camera camera, Vector3 worldPoint)
        {
            var screenPoint = camera.WorldToScreenPoint(worldPoint);
            var scale = Screen.height / (float)camera.scaledPixelHeight;
            screenPoint.y = Screen.height - screenPoint.y * scale;
            screenPoint.x *= scale;
            return screenPoint;
        }

        public static bool IsTransformVisible(this Camera camera, Transform transform)
        {
            var origin = camera.transform.position;
            var destination = transform.position;

            origin += (destination - origin).normalized * 0.1f; // Offset origin to avoid self-collision

            if (!Physics.Linecast(origin, destination, out var hitinfo, _layerMask))
                return false;

            return hitinfo.transform == transform;
        }

        public static Vector2 WorldPointToVisibleScreenPoint(this Camera camera, Vector3 worldPoint)
        {
            var screenPoint = camera.WorldToScreenPoint(worldPoint);
            var scale = Screen.height / (float)camera.scaledPixelHeight;
            screenPoint.y = Screen.height - screenPoint.y * scale;
            screenPoint.x *= scale;

            if (screenPoint.z > 0.01f && screenPoint.x > -5f && screenPoint.y > -5f && screenPoint.x < Screen.width && screenPoint.y < Screen.height)
                return screenPoint;

            return Vector2.zero;
        }
    }
}