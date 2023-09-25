using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform smokeGenerationPoint;
    [SerializeField] GameObject smoke;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] ForceBarManager forceBarManager;
    [SerializeField] PlayerAnimations playerAnimations;
    [SerializeField] AudioClip hammerAudio;
    private Rigidbody2D body;
    [Header("Debug")]
    [SerializeField] float currentCooldownTime;
    [SerializeField] float currentHammerChargeTime;
    public float hammerChargePercentage;
    [Header("Stats")]
    [SerializeField] float maxHammerChargeTime;
    [SerializeField] float jumpForce;
    [SerializeField] float cameraShakePower;
    [SerializeField] float cameraShakeTime;
    [SerializeField] float CooldownTime;
    [SerializeField] bool CoolingDownHammer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void HammeringInput(InputAction.CallbackContext callback)
    {
        if (!CoolingDownHammer)
        {
            if (callback.performed)
            {
                playerMovement.hammering = true;
                playerAnimations.HammerUp();
            }
        }

        if (callback.canceled)
        {
            playerMovement.hammering = false;
            if (hammerChargePercentage > 0)
            {
                StartCoroutine(JumpAnim());
                forceBarManager.UpdateForceBar(0);
            }

            else
            {
                currentHammerChargeTime = 0;
                hammerChargePercentage = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (CoolingDownHammer)
            currentCooldownTime += Time.fixedDeltaTime;
        if (currentCooldownTime > CooldownTime)
            CoolingDownHammer = false;

        if (playerMovement.hammering)
        {
            forceBarManager.UpdateForceBar(hammerChargePercentage / 100);
            currentHammerChargeTime += Time.fixedDeltaTime;
            currentHammerChargeTime = Mathf.Clamp(currentHammerChargeTime, 0, maxHammerChargeTime);
            hammerChargePercentage = currentHammerChargeTime * (100 / maxHammerChargeTime);
        }

        //gravity changer
        if (body.velocity.y < 0)
            body.gravityScale = Mathf.Lerp(body.gravityScale, 2, 0.25f);

        else
            body.gravityScale = Mathf.Lerp(body.gravityScale, 1, 0.25f);
    }

    void JumpAttack()
    {
        AudioManager.instance.audioSource.PlayOneShot(hammerAudio, (hammerChargePercentage / 100));
        Instantiate(smoke, smokeGenerationPoint.position, smokeGenerationPoint.rotation);
        CameraShaker.instance.CameraShake((cameraShakePower * (hammerChargePercentage / 100)), (cameraShakeTime));
        body.AddForce(new Vector2(0f, jumpForce * (hammerChargePercentage / 100)), ForceMode2D.Impulse);
        currentHammerChargeTime = 0;
        hammerChargePercentage = 0;
    }
    IEnumerator JumpAnim()
    {
        CoolingDownHammer = true;
        currentCooldownTime = 0;
        playerMovement.hasJumped = true;
        playerAnimations.HammerDown();
        yield return new WaitForSeconds(0.1f);
        JumpAttack();
        while (!playerMovement.grounded)
            yield return new WaitForSeconds(0.1f);
        playerMovement.hasJumped = false;
    }
}
