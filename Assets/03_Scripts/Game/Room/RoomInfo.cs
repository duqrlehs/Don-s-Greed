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
		Refresh();
	}

	public void OnDrawGizmos() // 플레이어 스폰위치 씬뷰에서 보이기
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

			int nIndex = eCheck.ToIndex();

			// 이벤트 벽 : 특정 트리거 발동시 막아두는 벽
			m_arrEventWallRoot[nIndex].SetActive(false);

			// 막는 벽
			m_arrBlockWallRoot[nIndex].SetActive(!m_stData.eOpenDir.AndOperation(eCheck));

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
