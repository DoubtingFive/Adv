using UnityEngine;

public class CubeButtonManager : MonoBehaviour
{
    CubeButton[] cButs;
    bool isFourth = false;
    [SerializeField] GameObject door;
    private void Start()
    {
        cButs = GetComponentsInChildren<CubeButton>();
        for (int i = 0; i < cButs.Length; i++)
        {
            Debug.Log("Founded "+i+": " + cButs[i].name);
        }
        cButs[4].gameObject.SetActive(false);
        cButs[5].gameObject.SetActive(false);
    }
    public void CheckLit()
    {
        if (isFourth)
        {
            for (int i = 0; i < cButs.Length; i++)
            {
                if (!cButs[i].IsLit()) return;
            }
            door.SetActive(false);
        } else
        {
            for (int i = 0; i < cButs.Length - 2; i++)
            {
                if (!cButs[i].IsLit()) return;
            }
            cButs[4].gameObject.SetActive(true);
            cButs[5].gameObject.SetActive(true);
            isFourth = true;
        }
    }
}
