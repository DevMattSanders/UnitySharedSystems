using System.Collections.Generic;
using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public static class Extensions
    {    

        //String
        public static string SplitString(this string input)
        {
            return string.Join(" ", input.ToCharArray());
        }

        //Vector3
        public static Vector2 ToV2(this Vector3 input) => new Vector2(input.x, input.y);

        public static Vector3 Flat(this Vector3 input) => new Vector3(input.x, 0, input.z);

        public static Vector3Int ToVector3Int(this Vector3 input) => new Vector3Int((int)input.x, (int)input.y, (int)input.z);

        //List
        public static T RandomValue<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        //Transform
        public static void DestroyChildren(this Transform t)
        {
            foreach(Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }

        //GameObject
        public static void SetLayersRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform t in gameObject.transform) t.gameObject.SetLayersRecursively(layer);
        }

        //Sprite Renderer
        public static void SetAlpha(this SpriteRenderer renderer, float alpha)
        {
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}