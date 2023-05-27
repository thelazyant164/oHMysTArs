using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public static class IImageExtension
{
    public static void SetTexture(this Image source, Texture2D newTex)
    {
        source.sprite = Sprite.Create(newTex, new Rect(Vector2.zero, new Vector2(newTex.width, newTex.height)), new Vector2(.5f, .5f));
    }

    public static IEnumerator Fade(this Image image, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = image.color;
        Color targetColor = startColor;
        targetColor.a = 1f - startColor.a;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            image.color = Color.Lerp(startColor, targetColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = targetColor;
    }
}
