using UnityEngine;

public class WallCrawlerWalk : WallCrawlerState
{
    //TODO por alguma forma de 
    private int direction;

    public static WallCrawlerWalk Create(WallCrawler target)
    {
        WallCrawlerWalk state = WallCrawlerState.Create<WallCrawlerWalk>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        direction = target.startUp? 1 : -1;
        //TODO trocar sprite
    }

    public override void StateUpdate()
    {
            //TODO andar para cima e para baixo conforma direction
            //Parar para disparar quando fica em frente ao player
            SetState(WallCrawlerShoot.Create(target));
    }
}
