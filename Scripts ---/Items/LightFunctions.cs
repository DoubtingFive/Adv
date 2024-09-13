using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFunctions : MonoBehaviour
{
    [HideInInspector] public bool isLight = true;
    [SerializeField] Material onMaterial;
    [SerializeField] Material offMaterial;
    Vector3 spawnPos;
    private void Start()
    {
        isLight = true;
        spawnPos = transform.position;
        ToggleLight();
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            transform.position = spawnPos;
        }
    }
    public void ToggleLight()
    {
        if (isLight)
        { 
            GetComponent<Renderer>().material = offMaterial;
            isLight = false;
        }
        else
        {
            GetComponent<Renderer>().material = onMaterial;
            isLight = true;
        }
    }
}
