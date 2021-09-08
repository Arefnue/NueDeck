namespace NueDeck.Scripts.Card.CardActions
{
    public abstract class CardActionBase
    {
        public CardActionBase(){}
        public abstract CardActionData.PlayerActionType ActionType { get;}
        public abstract void DoAction();
        
    }
}