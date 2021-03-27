using UnityEngine;

public class BomberRegen : BomberState
{
    public static BomberRegen Create(Bomber target)
    {
        BomberRegen state = BomberState.Create<BomberRegen>(target);
        return state;
    }
    public override void StateStart()
    {
        base.StateStart();
        animator.Play("Regen");
    }

    public override void StateUpdate() { }

    void FinishRegen()
    {
        SetState(BomberFly.Create(target));
    }
}