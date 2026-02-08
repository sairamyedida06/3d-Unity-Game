using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player1 : MonoBehaviour
{

    [Header("Chngelbe Values")]
    [SerializeField] private float speed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpHeight;

    private Vector2 moveInput;
    private bool jumpInput;

    bool wasGrounded;

    [Header("Component References")]
    [SerializeField] CharacterController CharacterController;
    [SerializeField] Animator Animator;

    private float verticalVelocity = 0f;
    bool InputEnabled = true;

    [Header("Unity Events")]
    public UnityEvent Jumped;
    public UnityEvent Landed;
    public UnityEvent coinCollected;

    
    public bool Grounded
    {
        get
        {
            return CharacterController.isGrounded;
        }
    }



    #region Input Handling Methods
    public void OnMove(InputAction.CallbackContext context)
    {
        if (InputEnabled == false)
        {   
            moveInput = Vector2.zero;
            return;
        }
        moveInput = context.ReadValue<Vector2>();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (InputEnabled == false)
        {
            jumpInput = false;
            return;
        }
        if (context.performed)
        {
            jumpInput = true;
        }

    }
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        UpdateMovement();
        UpdateAnimator();
    }

    #endregion

    #region CharacetrControlMethods
    void UpdateMovement()
    {
        Vector3 moveInput3D = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 motion = moveInput3D * speed * Time.deltaTime;

        UpdatePlayerRotation(moveInput3D);

        if (Grounded)
        {
            verticalVelocity = -3f;



        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;

        }
        if (jumpInput && Grounded)
        {
            verticalVelocity = Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics.gravity.y * gravityScale));
            jumpInput = false;

            Jumped.Invoke();

        }

        motion.y = verticalVelocity * Time.deltaTime;


        CharacterController.Move(motion);

        if (!wasGrounded && Grounded)
        {
            Landed.Invoke();
        }

        wasGrounded = Grounded;

    }
        void UpdatePlayerRotation(Vector3 moveInput)
        {
            if (moveInput.sqrMagnitude <= 0.01f)
            {
                return;
            }
            Vector3 playerRotation = transform.rotation.eulerAngles;

            playerRotation.y = GetAngleFromVector(moveInput);

            Quaternion targetRotation = Quaternion.Euler(playerRotation);

            

            float maxDegrees = turnSpeed * Time.deltaTime;

            transform.rotation =Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegrees);

        }
        float GetAngleFromVector(Vector3 direction)
        {
            

            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            

            return rotation.eulerAngles.y;

        }
    

    #endregion

    #region Animator
     void UpdateAnimator()
    {
        bool jump = false;
        bool fall = false;

        if (CharacterController.isGrounded)
        {
            jump = false;
            fall = false;
        }
        else
        {
            if(verticalVelocity >= 0f)
            {
                jump = true;
            }
            else
            {
                fall = true;
            }
        }

        Vector3 velocity = CharacterController.velocity;
        velocity.y = 0f;
        float speed = velocity.magnitude;

        Animator.SetFloat("Speed", speed);
        Animator.SetBool("Jump", jump);
        Animator.SetBool("fall", fall);

    }

    public void OnDeath()
    {
        Animator.SetBool("Alive", false);
        InputEnabled = false;
    }

 
    #endregion

























}
