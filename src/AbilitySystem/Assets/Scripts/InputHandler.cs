using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerSkillUse playerSkillUse;
    PlayerMovement playerMovement;
    PlayerAnimate playerAnimate;

    InputAction move;
    InputAction skill0;
    InputAction skill1;
    InputAction skill2;
    InputAction skill3;
    InputAction skill4;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerSkillUse = transform.parent.GetComponentInChildren<PlayerSkillUse>();
        playerMovement = transform.parent.GetComponentInChildren<PlayerMovement>();
        playerAnimate = transform.parent.parent.GetComponentInChildren<PlayerAnimate>();

        move = playerInput.actions.FindAction("Move");
        skill0 = playerInput.actions.FindAction("Skill0");
        skill1 = playerInput.actions.FindAction("Skill1");
        skill2 = playerInput.actions.FindAction("Skill2");
        skill3 = playerInput.actions.FindAction("Skill3");
        skill4 = playerInput.actions.FindAction("Skill4");

        move.performed += playerMovement.Move;
        move.performed += playerAnimate.Move;
        move.canceled += playerMovement.Move;
        move.canceled += playerAnimate.Move;
        skill0.performed += playerSkillUse.Skill0;
        skill1.performed += playerSkillUse.Skill1;
        skill2.performed += playerSkillUse.Skill2;
        skill3.performed += playerSkillUse.Skill3;
        skill4.performed += playerSkillUse.Skill4;
    }
}
