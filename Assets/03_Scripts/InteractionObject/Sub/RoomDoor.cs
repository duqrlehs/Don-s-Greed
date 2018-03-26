using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class RoomDoor : InteractionObj
{
	#region INSPECTOR

	public eDir m_eDir;

	#endregion

	public override void Interaction(MoveObject a_obj)
	{
		DungeonMng.Ins.ChangeRoom(m_eDir);
	}
}
