using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CoinManager : MonoBehaviour
{
    ParticleSystem particle;
    [SerializeField] ScoreBarManager scoreBarManager;
    [SerializeField] Light2D coinLight;
    [SerializeField] int value;
    [SerializeField] float multiplier;
    [SerializeField] bool caught;
    [SerializeField] AudioClip coinAudio;
    private void Awake()
    {
        coinLight = GetComponent<Light2D>();
       particle = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.audioSource.PlayOneShot(coinAudio);
            scoreBarManager.ScoreUpdate(value);
            StartCoroutine(DesapearSequence());
            GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator DesapearSequence()
    {

        coinLight.pointLightOuterRadius -= 0.1f;
        yield return new WaitForSeconds(0.01f);
        if (coinLight.pointLightOuterRadius < 0.1)
        {
            particle.Play(true);
            
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            coinLight.enabled = false;
            yield return new WaitForSeconds(1f);
            
        }
        else
            StartCoroutine(DesapearSequence());
    }
    private void Update()
    {
        transform.position += new Vector3(0, Mathf.Cos(Time.realtimeSinceStartup + transform.position.x) / multiplier, 0);
    }
}
