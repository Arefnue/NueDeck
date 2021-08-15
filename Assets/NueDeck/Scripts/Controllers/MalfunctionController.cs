using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NueDeck.Scripts.Managers;
using NueExtentions;
using UnityEngine;
using UnityEngine.Rendering;

namespace NueDeck.Scripts.Controllers
{
    public class MalfunctionController : MonoBehaviour
    {
        public List<MalfunctionBase> allMalfunctions;

        [HideInInspector] public MalfunctionBase currentMalfunction;
        public int maxMalfunctionTurn = 5;
        [HideInInspector] public int malfunctionTurnCounter;

        public Volume myVolume;
        private bool _changeMalfunction=false;
        private void Awake()
        {
            myVolume.weight = 0;
        }


        private IEnumerator MalfunctionChange()
        {
            var waitFrame = new WaitForEndOfFrame();
            var timer = 0f;

            
            UIManager.instance.gameCanvas.SetActive(false);
            HandManager.instance.handController.cam.gameObject.SetActive(false);
            
            while (true)
            {
                timer += Time.deltaTime;

                myVolume.weight = Mathf.Lerp(0, 1, timer);
                
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
            
            timer = 0f;
            while (true)
            {
                timer += Time.deltaTime*10f;

                myVolume.weight = Mathf.Lerp(1, 0, timer);
                
                
                if (timer>=1f)
                {
                    break;
                }

                yield return waitFrame;
            }
            
            UIManager.instance.gameCanvas.SetActive(true);
            HandManager.instance.handController.cam.gameObject.SetActive(true);
            //LevelManager.instance.CurrentLevelState = LevelManager.LevelState.PlayerTurn;

        }

        public void CountMalfunction()
        {
            malfunctionTurnCounter--;
            if (malfunctionTurnCounter<=0)
            {
                malfunctionTurnCounter = maxMalfunctionTurn;
                GetRandomMalfunction();
                StartCoroutine(nameof(MalfunctionChange));
            }
            
            
            UIManager.instance.UpdateMalfunctionCounter();
        }
        
        public void GetRandomMalfunction()
        {
            var randomMalfunction = allMalfunctions.RandomItem();
            if (currentMalfunction != null)
            {
                if (currentMalfunction == randomMalfunction)
                {
                    randomMalfunction = allMalfunctions.FirstOrDefault(x => x != currentMalfunction);
                }
            }

            if (randomMalfunction != null)
            {
                ApplyStatus(randomMalfunction);
                UIManager.instance.UpdateMalfunctionName(currentMalfunction);
            }
            else
            {
                ApplyStatus(allMalfunctions.RandomItem());
            }
            
           
        }
        
        
        public void ApplyStatus(MalfunctionBase targetMalfunction)
        {
            if (currentMalfunction != null)
            {
                ReleaseStatus(currentMalfunction);
            }
            
            currentMalfunction = targetMalfunction;

            switch (targetMalfunction.myMalfunctionType)
            {
                case MalfunctionBase.MalfunctionType.VisualDisorder:
                    break;
                case MalfunctionBase.MalfunctionType.ReverseDraw:
                    break;
                case MalfunctionBase.MalfunctionType.SuicidalViolence:
                    break;
                case MalfunctionBase.MalfunctionType.CentralConcentration:
                    LevelManager.instance.CompressEnemies();
                    break;
                case MalfunctionBase.MalfunctionType.LackOfEmpathy:
                    UIManager.instance.UpdateHealthText();
                    foreach (var currentEnemy in LevelManager.instance.currentEnemies)
                    {
                        currentEnemy.myHealth.ChangeHealthText();
                    }
                    LevelManager.instance.playerController.myHealth.ChangeHealthText();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            UpdateStatus(targetMalfunction);
        }

        public void ReleaseStatus(MalfunctionBase targetMalfunction)
        {
            
            //currentMalfunction = null;
            switch (targetMalfunction.myMalfunctionType)
            {
                case MalfunctionBase.MalfunctionType.VisualDisorder:
                    break;
                case MalfunctionBase.MalfunctionType.ReverseDraw:
                    break;
                case MalfunctionBase.MalfunctionType.SuicidalViolence:
                    break;
                case MalfunctionBase.MalfunctionType.CentralConcentration:
                    LevelManager.instance.DecompressEnemies();
                    break;
                case MalfunctionBase.MalfunctionType.LackOfEmpathy:
                    // UIManager.instance.UpdateHealthText();
                    // foreach (var currentEnemy in LevelManager.instance.currentEnemies)
                    // {
                    //     currentEnemy.myHealth.ChangeHealthText();
                    // }
                    // LevelManager.instance.playerController.myHealth.ChangeHealthText();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateStatus(MalfunctionBase targetMalfunction)
        {
            UIManager.instance.UpdateHealthText();
            foreach (var currentEnemy in LevelManager.instance.currentEnemies)
            {
                currentEnemy.myHealth.ChangeHealthText();
            }
            LevelManager.instance.playerController.myHealth.ChangeHealthText();
        }
    }
    
    [Serializable]
    public class MalfunctionBase
    {
        public enum MalfunctionType
        {
            VisualDisorder,
            ReverseDraw,
            SuicidalViolence,
            CentralConcentration,
            LackOfEmpathy
        }

        public string malfunctionName;
        public MalfunctionType myMalfunctionType;
    }
    
}