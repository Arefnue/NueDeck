using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NueDeck.Scripts.Card.CardActions;

namespace NueDeck.Scripts.Card
{
    public static class CardActionProcessor
    {
        private static Dictionary<CardActionData.PlayerActionType, CardActionBase> _cardActionDict =
            new Dictionary<CardActionData.PlayerActionType, CardActionBase>();

        private static bool _initialized;

        private static void Initialize()
        {
            _cardActionDict.Clear();

            var allActionCards = Assembly.GetAssembly(typeof(CardActionBase)).GetTypes()
                .Where(t => typeof(CardActionBase).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var actionCard in allActionCards)
            {
                CardActionBase action = Activator.CreateInstance(actionCard) as CardActionBase;
                _cardActionDict.Add(action.ActionType,action);
            }

            _initialized = true;
        }



    }
}