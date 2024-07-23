/**
 * Render class Credit to ZAT from Unknowncheats
 * https://www.unknowncheats.me/forum/members/562321.html
 */

using EFT;
using EscapeFromTarkovCheat.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromTarkovCheat.Utils
{
    public static class Render
    {
        public static Material DrawMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));

        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);
        private class RingArray
        {
            public Vector2[] Positions { get; private set; }

            public RingArray(int numSegments)
            {
                Positions = new Vector2[numSegments];
                var stepSize = 360f / numSegments;
                for (int i = 0; i < numSegments; i++)
                {
                    var rad = Mathf.Deg2Rad * stepSize * i;
                    Positions[i] = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
                }
            }
        }

        private static readonly Dictionary<int, RingArray> ringDict = new Dictionary<int, RingArray>();

        public static Color Color
        {
            get { return GUI.color; }
            set { GUI.color = value; }
        }

        public static void DrawLine(Vector2 from, Vector2 to, float thickness, Color color)
        {
            Color = color;
            DrawLine(from, to, thickness);
        }
        public static void DrawLine(Vector2 from, Vector2 to, float thickness)
        {
            var delta = (to - from).normalized;
            var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            GUIUtility.RotateAroundPivot(angle, from);
            DrawBox(from, Vector2.right * (from - to).magnitude, thickness, false);
            GUIUtility.RotateAroundPivot(-angle, from);
        }

        public static void DrawBox(float x, float y, float w, float h, Color color)
        {
            DrawLine(new Vector2(x, y), new Vector2(x + w, y), 1f, color);
            DrawLine(new Vector2(x, y), new Vector2(x, y + h), 1f, color);
            DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), 1f, color);
            DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), 1f, color);
        }

        public static void DrawBox(Vector2 position, Vector2 size, float thickness, Color color, bool centered = true)
        {
            Color = color;
            DrawBox(position, size, thickness, centered);
        }
        public static void DrawBox(Vector2 position, Vector2 size, float thickness, bool centered = true)
        {
            // Calculer la position du coin supérieur gauche si centré
            Vector2 upperLeft = centered ? position - size / 2f : position;

            // Dessiner les côtés de la boîte
            GUI.DrawTexture(new Rect(upperLeft.x, upperLeft.y, size.x, thickness), Texture2D.whiteTexture); // Haut
            GUI.DrawTexture(new Rect(upperLeft.x, upperLeft.y, thickness, size.y), Texture2D.whiteTexture); // Gauche
            GUI.DrawTexture(new Rect(upperLeft.x + size.x, upperLeft.y, thickness, size.y), Texture2D.whiteTexture); // Droite
            GUI.DrawTexture(new Rect(upperLeft.x, upperLeft.y + size.y, size.x + thickness, thickness), Texture2D.whiteTexture); // Bas
        }


        public static void DrawCross(Vector2 position, Vector2 size, float thickness, Color color)
        {
            Color = color;
            DrawCross(position, size, thickness);
        }
        public static void DrawCross(Vector2 position, Vector2 size, float thickness)
        {
            GUI.DrawTexture(new Rect(position.x - size.x / 2f, position.y, size.x, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y - size.y / 2f, thickness, size.y), Texture2D.whiteTexture);
        }

        public static void DrawDot(Vector2 position, Color color)
        {
            Color = color;
            DrawDot(position);
        }
        public static void DrawDot(Vector2 position)
        {
            DrawBox(position - Vector2.one, Vector2.one * 2f, 1f);
        }

        public static void DrawString(Vector2 position, string label, Color color, bool centered = true)
        {
            Color = color;
            DrawString(position, label, centered);
        }
        public static void DrawString(Vector2 position, string label, bool centered = true)
        {
            var content = new GUIContent(label);
            var size = StringStyle.CalcSize(content);
            var upperLeft = centered ? position - size / 2f : position;
            GUI.Label(new Rect(upperLeft, size), content);
        }

        public static void DrawCircle(Vector2 position, float radius, int numSides, bool centered = true, float thickness = 1f)
        {
            DrawCircle(position, radius, numSides, Color.white, centered, thickness);
        }
        public static void DrawCircle(Vector2 position, float radius, int numSides, Color color, bool centered = true, float thickness = 1f)
        {
            RingArray arr;
            if (ringDict.ContainsKey(numSides))
                arr = ringDict[numSides];
            else
                arr = ringDict[numSides] = new RingArray(numSides);


            var center = centered ? position : position + Vector2.one * radius;

            for (int i = 0; i < numSides - 1; i++)
                DrawLine(center + arr.Positions[i] * radius, center + arr.Positions[i + 1] * radius, thickness, color);

            DrawLine(center + arr.Positions[0] * radius, center + arr.Positions[arr.Positions.Length - 1] * radius, thickness, color);
        }

        public static void DrawSnapline(Vector3 worldpos, Color color)
        {
            Vector3 pos = Main.MainCamera.WorldToScreenPoint(worldpos);
            pos.y = Screen.height - pos.y;
            GL.PushMatrix();
            GL.Begin(1);
            DrawMaterial.SetPass(0);
            GL.Color(color);
            GL.Vertex3(Screen.width / 2, Screen.height, 0f);
            GL.Vertex3(pos.x, pos.y, 0f);
            GL.End();
            GL.PopMatrix();
        }

        public static void DrawBoneLine(Vector2 from, Vector2 to, float thickness, Color color)
        {
            Color = color;
            DrawBoneLine(from, to, thickness);
        }

        public static void DrawBoneLine(Vector2 from, Vector2 to, float thickness)
        {
            var delta = (to - from).normalized;
            var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            GUIUtility.RotateAroundPivot(angle, from);
            DrawBox(from, Vector2.right * (from - to).magnitude, thickness, false);
            GUIUtility.RotateAroundPivot(-angle, from);
        }
        public static bool IsPointVisible(Vector3 point)
{
    Vector3 direction = point - Camera.main.transform.position;
    float distance = direction.magnitude;
    direction.Normalize();  // Normalisez la direction pour ne pas influencer la longueur du raycast

    if (Physics.Raycast(Camera.main.transform.position, direction, out RaycastHit hit, distance))
    {
        // Vérifiez si le raycast touche un objet avant d'atteindre le point donné
        return hit.distance >= distance;  // Si le rayon atteint une distance plus courte, le point est obstrué
    }

    // Si le raycast ne touche rien, le point est visible
    return true;
}

    }
}