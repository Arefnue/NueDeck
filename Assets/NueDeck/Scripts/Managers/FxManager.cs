using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NueDeck.Scripts.Managers
{
    public class FxManager : MonoBehaviour
    {
        public static FxManager instance;
        
        public enum FxType
        {
            Attack,
            Heal,
            Poison,
            Block,
            Str,
            Buff
        }

        [Header("Fx")] 
        public List<FxBundle> fxList;


        public Dictionary<FxType, GameObject> fxDict = new Dictionary<FxType, GameObject>();
        
        private void Awake()
        {
            instance = this;
            for (int i = 0; i < Enum.GetValues(typeof(FxType)).Length; i++)
            {
                fxDict.Add((FxType)i,fxList.FirstOrDefault(x=>x.myType == (FxType)i).myObject);
            }
        }

        public void PlayFx(Transform targetTransform, FxType targetFx)
        {
            var clone = Instantiate(fxDict[targetFx], targetTransform);
            
        }
        
    }

    [Serializable]
    public class FxBundle
    {
        public FxManager.FxType myType;
        public GameObject myObject;
    }
}