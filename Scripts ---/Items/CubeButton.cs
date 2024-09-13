using UnityEngine;

public class CubeButton : MonoBehaviour
{
    [SerializeField] Material lit;
    [SerializeField] Material mat;
    bool isLit = false;
    private void OnCollisionEnter(Collision collision)
    {
        isLit = true;
        GetComponentInParent<CubeButtonManager>().CheckLit();
    }
    private void OnCollisionStay(Collision collision)
    {
        GetComponent<Renderer>().material = lit;
        isLit = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Renderer>().material = mat;
        isLit = false;
    }
    public bool IsLit()
    {
        return isLit;
    }
}
