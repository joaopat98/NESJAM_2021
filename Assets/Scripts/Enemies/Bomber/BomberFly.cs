using UnityEngine;

public class BomberFly : BomberState
{
    int direction = -1;
    public static BomberFly Create(Bomber target)
    {
        BomberFly state = BomberState.Create<BomberFly>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        animator.Play("Fly");
    }

    public override void StateUpdate()
    {
        renderer.flipX = direction < 0;
        transform.Translate(Vector3.right * direction * target.FlySpeed * Time.deltaTime);
        float deltaX = (transform.position - target.StartPos).x;
        if (direction * deltaX > target.FlyRange / 2)
        {
            direction = -direction;
        }
        PlayerEntity player = PlayerEntity.instance;
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < target.AttackRange)
        {
            SetState(BomberDrop.Create(target));
        }
    }
}