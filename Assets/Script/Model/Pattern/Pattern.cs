using UnityEngine;
using System.Collections.Generic;
using Com.oHMysTArs.Grid;
using static Com.oHMysTArs.Grid.GridSystem;
using System.Linq;

namespace Com.oHMysTArs.Pattern
{
    [CreateAssetMenu(fileName = "New pattern", menuName = "Pattern")]
    public sealed class Pattern : ScriptableObject
    {
        public Coordinate[] Order;

        public static Pattern Init(string name, Coordinate[] coordinates)
        {
            Pattern pattern = ScriptableObject.CreateInstance<Pattern>();
            pattern.name = name;
            pattern.Order = coordinates;
            return pattern;
        }

        public static Pattern Record(List<Point> points) 
            => Init("runtime_internal", 
                points.Select(point => point.Coordinate).ToArray());

        public static bool Match(Pattern first, Pattern second)
        {
            if (first.Order.Length != second.Order.Length) return false;
            for (int i = 0; i < first.Order.Length; i++) 
            {
                if (!Coordinate.Match(first.Order[i], second.Order[i])) return false;
            }
            return true;
        }
    }
}