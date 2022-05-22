using System.Text;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.NueExtentions
{
    public static class ColorExtentions
    {
        public static string ColorString(string text, Color color)
        {
            var str = new StringBuilder();
            str.Append("<color=#").Append(ColorUtility.ToHtmlStringRGBA(color)).Append(">").Append(text)
                .Append("</color>");
            return str.ToString();
        }
       
    }
}