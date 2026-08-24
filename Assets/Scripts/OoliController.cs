using UnityEngine;
using UnityEngine.InputSystem;

public class OoliController : MonoBehaviour{
    public float speed = 5;
        
    public CharacterController cc;
    
    public float verticalVelocity = 0;
    public float gravity = 9.81f;
    public float jumpPower = 20;

    [SerializeField] private Checkpoint lastCheckpoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update(){
        // Horizontal Movement (Absolute)
        Vector2 input = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0, input.y);
        movement *= Time.deltaTime * speed;

        if (input.sqrMagnitude > 0){
            transform.forward = movement;
        }
        
        // Vertical Movement (Quasi Simulation)
        if (cc.isGrounded && verticalVelocity < 0){
            verticalVelocity = 0;
        }
        
        verticalVelocity -= gravity * Time.deltaTime;
        
        bool jumpPressed = InputSystem.actions.FindAction("Jump").WasPressedThisFrame();
        if (jumpPressed && cc.isGrounded){
            verticalVelocity += jumpPower;
        }

        movement.y = verticalVelocity;
        
        cc.Move(movement);

        if (transform.position.y < -10){
            Respawn();
        }
    }

    public void SetCheckpoint(Checkpoint newCheckpoint){
        if (lastCheckpoint == newCheckpoint) return;
        lastCheckpoint = newCheckpoint;
        // And maybe some animations and stuff
    }

    public void Respawn(){
        transform.position = lastCheckpoint.RespawnPoint.position;
        verticalVelocity = 0;
        
    }
}
