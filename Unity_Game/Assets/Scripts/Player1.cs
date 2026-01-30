using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player1 : MonoBehaviour
{    
    private Vector2 moveInput;

    [SerializeField] private float speed;

    [SerializeField] CharacterController CharacterController;

    private float verticalVelocity = 0f;

    [SerializeField] private bool jumpInput;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;

    




    #region Input Handling Methods
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpInput = true;
        }
        
    }
 #endregion

    #region Unity Callbacks
    private void Update()
    {
        Vector3 moveInput3D = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 motion = moveInput3D * speed * Time.deltaTime;

        UpdatePlayerRotation(moveInput3D);
       

        if (CharacterController.isGrounded)
        {
            verticalVelocity = -3f;
            

        }
        else
        {
            verticalVelocity += Physics.gravity.y*gravityScale * Time.deltaTime;
            
        }
        if (jumpInput && CharacterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics.gravity.y * gravityScale));
            jumpInput = false;
        }

        motion.y = verticalVelocity * Time.deltaTime;

        CharacterController.Move(motion);

    }

    #endregion

  
void UpdatePlayerRotation(Vector3 moveInput)
    {
        if (moveInput.sqrMagnitude <= 0.01f)
        {
            return;
        }
        Vector3 playerRotation = transform.rotation.eulerAngles;

        playerRotation.y = GetAngleFromVector(moveInput);

        transform.rotation = Quaternion.Euler(playerRotation);
    }
    float GetAngleFromVector(Vector3 direction)
    {

        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        return rotation.eulerAngles.y;

    }
}
