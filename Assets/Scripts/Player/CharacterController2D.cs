using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] private float m_DoubleJumpExtraVelocity = 25f;				// Added to allow "air jumps"
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching


	[Header("Dash")]
	[SerializeField] private float m_DashForce = 20f;
	[SerializeField] private float m_DashTime = 0.5f;
	public GameObject dashEffect;

	private float dashTime = 0.0f;
	private float prevVelocityY = 0f;
	private float dashDirection = 0f;

	private float inAirTime = 0.0f;
	private float onGroundTime = 0.0f;

	const float k_GroundedRadius = .4f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;
	bool doubleJumped = false;
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		/*
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}*/

		if (m_Rigidbody2D.velocity.y > -0.01 && m_Rigidbody2D.velocity.y < 0.01)
		{
			onGroundTime += Time.deltaTime;
			if (onGroundTime > 0.02f)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
		else
        {
			onGroundTime = 0.0f;
        }

		if (m_Grounded)
        {
			inAirTime = 0;
			doubleJumped = false;
        } else {
			inAirTime += Time.deltaTime;
        }
	}

	public bool isDashing()
    {
		return dashTime > 0;
    }

	public void Move(float move, bool crouch, bool jump, bool dash)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		
		if (dashTime > 0)
        {
			dashTime -= Time.deltaTime;
			m_Rigidbody2D.velocity = new Vector2(m_DashForce * dashDirection, 0);
			if (dashTime <= 0)
            {
				m_Rigidbody2D.velocity = new Vector2(m_DashForce * dashDirection, prevVelocityY);
            }
        } 

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...

		if (jump)
		{
			if(m_Grounded)
            {
                SoundManager.Instance.playSound(SoundManager.Instance.player_jump);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			}
			else if (!doubleJumped && SkillContainer.Instance.isSkillReady(SkillType.DoubleJump))
            {
				SkillContainer.Instance.useSkill(SkillType.DoubleJump);
				//m_Grounded = false;
				if (m_Rigidbody2D.velocity.y > -25.0f && m_Rigidbody2D.velocity.y < 0f)
				{
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
				}
				else if (m_Rigidbody2D.velocity.y < -25.0f)
				{
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y + 25.0f);
				}
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                SoundManager.Instance.playSound(SoundManager.Instance.player_jump);
                doubleJumped = true;
			}
		}

		if (dash)
        {
			prevVelocityY = m_Rigidbody2D.velocity.y;
			dashDirection = m_FacingRight ? 1 : -1;
			dashTime = m_DashTime;
			m_Rigidbody2D.velocity = new Vector2(m_DashForce, 0);
			Instantiate(dashEffect, transform.position, Quaternion.identity);
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
