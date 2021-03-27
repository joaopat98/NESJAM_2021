using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WorldType
{
    Light,
    Dark,
}

[System.Serializable]
public class WorldSwitchEvent : UnityEvent<WorldType> { }

public class WorldManager : MonoBehaviour
{
    [SerializeField] WorldSwitchEvent m_onWorldSwitch = new WorldSwitchEvent();
    public static WorldSwitchEvent OnWorldSwitch { get => instance.m_onWorldSwitch; }
    [SerializeField] WorldType StartingWorld;
    static WorldManager instance;
    WorldType world;
    public static WorldType currentWorld { get => instance.world; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate WorldManager found in scene!", instance);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SwitchToWorld(StartingWorld);
    }

    void SwitchToWorld(WorldType world)
    {
        this.world = world;
        OnWorldSwitch.Invoke(world);
    }

    public static void SwitchWorld()
    {
        if (currentWorld == WorldType.Dark)
            instance.SwitchToWorld(WorldType.Light);
        else
            instance.SwitchToWorld(WorldType.Dark);
    }
}
