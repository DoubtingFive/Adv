using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserContorl : Interactable
{
    //[SerializeField] LayerMask touchContact;
    LineRenderer lineRenderer;
    LaserSpawn spawner;
    float reach = 20;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.forward * reach);
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -10 || transform.position.y > 100)
        {
            Destroy(gameObject);
            spawner.SpawnLaser();
        }
    }
    public void SetSpawner(LaserSpawn _spawner)
    {
        spawner = _spawner;
    }
    public override void Use()
    {
        base.Use();
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out RaycastHit hit, 4);
        if (hit.collider)
        {
            Inventory player = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            player.ItemUnlock();
            transform.position = hit.point + Vector3.up*0.5f;
            transform.rotation = player.transform.rotation;
        }
    }
}
