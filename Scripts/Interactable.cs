using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool pickable = false;
    string itemName;
    private void Start()
    {
        itemName = gameObject.name;
    }
    virtual public void Use()
    {
        Debug.Log("Interactable.Use");
    }
}
