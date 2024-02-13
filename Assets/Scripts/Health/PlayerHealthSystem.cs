using Cinemachine;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    private int lives;
    private GameObject player;
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private CinemachineImpulseSource impulseSource;

    public bool isDead;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        impulseSource = GetComponent<CinemachineImpulseSource>();     
    }

    private void Start()
    {
        lives = hearts.Length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHealth();
        }
    }

    public void TakeDamage()
    {
        if (isDead) return;

        lives--;

        animator.Play("Hurt");
        CamShake.instance.CameraShake(impulseSource);
        playerMovement.RB.velocity = new Vector2(0, playerMovement.RB.velocity.y);
        playerMovement.enabled = false;
        playerAttack.enabled = false;

        StartCoroutine(Hurt());


        if (lives < hearts.Length)
        {
            hearts[lives].enabled = false;
        }

        if (lives <= 0)
        {
            Death();
        }
    }

    public void Heal(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (lives < hearts.Length) 
            {
                hearts[lives].enabled = true; 
                lives++; 
            }
            else
            {
                break; 
            }
        }
    }

    public void ResetHealth()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        isDead = false;
        lives = hearts.Length; 

        foreach (var heart in hearts)
        {
            heart.enabled = true; 
        }
    }

    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(0.2f);
        playerMovement.enabled = true;
        playerAttack.enabled = true;
    }

    private void Death()
    {
        if (!isDead)
        {
            playerMovement.enabled = false;
            playerAttack.enabled = false;
            isDead = true;
            animator.Play("Death");
            StartCoroutine(DeathWait());
        }
    }

    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(0.97f);
        player.SetActive(false);
        GameManager.Instance.PlayerDied();
    }
    
}