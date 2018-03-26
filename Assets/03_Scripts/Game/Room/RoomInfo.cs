using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class RoomInfo : MonoBehaviour, IFixedUpdate
{
	// 네 방향
	const int nDIR_COUNT = 4;

	#region INSPECTOR
	
	public GameObject m_objRoot_Tile;
	public GameObject m_objRoot_FunctionWall;

	public GameObject[] m_arrSpawnRoot = new GameObject[nDIR_COUNT]; // 네 방향 스폰위치
	public GameObject[] m_arrEventWallRoot = new GameObject[nDIR_COUNT]; // 네 방향 이벤트 벽 루트
	public GameObject[] m_arrBlockWallRoot = new GameObject[nDIR_COUNT]; // 네 방향 막혀있는 방일 때 막는 벽 루트
	public GameObject[] m_arrTriggerRoot = new GameObject[nDIR_COUNT];  // 네 방향 트리거 루트

	public BoxCollider2D[] m_arrTrigger = new BoxCollider2D[nDIR_COUNT]; // 네 방향 트리거의 콜라이더

	public List<FunctionWall> m_liWall = new List<FunctionWall>();

	#endregion
	
	ST_Room m_stData = null;

	public void SetData(ST_Room a_stData)
	{
		m_stData = a_stData;
		CreateRoom();
		Refresh();
	}

	void CreateRoom()
	{
		// 이 아이디로 저장된 맵을 로딩 : 하는게 좋지만 일단 현재 구현은 프리팹으로 때우는거라 :D
		// m_stData.nRoomID;
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		for( int i=0; i< m_arrSpawnRoot.Length; ++i )
		{
			Gizmos.DrawCube(m_arrSpawnRoot[i].transform.position, new Vector3(0.1f,0.1f,0.1f));
		}
	}

	public void Refresh()
	{
		int nVal = (int)eDir.Top;
		eDir eCheck = eDir.None;

		for (int i = 0; i < nDIR_COUNT; ++i)
		{
			eCheck = (eDir)nVal;
			m_arrBlockWallRoot[eCheck.ToIndex()].SetActive(!m_stData.eOpenDir.AndOperation(eCheck));

			nVal >>= 1;
		}
	}
	
	public void DoFixedUpdate(float a_fDelta)
	{
		for (int i = 0; i < m_liWall.Count; ++i)
		{
			m_liWall[i].DoFixedUpdate(a_fDelta);
		}
	}

}
