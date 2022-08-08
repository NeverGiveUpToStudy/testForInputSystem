using UnityEngine;
using UnityEngine.InputSystem;

public class RoleControl : MonoBehaviour {

    private Rigidbody rb;
    public float jumpForce = 8;

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update() {

    }


    public void Jump(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            Debug.Log("��ԾStarted");
        } else if (context.phase == InputActionPhase.Waiting) {
            Debug.Log("��ԾWaiting");
        } else if (context.phase == InputActionPhase.Performed) {
            Debug.Log("��ԾPerformed");

            rb.velocity += Vector3.up * jumpForce;

        } else if (context.phase == InputActionPhase.Canceled) {
            Debug.Log("��ԾCanceled");
        } else if (context.phase == InputActionPhase.Disabled) {
            Debug.Log("��ԾDisabled");
        }
    }
}
