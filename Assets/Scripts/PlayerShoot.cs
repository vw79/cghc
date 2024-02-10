using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private Transform spearSpawn;
    [SerializeField] private GameObject trail;
    public float cooldownTime = 1f;
    [SerializeField] private Cooldown cdScript;

    private bool isShooting = false;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();

        if (Input.GetMouseButtonDown(1))
        {
            trail.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            trail.SetActive(false);
            if (cdScript.UseSpell()) 
            {
                Shoot();
            }
        }
    }

    private void UpdateDirection()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        direction.Normalize();

        // Trail looks towards the direction of the spear
        float angle;
        Vector2 frontDirection = transform.localScale.x >= 0 ? Vector2.right : Vector2.left;
        if(direction.y > 0)
        {
            angle = Vector2.Angle(Vector2.right, direction);
        }
        else
        {
            angle = 360 - Vector2.Angle(Vector2.right, direction);
        }
        if(transform.localScale.x < 0) angle = 180 + angle;
        trail.transform.rotation = Quaternion.Euler(0, 0, angle);
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
        isShooting = false;
    }
}
