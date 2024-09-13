using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using IngameDebugConsole;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;
    [SerializeField] float walkSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float offset = 0.2f;
    [SerializeField] float gravity = -9.81f;
    static float sens = 1;
    Text debug;
    CharacterController controller;
    Transform Orientation;
    Transform cam;
    Vector3 velocity;
    float airMultiplayer = 1;
    float speed,hasteSpeed =0;
    float yRotation = 0;
    float horizontal, vertical;
    bool isGrounded = true;
    bool isCrouch = false;
    bool jumpReady = true;
    bool showSpeed = true;
    bool isCancel = false;
    float cancelTime = 3;
    float cancelCool;
    private void Start()
    {
        cancelCool = cancelTime;
        DebugLogConsole.AddCommand("toggleSpeed", "toggles showing speed real-time", ToggleSpeed);
        sens = MainMenu.sens;
        debug = GameObject.FindGameObjectWithTag("debug").GetComponent<Text>();
        showSpeed = true;
        ToggleSpeed();
        speed = walkSpeed;
        Orientation = transform.Find("Orientation");
        cam = Orientation.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        float Mx = Input.GetAxisRaw("Mouse X")*sens;
        float My = -Input.GetAxisRaw("Mouse Y")*sens;

        //if (Input.GetButtonDown("Cancel"))
        //{
        //    if (Cursor.lockState == CursorLockMode.Locked) {
        //        Cursor.lockState = CursorLockMode.Confined;
        //        Cursor.visible = true; 
        //    }
        //    else
        //    {
        //        Cursor.lockState = CursorLockMode.Locked;
        //        Cursor.visible = false;
        //    } 
        //}

        Vector3 currentRotation = Orientation.rotation.eulerAngles;
        float normalizedXRotation = currentRotation.x > 180 ? currentRotation.x - 360 : currentRotation.x;
        transform.Rotate(0, Mx, 0);
        yRotation += My;
        yRotation = Mathf.Clamp(yRotation, -90, 90);
        if (yRotation < 40 && yRotation > -40)
        {
            Orientation.localRotation = Quaternion.Euler(yRotation, 0, 0);
        }
        else
        {
            cam.localRotation = Quaternion.Euler(yRotation - normalizedXRotation, 0, 0);
        }
        if (isGrounded && jumpReady)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
            else if (hasteSpeed > 0) hasteSpeed = 0;
        }
        isGrounded = Physics.CheckSphere(transform.position + Vector3.up*offset, 0.5f, groundMask);

        if (isGrounded && jumpReady)
        {
            controller.Move(Vector3.down);
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        Vector3 dir = transform.right * horizontal + transform.forward * vertical;
        if (horizontal == 0 && vertical == 0 && hasteSpeed > 0) hasteSpeed = 0;
        if (showSpeed) debug.text = "speed: " + speed + "\nhasteSpeed: " + hasteSpeed + "\nTotalSpeed: " + (speed + hasteSpeed* (isGrounded ? 1 : airMultiplayer)) + "\nisGrounded: " + isGrounded;
        controller.Move((speed + hasteSpeed) * Time.deltaTime * dir.normalized * (isGrounded?1: airMultiplayer));
        if (transform.position.y < -10)
        {
            transform.position = Vector3.zero;
            hasteSpeed = 0;
        }
        if (isCancel)
        {
            cancelCool -= Time.deltaTime;
            if (cancelCool < 0)
            {
                SceneManager.LoadScene("MainMenu");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    void Jump()
    {
        if (isCrouch) { hasteSpeed = 0; return; }
        jumpReady = false;
        hasteSpeed = (speed+hasteSpeed) * 0.5f;
        controller.Move(Vector3.up * (offset + 0.01f));
        velocity.y = Mathf.Sqrt(jumpForce * gravity * -2);
        Invoke("JumpReset", 0.5f);
    }
    void JumpReset()
    {
        jumpReady = true;
    }
    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouch = true;
            Orientation.localPosition = Vector3.zero;
            controller.height = 1;
            controller.center = new(0, -0.3f);
            speed = crouchSpeed;
            //if (isGrounded) { hasteSpeed = 0; }
            airMultiplayer = 2;
        }
        if(context.canceled)
        {
            isCrouch = false;
            Orientation.localPosition = Vector3.up * 0.75f;
            controller.height = 2;
            controller.center = new(0, 0);
            speed = walkSpeed;
            airMultiplayer = 1;
        }
    }
    public void Cancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCancel = true;
        }
        else if (context.canceled)
        {
            isCancel = false;
            cancelCool = cancelTime;
        }
    }
    void ToggleSpeed()
    {
        if (showSpeed)
        {
            debug.gameObject.SetActive(false);
            showSpeed = false;
        }
        else
        {
            debug.gameObject.SetActive(true);
            showSpeed = true;
        }
    }
    [ConsoleMethod("sens","Set sensitivity. Default is 1.")]
    public static void SetSensitivity(float _sens)
    {
        sens = _sens;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StuckDoor"))
        {
            controller.Move(transform.forward * -3);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (1 + offset));
        Gizmos.DrawSphere(transform.position + Vector3.up * offset, 0.5f);
    }
}
