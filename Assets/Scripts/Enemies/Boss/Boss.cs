using UnityEngine;

public class Boss : EnemyBase<Boss>
{
    [System.Serializable]
    public class First
    {
        public GameObject Assets;
        public int HealthThreshold;

        [System.Serializable]
        public class Light
        {
            public GameObject BombPrefab;
            public int NumBombs;
            public float TimeBetweenBombs;
            public float Cooldown;
            public float TeleportTime;
            public Transform[] Teleports;
            [HideInInspector] public int currentTeleport;
        }

        public Light light;

        [System.Serializable]
        public class Dark
        {
            public GameObject ClonePrefab;
            public GameObject BulletPrefab;
            public float TimeBetweenShots;
            public Transform StartPos;
            public Transform CloneStart;
            public LayerMask GroundMask;
            public float Speed;
            public float TimeWithShield;
        }

        public Dark dark;
    }
    public First first;

    [System.Serializable]
    public class Second
    {
        public GameObject Assets;
        public int HealthThreshold;
        [HideInInspector] public bool Started = false;

        public class Light
        {
            public Transform StartPoint;
            public GameObject BombPrefab;
            public float TimeBetweenBombs;
        }

        public Light light;

        [System.Serializable]
        public class Dark
        {
        }

        public Dark dark;
    }

    public Second second;

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

    public GameObject Shield;
    [HideInInspector] public bool isClone;

    [HideInInspector] public Boss other;

    protected override void Start()
    {
        base.Start();
        state = BossFirstPhaseDarkIdle.Create(this);
    }

    public void SetShieldStatus(bool status)
    {
        Shield.SetActive(status);
    }
}