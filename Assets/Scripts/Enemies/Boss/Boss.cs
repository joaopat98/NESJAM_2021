using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : EnemyBase<Boss>
{
    [System.Serializable]
    public class First
    {
        public int HealthThreshold;
        public GameObject Assets;

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
            [HideInInspector] public bool started;
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
            [HideInInspector] public bool started;
        }

        public Dark dark;
    }
    public First first;

    [System.Serializable]
    public class Second
    {
        public int HealthThreshold;
        [HideInInspector] public bool started = false;
        public GameObject Assets;

        [System.Serializable]
        public class Light
        {
            public Transform StartPoint;
            public GameObject BombPrefab;
            public float TimeBetweenBombs;
            [HideInInspector] public bool started;
        }

        public Light light;

        [System.Serializable]
        public class Dark
        {
            [HideInInspector] public bool started;
            public int Damage;
            public float ImpulseStrength;
            public float ImpulseTime;
            public Transform StartPos;
            public LayerMask HitMask;
            public float FlySpeed;
            public float AttackRange;

            public float FlyRange;

            public float LockOnTime;

            public float DropSpeed;

            public float RecoverSpeed;
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
    public GameObject SecondShield;
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

    void OnDestroy()
    {
        if (!isClone)
        {
            Debug.Log("End");
            SceneManager.LoadScene("Scenes/Credits");
        }
    }
}