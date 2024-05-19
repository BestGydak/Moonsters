using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Moonsters
{
    public static class EnumerableExtentsion
    {
        public static T GetRandom<T>(this IEnumerable<T> values)
        {
            var array = values.ToArray();
            var id = Random.Range(0, array.Length);
            return array[id];
        }
    }
}
