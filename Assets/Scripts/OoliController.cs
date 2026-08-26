using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OoliController : MonoBehaviour{
    public float speed = 5;
        
    public CharacterController cc;
    public Animator animator;
    
    public float verticalVelocity = 0;
    public float gravity = 0.4f;
    public float jumpPower = 0.1250f;
    
    public float deathPlaneY = -10;

    [SerializeField] private Checkpoint lastCheckpoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        // Horizontal Movement (Absolute)
        Vector2 input = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0, input.y);
        // rotate around up axis to match camera rotation
        movement = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movement;
        movement *= Time.deltaTime * speed;

        if (input.sqrMagnitude > 0){
            transform.forward = movement;
        }
        
        // Vertical Movement (Quasi Simulation)
        if (cc.isGrounded && verticalVelocity < 0){
            verticalVelocity = 0;
            animator.Play("idle");
        }
        
        verticalVelocity -= gravity * Time.deltaTime;
        
        bool jumpPressed = InputSystem.actions.FindAction("Jump").WasPressedThisFrame();
        if (jumpPressed && cc.isGrounded){
            verticalVelocity += jumpPower;
            animator.Play("jump");
        }

        movement.y = verticalVelocity;
        
        cc.Move(movement);

        if (transform.position.y < deathPlaneY){
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
        Physics.SyncTransforms();
    }

    private void OnDrawGizmos(){
        Gizmos.color= new Color(1, 0, 0, 0.5f);
        Vector3 refPos = Camera.current.transform.position + Camera.current.transform.forward * 10;
        Gizmos.DrawCube(new Vector3(refPos.x, deathPlaneY, refPos.z), new Vector3(20, 0.1f, 20));
    }
}
