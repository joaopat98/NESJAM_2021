using UnityEngine;

public class CaterpillarIdle : CaterpillarState
{
    public static CaterpillarIdle Create(Caterpillar target)
    {
        CaterpillarIdle state = CaterpillarState.Create<CaterpillarIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        animator.Play("Idle");
    }

    public override void StateUpdate()
    {
        //Eles não fazem nada só ficam parados.
    }
}
