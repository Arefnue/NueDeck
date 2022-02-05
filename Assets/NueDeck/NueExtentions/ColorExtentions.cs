using UnityEngine;

namespace NueExtentions
{
    public static class ColorExtentions
    {
        public static string ColorString(string text, Color color)
        {
            return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
        }
       
    }
}