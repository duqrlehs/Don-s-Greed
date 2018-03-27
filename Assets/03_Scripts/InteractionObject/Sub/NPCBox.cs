using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBox : InteractionObj
{
	System.Action<MoveObject> m_fpInteraction = null;
	
	public void SetInteraction(System.Action<MoveObject> a_fpCallback)
	{
		m_fpInteraction = a_fpCallback;
	}

	public override void Interaction(MoveObject a_obj)
	{
		if(m_fpInteraction != null && a_obj != null)
		{
			if( a_obj.transform.localPosition.y < transform.localPosition.y )
			{
				m_fpInteraction(a_obj);
			}
		}
	}
}
