using UnityEngine;

public class Crowbar : Interactable
{
    [SerializeField] LayerMask barrierMask;
    [HideInInspector] public CrowbarSpawn spawner;
    Transform cam;
    private void Start()
    {
        cam = Camera.main.transform;
    }
    public override void Use()
    {
        Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, 4f, barrierMask);
        if (hitInfo.collider)
        {
            Destroy(hitInfo.collider.gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -10 || transform.position.y > 100)
        {
            Destroy(gameObject);
            spawner.SpawnCrowbar();
        }
    }
}
