using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MoveObject
{
	private void Update_InTown()
	{
		
	}

	private void FixedUpdate_InTown()
	{
		if (Input.GetAxis("Vertical") < 0 && Input.GetButton("Jump"))
		{
			var pos = transform.localPosition;
			pos.y -= 20;
			transform.localPosition = pos;
			// 
			// 			m_rb.velocity = new Vector2(m_rb.velocity.x, 0);
			// 			m_rb.AddForce(new Vector2(0, -10.0f));


		}
		else if (Input.GetButtonDown("Jump") == true)
		{
			if( m_refStat.nNowJumpCount < m_refStat.nMaxJump )
			{
				m_rb.velocity = new Vector2(m_rb.velocity.x, 0);
				m_rb.AddForce(new Vector2(0, m_refStat.fJumpForce));

				// 				var pos = transform.localPosition;
				// 				pos.y += 15.0f;
				// 				transform.localPosition = pos;

				// ++m_refStat.nNowJumpCount;
			}

			// m_rb.isKinematic = true;
		}
		else
		{
			// m_rb.isKinematic = false;
		}

		bool bGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
		if( bGround == true )
		{
			m_refStat.nNowJumpCount = 0;

			Debug.Log("ground ground");
		}

		m_bGrounded = bGround;

		float f = Input.GetAxis("Horizontal");
		f /= 2;

		if ((f > 0 && m_bRight == false) || (f < 0 && m_bRight == true))
		{
			m_bRight = !m_bRight;

			m_spr.flip = (m_bRight) ? UIBasicSprite.Flip.Nothing : UIBasicSprite.Flip.Horizontally;
		}

		m_rb.velocity = new Vector2(f * m_refStat.fMove, m_rb.velocity.y);
	}

	private void OnTriggerEnter2D_InTown(Collider2D collision)
	{
		InteractionObj obj = collision.gameObject.GetComponent<InteractionObj>();

		if (obj == null)
		{
			return;
		}

		obj.Interaction(this);
	}

	private void OnCollisionEnter2D_InTown(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("NPCBox") == true)
		{
			NPCBox a_refBox = collision.gameObject.GetComponent<NPCBox>();

			if (a_refBox == null)
			{
				Debug.LogError("logic error - check Collider Tag");
				return;
			}

			a_refBox.Interaction(this);
		}
	}



	
}
