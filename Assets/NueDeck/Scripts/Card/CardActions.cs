using System;
using NueDeck.Scripts.Controllers;
using NueDeck.Scripts.Managers;

namespace NueDeck.Scripts.Card
{
    public static class CardActions
    {
        public static void PlayCardAction(EnemyBase targetEnemy, PlayerAction playerAction)
        {
            switch (playerAction.myPlayerActionType)
            {
                case PlayerAction.PlayerActionType.Attack:
                    AttackTargetEnemy(targetEnemy, playerAction);
                    break;
                case PlayerAction.PlayerActionType.Heal:
                    HealPlayer(playerAction);
                    break;
                case PlayerAction.PlayerActionType.Block:
                    GainBlock(playerAction);
                    break;
                case PlayerAction.PlayerActionType.IncreaseStr:
                    GainStrength(playerAction);
                    break;
                case PlayerAction.PlayerActionType.IncreaseMaxHealth:
                    GainMaxHealth(playerAction);
                    break;
                case PlayerAction.PlayerActionType.Draw:
                    DrawCards(playerAction);
                    break;
                case PlayerAction.PlayerActionType.ReversePoisonDamage:
                    ReversePoisonToDamage(targetEnemy, playerAction);
                    break;
                case PlayerAction.PlayerActionType.ReversePoisonHeal:
                    ReversePoisonToHeal(playerAction);
                    break;
                case PlayerAction.PlayerActionType.IncreaseMana:
                    GainMana(playerAction);
                    break;
                case PlayerAction.PlayerActionType.StealMaxHealth:
                    StealMaxHealthFromTarget(targetEnemy, playerAction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region Methods

         private static void GainMana(PlayerAction playerAction)
        {
            HandManager.instance.IncreaseMana((int) playerAction.value);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Buff);
        }

        private static void ReversePoisonToHeal(PlayerAction playerAction)
        {
            var poisonCount = LevelManager.instance.playerController.myHealth.poisonStack;
            LevelManager.instance.playerController.myHealth.Heal(playerAction.value * poisonCount);
            LevelManager.instance.playerController.myHealth.ClearPoison();
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Heal);
        }

        private static void DrawCards(PlayerAction playerAction)
        {
            HandManager.instance.DrawCards((int) playerAction.value);
            //FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Buff);
        }

        private static void GainMaxHealth(PlayerAction playerAction)
        {
            GameManager.instance.ChangePlayerMaxHealth(playerAction.value);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Buff);
        }

        private static void GainStrength(PlayerAction playerAction)
        {
            LevelManager.instance.playerController.myHealth.ApplyStr((int) playerAction.value);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Str);
        }

        private static void GainBlock(PlayerAction playerAction)
        {
            LevelManager.instance.playerController.myHealth.ApplyBlock(playerAction.value);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Block);
        }


        private static void StealMaxHealthFromTarget(EnemyBase targetEnemy, PlayerAction playerAction)
        {
            targetEnemy.myHealth.DecreaseMaxHealth(playerAction.value);
            GameManager.instance.ChangePlayerMaxHealth(playerAction.value);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Buff);
            FxManager.instance.PlayFx(targetEnemy.fxParent,FxManager.FxType.Attack);
        }

        private static void ReversePoisonToDamage(EnemyBase targetEnemy, PlayerAction playerAction)
        {
            var poisonCount = LevelManager.instance.playerController.myHealth.poisonStack;
            targetEnemy.myHealth.TakeDamage(playerAction.value * poisonCount);
            LevelManager.instance.playerController.myHealth.ClearPoison();
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Buff);
            FxManager.instance.PlayFx(targetEnemy.fxParent,FxManager.FxType.Attack);
        }

        private static void HealPlayer(PlayerAction playerAction)
        {
            LevelManager.instance.playerController.myHealth.Heal(playerAction.value);
            FxManager.instance.PlayFx(LevelManager.instance.playerController.fxParent,FxManager.FxType.Heal);
        }

        private static void AttackTargetEnemy(EnemyBase targetEnemy, PlayerAction playerAction)
        {
            targetEnemy.myHealth.TakeDamage(playerAction.value + LevelManager.instance.playerController.myHealth.bonusStr);
            FxManager.instance.PlayFx(targetEnemy.fxParent,FxManager.FxType.Attack);
        }

        #endregion
    }
}