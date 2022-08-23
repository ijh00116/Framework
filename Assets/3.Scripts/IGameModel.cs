using UnityEngine;

public interface IGameModel:IModel
{
    BindableProperty<int> Gold { get; }

    BindableProperty<int> Score { get; }
}

public class GameModel : AbstractModel, IGameModel
{
    public BindableProperty<int> Gold { get; } = new BindableProperty<int>()
    {
        Value = 0
    };

    public BindableProperty<int> Score { get; } = new BindableProperty<int>()
    {
        Value = 0
    };

    protected override void OnInit()
    {
        Gold.Value = 0;
        Gold.Register(o => { Debug.Log($"°ñµå È¹µæ: {Gold.ToString()}"); } );

        Score.Value = 0;
        Score.Register(o => { Debug.Log($"½ºÄÚ¾î È¹µæ: {Gold.ToString()}"); });
    }
}

