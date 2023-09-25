using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerJump playerJump;
    [SerializeField] BlackScreenManager blackScreenManager;
    [SerializeField] MenuManager menuManager;
    [SerializeField] AudioClip deathAudio;
    public static PlayerDeath instance { get; private set; }
    private void Awake()
    {
        instance = this;
        playerJump = GetComponent<PlayerJump>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    public IEnumerator Death()
    {
        AudioManager.instance.audioSource.PlayOneShot(deathAudio);
        playerMovement.enabled = false;
        playerJump.enabled = false;
        blackScreenManager.FadeToBlack();
        yield return new WaitForSeconds(1);
        menuManager.LaunchLevel(1);
    }
}
