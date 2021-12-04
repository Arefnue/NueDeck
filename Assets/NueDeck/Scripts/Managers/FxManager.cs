using System;
using System.Collections.Generic;
using System.Linq;
using NueDeck.Scripts.Enums;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class FxManager : MonoBehaviour
    {
        public static FxManager Instance;
    
        [Header("Fx")] 
        public List<FxBundle> fxList;


        public Dictionary<FxType, GameObject> fxDict = new Dictionary<FxType, GameObject>();
        
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            for (int i = 0; i < Enum.GetValues(typeof(FxType)).Length; i++)
            {
                fxDict.Add((FxType)i,fxList.FirstOrDefault(x=>x.myType == (FxType)i)?.myObject);
            }
        }

        public void PlayFx(Transform targetTransform, FxType targetFx)
        {
            Instantiate(fxDict[targetFx], targetTransform);
        }
        
    }

    [Serializable]
    public class FxBundle
    {
        public FxType myType;
        public GameObject myObject;
    }
}