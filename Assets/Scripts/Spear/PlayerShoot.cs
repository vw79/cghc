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

    private Animator animator;
    private bool isShooting = false;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();

        if (Input.GetMouseButtonDown(1))
        {
            animator.Play("Charge");
            trail.SetActive(true);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerMovement>().StopRunningSound();
        }

        if (Input.GetMouseButtonUp(1))
        {
            animator.Play("Shoot");
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
            angle = Vector2.Angle(frontDirection, direction);
        }
        else
        {
            angle = -Vector2.Angle(frontDirection, direction);
        }
        angle = Mathf.Clamp(angle, -90, 90);
        //if(transform.localScale.x < 0) angle = 180 + angle;
        trail.transform.rotation = Quaternion.Euler(0, 0, angle);
        if(transform.localScale.x < 0) trail.transform.rotation = new Quaternion(trail.transform.rotation.x, trail.transform.rotation.y, -trail.transform.rotation.z, trail.transform.rotation.w);
    }

    private void Shoot()
    {
        if(isShooting) return;
        StartCoroutine(WaitAnim());
        
    }

    public void ResetStatus()
    {
        isShooting = false;
    }

    IEnumerator WaitAnim() 
    { 
        yield return new WaitForSeconds(0.3f);
        Spear spear = Instantiate(spearPrefab, spearSpawn.position, spearPrefab.transform.rotation).GetComponent<Spear>();
        spear.Initialise(this, direction);
        isShooting = true;
        GetComponent<PlayerMovement>().enabled = true;
    }
}
