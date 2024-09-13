using UnityEngine;

public class LightManager : MonoBehaviour
{
    LightFunctions[] lightObject;
    [SerializeField] CrowbarSpawn spawner;
    float time = 2;
    float cooldown;

    private void Start()
    {
        lightObject = gameObject.GetComponentsInChildren<LightFunctions>();
        for (int i = 0; i < lightObject.Length; i++)
        {
            Debug.Log("Founded: " + lightObject[i].name);
        }
    }
    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }
    public void ChangeLights(bool[] lights)
    {
        if (cooldown <= 0)
        {
            cooldown = time;
            bool[] bools = new bool[lights.Length];
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i])
                {
                    lightObject[i].ToggleLight();
                }
                bools[i] = lightObject[i].isLight;
            }
            if (bools[0] && bools[1] && bools[2] && bools[3])
            {
                spawner.SpawnCrowbar();
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("Button"))
                {
                    Destroy(x);
                }
            }
        }
    }
}
