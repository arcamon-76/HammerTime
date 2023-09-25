using System.Collections;
using UnityEngine;

public class EnemyBeheviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Collider2D enemyCollider2D;
    [SerializeField] Collider2D damageZone;
    [SerializeField] Rigidbody2D body;
    [SerializeField] Animator anim;
    [SerializeField] ScoreBarManager scoreBarManager;
    [SerializeField] AudioClip deathAudio;
    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] int pointsGivenOnDeath;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        enemyCollider2D = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(speed / Time.fixedDeltaTime, body.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyWall"))
        {
            UTurn();
        }
        else if (other.CompareTag("Death"))
        {
            StartCoroutine(OnDeath());
        }
        else if (other.CompareTag("Player"))
            StartCoroutine(PlayerDeath.instance.Death());
            
           
    }

    private void UTurn()
    {
        if (transform.rotation.eulerAngles.y == 0f)
        {
            speed = -speed;
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            speed = -speed;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private IEnumerator OnDeath()
    {
        AudioManager.instance.audioSource.PlayOneShot(deathAudio);
        
        enemyCollider2D.enabled = false;
        damageZone.enabled = false;
        anim.Play("Enemy Death");
        scoreBarManager.ScoreUpdate(pointsGivenOnDeath);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
