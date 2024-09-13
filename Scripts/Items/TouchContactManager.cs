using UnityEngine;
using IngameDebugConsole;

public class TouchContactManager : MonoBehaviour
{
    TouchContact[] contacts;
    [SerializeField] GameObject door;
    private void Start()
    {
        DebugLogConsole.AddCommand("tsolve", "Solves Touch Contacts", TSolve);
        contacts = GetComponentsInChildren<TouchContact>();
    }
    public void CheckLit()
    {
        for (int i = 0; i < contacts.Length; i++)
        {
            if (!contacts[i].isLit) { Debug.Log(contacts[i].name + " is not lit"); return; }
        }
        Debug.Log("checked for lit");
        for (int i = 0; i < contacts.Length; i++)
        {
            Destroy(contacts[i]);
        }
        door.SetActive(false);
    }
    [ConsoleMethod("tsolve", "Solves Touch Contacts")]
    void TSolve()
    {
        for (int i = 0; i < contacts.Length; i++)
        {
            contacts[i].Lit();
            Destroy(contacts[i]);
        }
        door.SetActive(false);
    }
}
