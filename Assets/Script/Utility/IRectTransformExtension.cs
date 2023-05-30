using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IRectTransformExtension
{
    public static void Copy(this RectTransform target, RectTransform source)
    {
        target.anchorMin = source.anchorMin;
        target.anchorMax = source.anchorMax;
        target.anchoredPosition = source.anchoredPosition;
        target.sizeDelta = source.sizeDelta;
        target.pivot = source.pivot;
    }
}
