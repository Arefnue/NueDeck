using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NueDeck.Scripts.Enums;

namespace NueDeck.Scripts.EnemyBehaviour
{
    public static class EnemyActionProcessor
    {
        private static Dictionary<EnemyActionType, EnemyActionBase> _enemyActionDict =
            new Dictionary<EnemyActionType, EnemyActionBase>();

        private static bool _initialized;

        public static void Initialize()
        {
            _enemyActionDict.Clear();

            var allEnemyActions = Assembly.GetAssembly(typeof(EnemyActionBase)).GetTypes()
                .Where(t => typeof(EnemyActionBase).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var enemyAction in allEnemyActions)
            {
                EnemyActionBase action = Activator.CreateInstance(enemyAction) as EnemyActionBase;
                _enemyActionDict.Add(action.ActionType,action);
            }

            _initialized = true;
        }

        public static EnemyActionBase GetAction(EnemyActionType targetAction) =>
            _enemyActionDict[targetAction];
    }
}