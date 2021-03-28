using UnityEngine;

public class Boss : EnemyBase<Boss>
{
    [System.Serializable]
    public class Light
    {
        [System.Serializable]
        public class First
        {
            public GameObject Assets;
            public GameObject BombPrefab;
            public int NumBombs;
            public float TimeBetweenBombs;
            public float Cooldown;
            public float TeleportTime;
            public Transform[] Teleports;
            [HideInInspector] public int currentTeleport;
            public int HealthThreshold;
        }
        public First first;

        [System.Serializable]
        public class Second
        {
            public GameObject Assets;
            public Transform StartPoint;
            public GameObject BombPrefab;
            public float TimeBetweenBombs;
            [HideInInspector] public bool Started = false;
            public int HealthThreshold;
        }

        public Second second;
    }
    new public Light light;

    [System.Serializable]
    public class Third
    {
        public GameObject Assets;
        public DirectionProvider[] Teleports;
        [HideInInspector] public int currentTeleport;
        public GameObject BulletPrefab;
        public float TimeBeforeShot;
        public float TimeAfterShot;
        [HideInInspector] public bool Started;
    }

    public Third third;

    protected override void Start()
    {
        base.Start();
        transform.position = light.first.Teleports[0].position;
        state = BossFirstPhaseLightIdle.Create(this);
    }
}