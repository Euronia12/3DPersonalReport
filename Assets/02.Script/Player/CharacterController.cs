using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float boostSpeed = 1f;
    private Vector2 curMovementInput;
    public float jumptForce;
    public LayerMask groundLayerMask;
    public bool IsRun = false;
    public Vector3 beforeDir;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private new Rigidbody rigidbody;
    private Animator animator;
    private CharacterCondition condition;

    public Action inventory;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        condition = GetComponent<CharacterCondition>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (IsRun)
        {
            if(condition.mana.curValue > 1f)
                condition.mana.Subtract(condition.mana.passiveValue * 5f * Time.deltaTime);
            else
            {
                IsRun = false;
                animator.SetBool("Run", false);
                boostSpeed = 1f;
            }
        }        
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }





    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("Walk", true);
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            animator.SetBool("Walk", false);
            curMovementInput = Vector2.zero;
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed * boostSpeed;
        dir.y = rigidbody.velocity.y;

        if (dir != Vector3.zero)
        {
            rigidbody.velocity = dir;
            beforeDir = dir;
        }
        else
        {
            if (dir != beforeDir)
            {
                rigidbody.velocity = dir;
                beforeDir = dir;
            }
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && condition.stamina.curValue > condition.stamina.maxValue * 0.1f)
        {
            condition.stamina.Subtract(condition.stamina.maxValue * 0.1f);
            animator.SetTrigger("Jump");
            rigidbody.AddForce(Vector2.up * jumptForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed) 
        {
            if (condition.mana.curValue > 0f)
            {
                IsRun = true;                
                animator.SetBool("Run", true);
                boostSpeed = 2f;
            }
        }
        else if (context.canceled)
        {
            IsRun = false;
            animator.SetBool("Run", false);
            boostSpeed = 1f;
        }
    }

    public void OnInteratction(InputAction.CallbackContext context)
    {

    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

}
