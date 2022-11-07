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
    bool dash = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //controller.OnLandEvent.AddListener(OnLanding);
    }

    // Update is called once per frame
    void Update()
    {
        paused = menu.GameIsPaused;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && !controller.isDashing())
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetMouseButtonDown(1) && SkillContainer.Instance.isSkillReady(SkillType.Dash)) //right click
        {
            SkillContainer.Instance.useSkill(SkillType.Dash);
            dash = true;
            SoundManager.Instance.playSound(SoundManager.Instance.dash);
            animator.SetTrigger("Dash");
        }

        if(!paused)
        {
            animator.SetFloat("VSpeed", rb.velocity.y);
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, dash);
        jump = false;
        dash = false;
    }
}