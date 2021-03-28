using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnLadders : MonoBehaviour
{
    [SerializeField] LayerMask LadderMask;
    LayerMask DefaultMask;
    PlatformEffector2D effector;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        DefaultMask = effector.colliderMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground")))
        {
            effector.colliderMask = LadderMask;
        }
        else
        {
            effector.colliderMask = DefaultMask;
        }
    }
}
