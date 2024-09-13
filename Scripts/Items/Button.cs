using UnityEngine;

public class Button : Interactable
{
    [SerializeField] bool[] lights = new bool[4];
    [SerializeField] LightManager lightManager;

    public override void Use()
    {
        base.Use();
        lightManager.ChangeLights(lights);
    }
}
