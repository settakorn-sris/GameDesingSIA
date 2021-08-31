using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

public class Player : MonoBehaviour, IPlayerControlActions
{

    [SerializeField]private float speed = 10;
   
  
    private PlayerInput playerInput;
    private Vector3 playerDiraction;
    
    float rotationSpeed = 20;

    // For Mouse Look
    private Camera main;
    private Vector2 mousePositionOnScreen;
    float angle;

    
    void Awake()
    {
        
        playerInput = new PlayerInput();
        playerInput.PlayerControl.SetCallbacks(this);

    }

    // Update is called once per frame
    void Update()
    {
        main = Camera.main;
        if (playerDiraction == Vector3.zero) return;
        transform.position += Positon();
        transform.rotation = Rotatetion();


        //transform.rotation  = Rotate(); // For Mouse Look
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();

        playerDiraction = new Vector3(direction.x, 0, direction.y);
    }
    public void OnMousePosition(InputAction.CallbackContext ctx)
    {
        //Vector2 getMousePositionOnScreen = ctx.ReadValue<Vector2>();
        
        //mousePositionOnScreen = new Vector2(getMousePositionOnScreen.x,getMousePositionOnScreen.y);
     
    }


    // For Mouse Look

    //private Quaternion Rotate()
    //{
    //    Vector3 mouseWorldPosition = main.ScreenToViewportPoint(mousePositionOnScreen);
    //    Vector3 target = mouseWorldPosition - transform.position;
    //    angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
    //    return Quaternion.Euler(new Vector3(0, angle, 0));
    //}

    private Vector3 Positon()
    {
        return playerDiraction * speed * Time.deltaTime;
    }

    private Quaternion Rotatetion()
    {
        return Quaternion.LookRotation(Rotate());
    }
    private Vector3 Rotate()
    {
        return Vector3.RotateTowards(transform.forward, playerDiraction, rotationSpeed * Time.deltaTime, 0);
    }
   
    private void OnFire()
    {
        Debug.Log("Fire");
        //get from pooling class
    }

   
  
}

