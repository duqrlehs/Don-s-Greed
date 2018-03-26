using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScene_Town : CScene
{
	#region INSPECTOR

	public RoomInfo		m_RoomInfo;

	#endregion

	protected void Awake()
	{
		base.Awake();

		DungeonMng.Ins.EnterTown(m_RoomInfo);
		GameMng.Ins.EnterTown();
	}

	// 플레이어, 씬만 Update()를 돌리도록 합시다
	private void FixedUpdate()
	{
		var fDeltaTime = Time.deltaTime;

		DungeonMng.Ins.DoFixedUpdate(fDeltaTime);
	}
}
