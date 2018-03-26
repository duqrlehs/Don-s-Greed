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
// 			Vector3 heading = this.transform.position - a_obj.transform.position;
// 
// 			float dist = heading.magnitude;
// 			Vector3 dir = heading / dist;
// 
// 			if( ( dir.x < 0.1 && dir.x > -1.1 ) &&
// 				( dir.y < 1.1 && dir.y > 0.4 ) )
// 			{
// 				m_fpInteraction(a_obj);
// 			}

			if( a_obj.transform.localPosition.y < transform.localPosition.y )
			{
				m_fpInteraction(a_obj);
			}
		}
	}
}
