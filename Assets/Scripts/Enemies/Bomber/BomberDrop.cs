using UnityEngine;

public class BomberDrop : BomberState
{
    float timer = 0;
    bool dropped;

    public static BomberDrop Create(Bomber target)
    {
        BomberDrop state = BomberState.Create<BomberDrop>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        animator.Play("Prepare");
    }
    public override void StateUpdate()
    {
        if (!dropped)
        {
            timer += Time.deltaTime;
            if (timer > target.TimeToPrepare)
            {
                animator.Play("Drop");
                dropped = true;
                var bomb = Instantiate(target.BombPrefab, target.BombSpawn.position, Quaternion.identity).GetComponent<BomberBomb>();
                bomb.Flip = renderer.flipX;
                bomb.dropper = this;
            }
        }
    }

    public void Regen()
    {
        SetState(BomberRegen.Create(target));
    }
}