using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private Transform spearSpawn;

    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(isShooting) return;

        Spear spear = Instantiate(spearPrefab, spearSpawn.position, spearPrefab.transform.rotation).GetComponent<Spear>();
        spear.Initialise(this);
        isShooting = true;
    }

    public void ResetStatus()
    {
        isShooting = false;
    }
}
