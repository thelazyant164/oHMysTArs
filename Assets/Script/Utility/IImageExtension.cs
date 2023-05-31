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

    public static IEnumerator ToggleFadeInOut(this Image image, float duration)
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

    public static int GetSortingLayer(this Image image)
    {
        Canvas canvas = image.GetComponentInParent<Canvas>();
        return canvas.sortingLayerID;
    }

    public static void SetSortingLayer(this Image image, int sortingLayerId)
    {
        Canvas canvas = image.GetComponentInParent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingLayerID = sortingLayerId;
    }

    public static IEnumerator FadeTo(this Image image, float targetOpacity, float fadeDuration)
    {
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetOpacity);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            image.color = Color.Lerp(startColor, targetColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = targetColor;
    }

    public static IEnumerator Flash(this Image image, float startOpacity, float endOpacity, float interval, float duration)
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                float currentOpacity = Mathf.Lerp(startOpacity, endOpacity, t);

                image.color = new Color(image.color.r, image.color.g, image.color.b, currentOpacity);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, endOpacity);

            yield return new WaitForSeconds(interval);

            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                float currentOpacity = Mathf.Lerp(endOpacity, startOpacity, t);

                image.color = new Color(image.color.r, image.color.g, image.color.b, currentOpacity);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, startOpacity);

            yield return new WaitForSeconds(interval);
        }
    }
}
