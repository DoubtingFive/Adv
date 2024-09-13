using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : Interactable
{
    [SerializeField] GameObject door;
    public override void Use()
    {
        base.Use();
        door.SetActive(false);
        gameObject.SetActive(false);
    }
}
