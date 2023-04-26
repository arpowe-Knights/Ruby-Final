using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;

    public Text score;

    public GameObject projectilePrefab;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public AudioClip throwSound;
    public AudioClip hitSound;
    public AudioClip jambiVoice;

    public ParticleSystem damageParticles;
    public ParticleSystem healthParticles;
    public ParticleSystem smokeParticles;

    int cogs = 0;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

        cogs = GameObject.FindGameObjectsWithTag("Robot").Count();
        CogCounter.Instance.SetCount(cogs);

    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (currentHealth <= 0)
        {
            horizontal = vertical = 0; // makes everything equal 0 
        }

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.R) && (currentHealth <= 0 || RobotCounter.Instance.fixedRobots >= RobotCounter.Instance.robotCount))
        {
            GameOverText.Instance.Restart();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                    PlaySound(jambiVoice);

                }
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount, bool becomeInvincible = true)
    {
        if (amount < 0)
        {
            if (isInvincible || currentHealth <= 0)
                return;

            isInvincible = becomeInvincible;
            invincibleTimer = timeInvincible;

            PlaySound(hitSound);

            damageParticles.Emit(100);
        }

        if (amount > 0)
        {
            healthParticles.Emit(100);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UiHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            GameOverText.Instance.GameLose();
        }
    }

    // public void DamageByFire(int amount)
    // {

    // }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     FireZone fz = other.GetComponent<FireZone>();

    //     if (fz != null)
    //     {
    //         other.GetComponent<FireZone>().ApplyBurn(4);
    //     }
    // }

    public void ChangeAmmo(int amount)
    {
        cogs += amount;
        CogCounter.Instance.SetCount(cogs);
    }

    void Launch()
    {
        if (cogs <= 0) return;

        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        ChangeAmmo(-1);

        PlaySound(throwSound);
    }
}
