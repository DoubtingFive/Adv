using UnityEngine;

public class LaserSpawn : MonoBehaviour
{
    [SerializeField] GameObject laser;
    private void Start()
    {
        SpawnLaser();
    }
    public void SpawnLaser()
    {
        GameObject _x = Instantiate(laser, transform.position, transform.rotation);
        _x.GetComponent<LaserContorl>().SetSpawner(this);
    }
}
