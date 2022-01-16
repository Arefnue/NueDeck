using System;
using System.Collections.Generic;
using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Data.Containers
{
    [CreateAssetMenu(fileName = "Special Keyword", menuName = "Data/Containers/Special Keyword Data", order = 0)]
    public class SpecialKeywordData : ScriptableObject
    {
        [SerializeField] private List<SpecialKeywordBase> specialKeywordBaseList;
        public List<SpecialKeywordBase> SpecialKeywordBaseList => specialKeywordBaseList;
        
        
    }

    [Serializable]
    public class SpecialKeywordBase
    {
        [SerializeField] private SpecialKeywords specialKeyword;
        [SerializeField][TextArea] private string contentText;

        public SpecialKeywords SpecialKeyword => specialKeyword;
        
        
        public string GetHeader(string overrideKeywordHeader = "")
        {
            return string.IsNullOrEmpty(overrideKeywordHeader) ? overrideKeywordHeader : specialKeyword.ToString();
        }

        public string GetContent(string overrideContent = "")
        {
            return string.IsNullOrEmpty(overrideContent) ? overrideContent : contentText;
        }
    }
}