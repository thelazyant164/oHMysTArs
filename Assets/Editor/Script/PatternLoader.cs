using Com.oHMysTArs.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Com.oHMysTArs.Grid;
using System.Linq;
using static Com.oHMysTArs.Grid.GridSystem;

namespace Com.oHMysTArs.Pattern
{
    public sealed class PatternLoader : BaseLoader<Pattern>
    {
        public PatternLoader() : base("Pattern", "Pattern_data.csv") { }

        [MenuItem("Utility/Parse/Pattern")]
        public static void Invoke() => new PatternLoader().Generate();

        public override List<Pattern> ParseItems(string[] lines)
        {
            List<Pattern> items = new List<Pattern>();
            foreach (string line in lines)
            {
                string[] fields = line.Split(",");
                if (fields.Length == 0) break;
                List<Coordinate> predefinedOrder = new();
                for (int i = 0; i < fields.Length - 1; i++) 
                {
                    if (fields[i + 1] == "") break;
                    predefinedOrder.Add(ParseCoordinate(fields[i + 1]));
                }

                Pattern newPattern = Pattern.Init(fields[0], predefinedOrder.ToArray());
                items.Add(newPattern);
            }
            return items;
        }

        private Coordinate ParseCoordinate(string coordinate)
        {
            string[] coordinates = coordinate.Split(":");
            int column = DataParser.ParseInt(coordinates[0]);
            int row = DataParser.ParseInt(coordinates[1]);
            return new Coordinate(column, row);
        }
    }
}
