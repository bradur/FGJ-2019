// Author : bradur

using UnityEngine;
using TiledSharp;
using System.IO;

public class Tools : MonoBehaviour
{

    public static int IntParseFast(string value)
    {
        int result = 0;
        try
        {
            for (int i = 0; i < value.Length; i++)
            {
                char letter = value[i];
                result = 10 * result + (letter - 48);
            }
        }
        catch (System.NullReferenceException)
        {
            result = -1;
        }
        return result;
    }

    public static string GetProperty(PropertyDict properties, string property)
    {
        if (properties.ContainsKey(property))
        {
            return properties[property];
        }
        return null;
    }


    public static Texture2D duplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    public void SaveTextureAsPNG(Texture2D _texture)
    {
        Texture2D duplicate = duplicateTexture(_texture);
        byte[] pngShot = duplicate.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/" + duplicate.ToString() + "_" + Random.Range(0, 1024).ToString() + ".png", pngShot);
    }

    public static string ReplaceString(string originalString, string replaceThis, string replacement) {
        int subStringStartIndex = originalString.IndexOf(replaceThis);
        int subStringEndIndex = subStringStartIndex + replaceThis.Length;
        string firstHalf = "";
        if (subStringStartIndex > 0) {
            firstHalf = originalString.Substring(0, subStringStartIndex);
        }
        string secondHalf = "";
        if (subStringEndIndex > 0) {
            secondHalf = originalString.Substring(subStringEndIndex);
        }
        return string.Format("{0}{1}{2}", firstHalf, replacement, secondHalf);
    } 
}
