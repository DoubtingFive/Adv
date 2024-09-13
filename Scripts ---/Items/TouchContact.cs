using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchContact : MonoBehaviour
{
    [SerializeField] Material onMaterial;
    [SerializeField] Material offMaterial;
    [HideInInspector] public bool isLit = false;
    private void OnTriggerEnter(Collider other)
    {
        Lit();
        GetComponentInParent<TouchContactManager>().CheckLit();
    }
    private void OnTriggerStay(Collider other)
    {
        Lit();
    }
    private void OnTriggerExit(Collider other)
    {
        isLit = false;
        GetComponent<Renderer>().material = offMaterial;
    }
    public void Lit()
    {
        GetComponent<Renderer>().material = onMaterial;
        isLit = true;
    }
}
