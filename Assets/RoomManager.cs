using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{
    static RoomManager instance;
    List<RoomDivider> dividers;
    [SerializeField] int currentDivider = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            dividers = GetComponentsInChildren<RoomDivider>().ToList();
        }
        else
        {
            Debug.LogWarning("Duplicate RoomManager found in scene!", instance);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ActivateDividerAndPrevious(dividers[currentDivider]);
    }

    public static void ActivateDividerAndPrevious(RoomDivider current)
    {
        instance.currentDivider = instance.dividers.IndexOf(current);
        for (int i = 0; i <= instance.currentDivider; i++)
        {
            instance.dividers[i].Activate();
        }
    }

    public static Vector3 GetCurrentRespawnPoint()
    {
        return instance.dividers[instance.currentDivider].GetRespawnPoint();
    }
}
