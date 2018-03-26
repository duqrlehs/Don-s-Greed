using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionObj : MonoBehaviour
{
	#region INSPECTOR

	public Rigidbody2D		m_rbBody;
	public UISprite			m_sprBody;
	public BoxCollider2D	m_objCollider;

	#endregion

	public abstract void	Interaction(MoveObject a_obj);
}
