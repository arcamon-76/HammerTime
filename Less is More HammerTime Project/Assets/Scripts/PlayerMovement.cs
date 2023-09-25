using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D body;
    [SerializeField] PlayerAnimations playerAnimations;
    [Header("Stats")]
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] float defaultSpeed;
    [SerializeField] float hammeringSpeed;
    [SerializeField] float airTimeSpeed;
    [Header("Debug")]
    [SerializeField] bool walking;
    public           bool hammering;
    public           bool hasJumped;
    public           bool grounded;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void OnMoveInput(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            walking = true;
            if (!hammering && grounded && !hasJumped)
            {
                playerAnimations.Walk();
            }

        }
        if (callback.canceled)
        {
            if (!hammering)
            {
                playerAnimations.Idle();
            }
            walking = false;
            
        }
        direction.x = callback.ReadValue<float>();
    }

    void FixedUpdate()
    {   
        grounded = Physics2D.Raycast(transform.position + new Vector3(0,-0.7f,0),Vector2.down,0.2f);

        if (!grounded)
            speed = airTimeSpeed;
        else if (hammering)
            speed = hammeringSpeed;
        else
            speed = defaultSpeed;

        body.velocity = new Vector2(direction.x * (speed) / Time.fixedDeltaTime, body.velocity.y);
    }
}