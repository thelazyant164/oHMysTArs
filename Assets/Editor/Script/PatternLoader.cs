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
    public sealed class PatternLoader : BaseGenerator<Pattern>
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
                Coordinate[] predefinedOrder = new Coordinate[fields.Length - 1];
                for (int i = 1; i < fields.Length; i++) 
                {
                    predefinedOrder[i - 1] = ParseCoordinate(fields[i]);
                }

                Pattern newPattern = Pattern.Init(fields[0], predefinedOrder);
                items.Add(newPattern);
            }
            return items;
        }

        private Coordinate ParseCoordinate(string coordinate)
        {
            string[] coordinates = coordinate.Split(":");
            int column = CsvToScriptableObject.ParseInt(coordinates[0]);
            int row = CsvToScriptableObject.ParseInt(coordinates[1]);
            return new Coordinate(column, row);
        }
    }
}
