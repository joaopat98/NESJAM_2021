using UnityEngine;

public class FixedEnemyIdle : FixedEnemyState
{
    public static FixedEnemyIdle Create(FixedEnemy target)
    {
        FixedEnemyIdle state = FixedEnemyState.Create<FixedEnemyIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        //Eles não fazem nada só ficam parados.
    }
}
