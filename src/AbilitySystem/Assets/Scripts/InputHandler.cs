using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerAbilityUse playerAbilityUse;
    PlayerMovement playerMovement;
    PlayerAnimate playerAnimate;

    InputAction move;
    InputAction ability0;
    InputAction ability1;
    InputAction ability2;
    InputAction ability3;
    InputAction ability4;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAbilityUse = transform.parent.GetComponentInChildren<PlayerAbilityUse>();
        playerMovement = transform.parent.GetComponentInChildren<PlayerMovement>();
        playerAnimate = transform.parent.parent.GetComponentInChildren<PlayerAnimate>();

        move = playerInput.actions.FindAction("Move");
        ability0 = playerInput.actions.FindAction("Ability0");
        ability1 = playerInput.actions.FindAction("Ability1");
        ability2 = playerInput.actions.FindAction("Ability2");
        ability3 = playerInput.actions.FindAction("Ability3");
        ability4 = playerInput.actions.FindAction("Ability4");

        move.performed += playerMovement.Move;
        move.performed += playerAnimate.Move;
        move.canceled += playerMovement.Move;
        move.canceled += playerAnimate.Move;
        ability0.performed += playerAbilityUse.Ability0;
        ability1.performed += playerAbilityUse.Ability1;
        ability2.performed += playerAbilityUse.Ability2;
        ability3.performed += playerAbilityUse.Ability3;
        ability4.performed += playerAbilityUse.Ability4;
    }
}
