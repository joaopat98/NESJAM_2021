using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClone : Boss
{

    protected override void Start()
    {
        state = BossFirstPhaseDarkIdle.Create(this);
    }
    public void InitClone(Boss original)
    {
        other = original;
        CurrentHealth = original.MaxHealth;
        isClone = true;
        first = original.first;
        second = original.second;
        third = original.third;
    }

    public override void Hit(int damage)
    {
        other.Hit(damage);
        state.OnGetHit();
    }
}
