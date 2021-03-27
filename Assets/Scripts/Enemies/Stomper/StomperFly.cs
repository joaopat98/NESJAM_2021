using UnityEngine;

public class StomperFly : StomperState
{
    int direction = -1;
    public static StomperFly Create(Stomper target)
    {
        StomperFly state = StomperState.Create<StomperFly>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        //animator.Play("Fly");
    }

    public override void StateUpdate()
    {
        //TODO mudar para ser mesmo em cima do player
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
            SetState(StomperStomp.Create(target));
        }
    }
}