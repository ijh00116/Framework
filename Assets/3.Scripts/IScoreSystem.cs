public interface IScoreSystem:ISystem
{

}

public class ScoreSystem : AbstractSystem,IScoreSystem
{
    protected override void OnInit()
    {
        var gamemodel = this.GetModel<IGameModel>();

        
    }

    
}