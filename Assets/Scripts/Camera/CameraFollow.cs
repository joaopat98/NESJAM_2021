using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraFollow : MonoBehaviour
{
    PlayerMovement player;
    [SerializeField] Vector2 DeadZone;
    [SerializeField] Vector2 CenterOffset;

    void OnDrawGizmosSelected()
    {
        Vector3 right = DeadZone.x * Vector3.right;
        Vector3 up = DeadZone.y * Vector3.up;
        Vector3 pos = transform.position + (Vector3)CenterOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos - right - up, pos + right - up);
        Gizmos.DrawLine(pos + right - up, pos + right + up);
        Gizmos.DrawLine(pos + right + up, pos - right + up);
        Gizmos.DrawLine(pos - right + up, pos - right - up);
    }

    void OnValidate()
    {
        PixelPerfectCamera camera = GetComponent<PixelPerfectCamera>();
        DeadZone.x = Mathf.Round(Mathf.Max(0, DeadZone.x) * camera.assetsPPU) / camera.assetsPPU;
        DeadZone.y = Mathf.Round(Mathf.Max(0, DeadZone.y) * camera.assetsPPU) / camera.assetsPPU;
        CenterOffset.x = Mathf.Round(CenterOffset.x * camera.assetsPPU) / camera.assetsPPU;
        CenterOffset.y = Mathf.Round(CenterOffset.y * camera.assetsPPU) / camera.assetsPPU;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = player.transform.position;
        pos.z = transform.position.z;
        Vector3 offset = pos - (transform.position + (Vector3)CenterOffset);
        Vector3 camMove = Vector3.zero;
        if (Mathf.Abs(offset.x) > DeadZone.x)
        {
            camMove.x += Mathf.Sign(offset.x) * (Mathf.Abs(offset.x) - DeadZone.x);
        }
        if (Mathf.Abs(offset.y) > DeadZone.y)
        {
            camMove.y += Mathf.Sign(offset.y) * (Mathf.Abs(offset.y) - DeadZone.y);
        }
        transform.position += camMove;

    }
}
