using System;
using NueDeck.Scripts.Enums;
using NueDeck.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace NueDeck.Scripts.UI
{
    public class EncounterButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image completedImage;
        [SerializeField] private bool isFinal;
        
        private void Awake()
        {
            completedImage.gameObject.SetActive(false);
        }


        public void SetStatus(EncounterButtonStatus targetStatus)
        {
            
            
            switch (targetStatus)
            {
                case EncounterButtonStatus.Active:
                    button.interactable = true;
                    if (isFinal) GameManager.Instance.PersistentGameplayData.IsFinalEncounter = true;
                    
                    break;
                case EncounterButtonStatus.Passive:
                    button.interactable = false;
                    break;
                case EncounterButtonStatus.Completed:
                    button.interactable = false;
                    completedImage.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetStatus), targetStatus, null);
            }
        }
    }
}