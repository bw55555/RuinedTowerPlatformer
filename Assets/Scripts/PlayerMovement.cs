using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public PauseMenu menu;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    public Animator animator;
    bool paused;
    bool jump = false;

        // Update is called once per frame
        void Update()
    {
        paused = menu.GameIsPaused;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if(!paused)
        {
            
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    public void OnLanding ()
    {
        animator.SetBool("isJumping", false);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}