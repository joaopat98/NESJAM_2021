using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [HideInInspector] public bool IsActive;

    [SerializeField] private GameObject selectionArrow;

    public virtual void Activate()
    {
        IsActive = true;
        selectionArrow.gameObject.SetActive(IsActive);
    }

    public virtual void Deactivate()
    {
        IsActive = false;
        selectionArrow.gameObject.SetActive(IsActive);
    }

    public virtual void Press() { }
}
