using UnityEngine;

public class StomperStomp : StomperState
{
    bool hit;
    
    public static StomperStomp Create(Stomper target)
    {
        StomperStomp state = StomperState.Create<StomperStomp>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        //animator.Play("Stomp");
        hit = false;
    }
    public override void StateFixedUpdate()
    {

        if (!hit)
        {
          rb.MovePosition(rb.position + Vector2.down * target.DropSpeed * Time.fixedDeltaTime);
        }
        else
        {
            SetState(StomperFlyUp.Create(target));
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (target.hittables.HasLayer(collider.gameObject.layer))
        {
            hit = true;
        }
    }

    public override void StateUpdate()
    {
    }
}