using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Linq.Expressions;
using Com.oHMysTArs.Pattern;
using Com.oHMysTArs.Spaceship;
using Com.oHMysTArs.Level;

public abstract class BaseLoader<T> where T : ScriptableObject
{
    private readonly string directoryName;
    private readonly string csvFileName;
    private string CsvFilePath => $"{DataParser.csvDirectoryPath}/{csvFileName}";
    private string OutDir => $"{DataParser.dataDirectoryPath}/{directoryName}";

    public BaseLoader(string directoryName, string csvFileName)
    {
        this.directoryName = directoryName;
        this.csvFileName = csvFileName;
    }

    public virtual void Generate()
    {
        string[] lines = File.ReadAllLines(CsvFilePath);
        List<T> items = ParseItems(lines);

        Debug.Log($"{items.Count()} items found at {CsvFilePath}");
        RemoveOldItems();
        CreateNewItems(items);
        Debug.Log($"Parse done, generated {items.Count()} items at {OutDir}");
    }

    public virtual string GetFileName(T item)
    {
        return $"{item.name}.asset";
    }

    public abstract List<T> ParseItems(string[] lines);

    private void RemoveOldItems()
    {
        string[] folderPath = { OutDir };
        foreach (var asset in AssetDatabase.FindAssets("", folderPath))
        {
            var filePath = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(filePath);
        }
    }

    private void CreateNewItems(List<T> items)
    {
        items.ForEach(item => AssetDatabase.CreateAsset(item, $"{OutDir}/{GetFileName(item)}"));
        AssetDatabase.SaveAssets();
    }
}

