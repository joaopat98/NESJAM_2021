using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    protected CharacterController2D controller;
    protected bool active;

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        controller = FindObjectOfType<CharacterController2D>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            /*
            Vector2 normal = Vector3.zero;
            for (int i = 0; i < collision.contactCount; i++)
            {
                normal -= collision.GetContact(i).normal;
            }
            normal /= collision.contactCount;
            Debug.Log(normal);
            if (Vector2.Angle(Vector2.up, normal) < 10f)
                Activate();
                */
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Deactivate();

            collision.transform.parent = null;
        }
    }
}
