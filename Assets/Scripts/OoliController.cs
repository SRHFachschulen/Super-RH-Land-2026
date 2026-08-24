using UnityEngine;
using UnityEngine.InputSystem;

public class OoliController : MonoBehaviour
{
    public float speed = 5;
    public CharacterController cc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update(){
        Vector2 input = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0, input.y);
        movement *= Time.deltaTime * speed;
        cc.Move(movement);
    }
}
