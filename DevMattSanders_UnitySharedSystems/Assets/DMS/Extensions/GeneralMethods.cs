using System.Collections.Generic;
using UnityEngine;

namespace DMS.Extensions
{
    public static class GeneralMethods
    {
        public static Vector3 MeanVector(this List<Vector3> positions)
        {
            if (positions.Count == 0)
                return Vector3.zero;
            float x = 0f;
            float y = 0f;
            float z = 0f;
            foreach (Vector3 pos in positions)
            {
                x += pos.x;
                y += pos.y;
                z += pos.z;
            }
            return new Vector3(x / positions.Count, y / positions.Count, z / positions.Count);
        }
    }
}
