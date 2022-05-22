using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NueGames.NueDeck.Scripts.Enums;

namespace NueGames.NueDeck.Scripts.Card
{ 
    public static class CardActionProcessor
    {
        private static readonly Dictionary<CardActionType, CardActionBase> CardActionDict =
            new Dictionary<CardActionType, CardActionBase>();

        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            CardActionDict.Clear();

            var allActionCards = Assembly.GetAssembly(typeof(CardActionBase)).GetTypes()
                .Where(t => typeof(CardActionBase).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var actionCard in allActionCards)
            {
                CardActionBase action = Activator.CreateInstance(actionCard) as CardActionBase;
                if (action != null) CardActionDict.Add(action.ActionType, action);
            }

            IsInitialized = true;
        }

        public static CardActionBase GetAction(CardActionType targetAction) =>
            CardActionDict[targetAction];

    }
}