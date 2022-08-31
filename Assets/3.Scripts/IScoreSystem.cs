public interface IScoreSystem:ISystem
{

}

public class AddGold { }
public class AddScore { }
public class ScoreSystem : AbstractSystem,IScoreSystem
{
    protected override void OnInit()
    {
        var gamemodel = this.GetModel<IGameModel>();

        this.RegisterEvent<AddGold>(o => {
            gamemodel.Gold.Value += 10;
            UnityEngine.Debug.Log($"���:{gamemodel.Gold}");
        });
        this.RegisterEvent<AddScore>(o => {
            gamemodel.Score.Value += 10;
            UnityEngine.Debug.Log($"���ھ�:{gamemodel.Score}");
        });
    }

    
}