using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private Transform spearSpawn;
    [SerializeField] private float cooldownTime = 1f;

    private bool isShooting = false;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
        }

        if (Input.GetMouseButtonUp(1))
        {
            direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            direction.Normalize();
            Shoot();
        }
    }

    private void Shoot()
    {
        if(isShooting) return;

        Spear spear = Instantiate(spearPrefab, spearSpawn.position, spearPrefab.transform.rotation).GetComponent<Spear>();
        spear.Initialise(this, direction);
        isShooting = true;
    }

    public void ResetStatus()
    {
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        float cooldownStart = Time.time;
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if(Time.time >= cooldownStart + cooldownTime) break;
        }
        isShooting = false;
    }
}
