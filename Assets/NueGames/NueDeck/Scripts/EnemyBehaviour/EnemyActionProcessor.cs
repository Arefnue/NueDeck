using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NueGames.NueDeck.Scripts.Enums;

namespace NueGames.NueDeck.Scripts.EnemyBehaviour
{
    public static class EnemyActionProcessor
    {
        private static readonly Dictionary<EnemyActionType, EnemyActionBase> EnemyActionDict =
            new Dictionary<EnemyActionType, EnemyActionBase>();

        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            EnemyActionDict.Clear();

            var allEnemyActions = Assembly.GetAssembly(typeof(EnemyActionBase)).GetTypes()
                .Where(t => typeof(EnemyActionBase).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var enemyAction in allEnemyActions)
            {
                EnemyActionBase action = Activator.CreateInstance(enemyAction) as EnemyActionBase;
                if (action != null) EnemyActionDict.Add(action.ActionType, action);
            }

            IsInitialized = true;
        }

        public static EnemyActionBase GetAction(EnemyActionType targetAction) =>
            EnemyActionDict[targetAction];
    }
}