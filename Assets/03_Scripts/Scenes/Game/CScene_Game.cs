using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScene_Game : CScene
{
	#region INSPECTOR

	public GameObject m_objPopupRoot;
	public DungeonMap m_objMap;

	#endregion

	protected void Awake()
	{
		base.Awake();
		DungeonMng.Ins.EnterDungeon();
	}

	private void Start()
	{
		m_objMap.SetData(DungeonMng.Ins.m_stDungeon);
	}

	private void FixedUpdate()
	{
		if( Input.GetButtonDown("Fire3") == true )
		{
			bool bVisible = m_objMap.bIsVisible;
			bVisible = !bVisible;

			m_objMap.SetVisible(bVisible);
		}

		float fDeltaTime = Time.deltaTime;

		DungeonMng.Ins.DoFixedUpdate(fDeltaTime);
	}
}
