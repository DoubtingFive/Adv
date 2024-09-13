using UnityEngine;
using IngameDebugConsole;

public class CrowbarSpawn : MonoBehaviour
{
    [SerializeField] GameObject crowbar;
    private void Start()
    {
        DebugLogConsole.AddCommand("crowbar", "Spawns crowbar", SpawnCrowbar);
    }
    public void SpawnCrowbar()
    {
        GameObject _x = Instantiate(crowbar,transform.position,transform.rotation);
        _x.GetComponent<Crowbar>().spawner = this;
    }
}
