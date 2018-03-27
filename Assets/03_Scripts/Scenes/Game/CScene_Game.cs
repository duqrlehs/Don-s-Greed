using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScene_Game : CScene
{
	#region INSPECTOR

	public GameObject m_objPopupRoot;
	public DungeonMap m_objMap; // 많이 보는 UI라 씬이 물고있음

	#endregion

	protected new void Awake()
	{
		base.Awake();

		GameMng.Ins.EnterDungeon();
		DungeonMng.Ins.EnterDungeon();

		m_objMap.transform.parent = Camera.main.transform;
		m_objMap.transform.localPosition = new Vector3(0, 0);
	}

	private void Start()
	{
		m_objMap.SetData(DungeonMng.Ins.m_stDungeon);
	}

	public override void CloseAllPopup()
	{
		m_objMap.SetVisible(false);
	}

	private void FixedUpdate()
	{
		if( Input.GetButtonDown("Fire3") == true ) // 왼쪽 시프트
		{
			bool bVisible = m_objMap.bIsVisible;
			bVisible = !bVisible;

			m_objMap.SetVisible(bVisible);
		}

		float fDeltaTime = Time.deltaTime;

		DungeonMng.Ins.DoFixedUpdate(fDeltaTime);
	}
}
