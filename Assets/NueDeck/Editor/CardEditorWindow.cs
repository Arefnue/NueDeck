using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NueDeck.Scripts.Data.Collection;
using NueDeck.Scripts.Enums;
using NueExtentions;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace NueDeck.Editor
{
    public class CardEditorWindow : ExtendedEditorWindow
    {
        private static CardEditorWindow CurrentWindow { get; set; }
        private SerializedObject _serializedObject;
        
        #region Cache Card Data
        private static CardData CachedCardData { get; set; }
        private List<CardData> AllCardDataList { get; set; }
        private CardData SelectedCardData { get; set; }
        private string CardId { get; set; }
        private string CardName{ get; set; }
        private int ManaCost{ get; set; }
        private Sprite CardSprite{ get; set; }
        private bool UsableWithoutTarget{ get; set; }
        private List<CardActionData> CardActionDataList{ get; set; }
        private List<CardDescriptionData> CardDescriptionDataList{ get; set; }
        private List<SpecialKeywords> SpecialKeywordsList{ get; set; }
        private AudioActionType AudioType{ get; set; }

        private void CacheCardData()
        {
            CardId = SelectedCardData.Id;
            CardName = SelectedCardData.CardName;
            ManaCost = SelectedCardData.ManaCost;
            CardSprite = SelectedCardData.CardSprite;
            UsableWithoutTarget = SelectedCardData.UsableWithoutTarget;
            CardActionDataList = new List<CardActionData>(SelectedCardData.CardActionDataList);
            CardDescriptionDataList = new List<CardDescriptionData>(SelectedCardData.CardDescriptionDataList);
            SpecialKeywordsList = new List<SpecialKeywords>(SelectedCardData.KeywordsList);
            AudioType = SelectedCardData.AudioType;

        }
        
        private void ClearCachedCardData()
        {
            CardId = String.Empty;
            CardName = String.Empty;
            ManaCost = 0;
            CardSprite = null;
            UsableWithoutTarget = false;
            CardActionDataList?.Clear();
            CardDescriptionDataList?.Clear();
            SpecialKeywordsList?.Clear();
            AudioType = AudioActionType.Attack;
        }
        #endregion
        
        #region Setup
        [MenuItem("NueDeck/CardEditor")]
        public static void OpenCardEditor() =>  CurrentWindow = GetWindow<CardEditorWindow>("Card Editor");
        public static void OpenCardEditor(CardData targetData)
        {
            CachedCardData = targetData;
            OpenCardEditor();
        } 
        
        private void OnEnable()
        {
            if (CachedCardData)
            {
                SelectedCardData = CachedCardData;
                _serializedObject = new SerializedObject(SelectedCardData);
                CacheCardData();
            }
            
            AllCardDataList?.Clear();
            AllCardDataList = ListExtentions.GetAllInstances<CardData>().ToList();

            Selection.selectionChanged += Repaint;
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= Repaint;
            CachedCardData = null;
            SelectedCardData = null;
        }

      
        #endregion

        #region Process
        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            
            DrawAllCardButtons();
            DrawSelectedCard();
            
            EditorGUILayout.EndHorizontal();
        }
        #endregion
        
        #region Layout Methods
        private void DrawAllCardButtons()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
            
            foreach (var data in AllCardDataList)
                if (GUILayout.Button(data.CardName))
                {
                    SelectedCardData = data;
                    _serializedObject = new SerializedObject(SelectedCardData);
                    CacheCardData();
                }
            
            EditorGUILayout.EndVertical();
        }
        private void DrawSelectedCard()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
            if (!SelectedCardData)
            {
                EditorGUILayout.LabelField("Select item");
                return;
            }
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            ChangeId();
            ChangeCardName();
            ChangeManaCost();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("Save",EditorStyles.miniButton))
                SaveCardData();
            
            EditorGUILayout.EndVertical();
        }
        
        #endregion

        #region Card Data Methods
        private void ChangeId()
        {
            GUILayout.BeginHorizontal();
            CardId = CardName = EditorGUILayout.TextField("Card Id:", CardName);
            GUILayout.EndHorizontal();
        }
        private void ChangeCardName()
        {
            GUILayout.BeginHorizontal();
            CardName = EditorGUILayout.TextField("Card Name:", CardName);
            GUILayout.EndHorizontal();
        }
        
        private void ChangeManaCost()
        {
            GUILayout.BeginHorizontal();
            ManaCost = EditorGUILayout.IntField("Mana Cost:", ManaCost);
            GUILayout.EndHorizontal();
        }

        private void ChangeCardSprite()
        {
            GUILayout.BeginHorizontal();
            GUILayout.EndHorizontal();
        }
        
        private void SaveCardData()
        {
            if (!SelectedCardData) return;
            
            SelectedCardData.EditId(CardId);
            SelectedCardData.EditCardName(CardName);
            SelectedCardData.EditManaCost(ManaCost);
            
            EditorUtility.SetDirty(SelectedCardData);
            AssetDatabase.SaveAssets();
        }

      
        #endregion
    }
}
