using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackTree;

public interface IMyinter
{

}

public interface ISecondInter:IMyinter
{

}
public class InGameManager : Architecture<InGameManager>
{
    protected override void Init()
    {
        RegisterSystem<IScoreSystem>(new ScoreSystem());
        RegisterModel<IGameModel>(new GameModel());
    }
}
