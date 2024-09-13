using UnityEngine;
using IngameDebugConsole;

public class CubeSpawn : MonoBehaviour
{
    [SerializeField] GameObject cube;
    Transform cubeObj;
    private void Start()
    {
        SpawnCube();
    }
    public void SpawnCube()
    {
        cubeObj = Instantiate(cube, transform.position, transform.rotation).transform;
    }
    private void FixedUpdate()
    {
        if (cubeObj.position.y < - 10)
        {
            Destroy(cubeObj.gameObject);
            SpawnCube();
        }
    }
}
