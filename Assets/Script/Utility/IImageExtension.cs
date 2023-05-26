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
}
