using System.Text;
using NueDeck.Scripts.Data.Collection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NueDeck.Editor
{
    public class CardEditorWindow : EditorWindow
    {
        public CardData cardData;

        private static CardData _cardData;
        private SerializedObject _so;
        private SerializedProperty _propCardData;
       
        [MenuItem("NueDeck/CardEditor")]
        public static void OpenCardEditor() => GetWindow<CardEditorWindow>("Card Editor");

        public static void OpenCardEditor(CardData targetData)
        {
            _cardData = targetData;
            GetWindow<CardEditorWindow>("Card Editor");
        } 
        
        private void OnEnable()
        {
            if (_cardData)
                cardData = _cardData;
            _so = new SerializedObject(this);
            _propCardData = _so.FindProperty("cardData");
           
            Selection.selectionChanged += Repaint;
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= Repaint;
            _cardData = null;
        }

        void OnGUI()
        {
            _so.Update();
            SetCardView();
            _so.ApplyModifiedProperties();
            
        }

        private void SetCardView()
        {
            EditorGUILayout.PropertyField(_propCardData);
            if (!cardData) return;
         
            GUILayout.Space(10);
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            ChangeId();
            ChangeCardName();
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("Save",EditorStyles.miniButton))
                SaveCardData();
        }

        private void ChangeId()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Card Id: ", EditorStyles.boldLabel);
            
            var newId = GUILayout.TextArea(cardData.Id);
           
            cardData.EditId(newId);
            GUILayout.EndHorizontal();
        }
        private void ChangeCardName()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Card Name: ", EditorStyles.boldLabel);
            var newCardName = GUILayout.TextArea(cardData.CardName);
            cardData.EditCardName(newCardName);
            GUILayout.EndHorizontal();
        }
        
        private void SaveCardData()
        {
            if (!cardData) return;
            EditorUtility.SetDirty(cardData);
            AssetDatabase.SaveAssets();
        }

      
    }
}
