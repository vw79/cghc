using Cinemachine;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(0.97f);
        player.SetActive(false);
    }

    public void TakeDamage()
    {
        if (isDead || lives <= 0) return;
        
        //UI
        lives--;
        if (lives >= 0 && lives < hearts.Length)
        {
            hearts[lives].enabled = false; 
        }

        // Hurt Effect
        if (!isDead)
        {
            animator.Play("Hurt");
            CamShake.instance.CameraShake(impulseSource);
            playerMovement.enabled = false;
            playerAttack.enabled = false;
            StartCoroutine(Hurt());
        }

        // Death
        if (lives <= 0 && !isDead)
        {
            isDead = true;
            animator.Play("Death");
            StartCoroutine(DeathWait());
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
}