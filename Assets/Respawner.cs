using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] GameObject EntityPrefab;
    [SerializeField] float RespawnTime = 3;
    [SerializeField] GameObject[] WatchList;
    Vector3[] positions;
    Transform[] parents;
    bool[] respawning;
    // Start is called before the first frame update
    void Start()
    {
        respawning = new bool[WatchList.Length];
        positions = new Vector3[WatchList.Length];
        parents = new Transform[WatchList.Length];
        for (int i = 0; i < WatchList.Length; i++)
        {
            respawning[i] = false;
            positions[i] = WatchList[i].transform.position;
            parents[i] = WatchList[i].transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < WatchList.Length; i++)
        {
            if (WatchList[i] == null && !respawning[i])
            {
                StartCoroutine(Respawn(i));
            }
        }
    }

    IEnumerator Respawn(int index)
    {
        respawning[index] = true;
        yield return new WaitForSeconds(RespawnTime);
        WatchList[index] = Instantiate(EntityPrefab, positions[index], Quaternion.identity, parents[index]);
        respawning[index] = false;
    }
}
