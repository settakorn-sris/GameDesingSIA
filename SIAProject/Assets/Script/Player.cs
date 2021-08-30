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
    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.PlayerControl.SetCallbacks(this);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Positon();
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();

        playerDiraction = new Vector3(direction.x, 0, direction.y);
    }

    private void OnFire()
    {

    }





    private Vector3 Positon()
    {
        return playerDiraction * speed * Time.deltaTime;
    }



    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
}

