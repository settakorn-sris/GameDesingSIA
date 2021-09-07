using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

public class Player : MonoBehaviour, IPlayerControlActions
{
    //public event Action OnPlayerIsDie;

    [SerializeField] private PlayerCharecter player;
    private PlayerInput playerInput;
    private Vector3 playerDiraction;

    // For Mouse Look
    
    private Vector2 mousePositionOnScreen;
 
    void Awake()
    {
        
        playerInput = new PlayerInput();
        playerInput.PlayerControl.SetCallbacks(this);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Positon();
        PlayerRotateMouseControll();
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
        Vector2 getMousePositionOnScreen = ctx.ReadValue<Vector2>();

        mousePositionOnScreen = new Vector2(getMousePositionOnScreen.x, getMousePositionOnScreen.y);

     
    }

    // For Mouse Look
    private void PlayerRotateMouseControll()
    {
       
        transform.rotation = Quaternion.Euler(new Vector3(0, mousePositionOnScreen.x, 0));
      
    }

    private Vector3 Positon()
    {
       // return playerDiraction * player.Speed * Time.deltaTime;
        return playerDiraction * 10 * Time.deltaTime;
    }

    private void OnFire()
    {
        Debug.Log("Fire");
        player.Attack();
        //get from pooling class
    }

   
    //other

   private void PlayerDie()
    {
        // implement next time
    }
}

