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
    
    private Vector3 playerLookPosition;
 
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
    public void OnPlayerLook(InputAction.CallbackContext ctx)
    {
        Vector2 getPositionfForPlayerLook = ctx.ReadValue<Vector2>();
        playerLookPosition = new Vector3(getPositionfForPlayerLook.x,0, getPositionfForPlayerLook.y);
        player.Attack();

    }

    // For Mouse Look
    private void PlayerRotateMouseControll()
    {
        Vector3 lookAtPosition = transform.position + playerLookPosition;
        transform.LookAt(lookAtPosition); 
    }

    private Vector3 Positon()
    {
       // return playerDiraction * player.Speed * Time.deltaTime;
        return playerDiraction * 10 * Time.deltaTime;
    }


    void IPlayerControlActions.OnFire(InputAction.CallbackContext ctx)
    {
        player.Attack();
        //get from pooling clas
    }


}

