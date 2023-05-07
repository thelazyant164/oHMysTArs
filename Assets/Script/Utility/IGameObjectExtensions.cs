using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IGameObjectExtensions
{
    private static List<GameObject> FindChildrenWithTag(this Transform parent, string tag)
    {
        List<GameObject> taggedGameObjects = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == tag)
            {
                taggedGameObjects.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                taggedGameObjects.AddRange(FindChildrenWithTag(child, tag));
            }
        }
        return taggedGameObjects;
    }

    public static GameObject FindChildWithTag(this GameObject parent, string tag)
    {
        return parent.transform.FindChildrenWithTag(tag).FirstOrDefault();
    }
}
