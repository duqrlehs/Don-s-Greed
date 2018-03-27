using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class Room : IFixedUpdate
{
	#region UI

	GameObject	m_objRoom = null;
	RoomInfo	m_refRoom = null;

	#endregion

	ST_Room m_stRoomData = null;
	int m_nX = 0;
	int m_nY = 0;
	int m_nKey = 0;

	public int nCoordKye { get { return m_nKey; } }

	public Room(int a_nMapCoordX, int a_nMapCoordY)
	{
		m_nX = a_nMapCoordX;
		m_nY = a_nMapCoordY;

		m_nKey = m_nX.GetCoordKey(m_nY);
	}

	public void SetData(RoomInfo a_refRoomInfo)
	{
		m_refRoom = a_refRoomInfo;
		m_objRoom = a_refRoomInfo.gameObject;
	}

	public void SetData(GameObject a_objRoot, ST_Room a_stRoom)
	{
		m_stRoomData = a_stRoom;
	
		string strPrefabName = string.Format("Rooms/Room_{0:000}", m_stRoomData.nRoomID);

		m_objRoom = a_objRoot.Instantiate_asChild(strPrefabName);
		m_objRoom.name = string.Format("{0}_{1}_{2}", m_objRoom.name, a_stRoom.nX, a_stRoom.nY);

		m_refRoom = m_objRoom.GetComponent<RoomInfo>();
		m_refRoom.SetData(m_stRoomData);
		
		SetVisible(false);
	}

	public void SetVisible(bool a_bVisible)
	{
		m_refRoom.SetVisible(a_bVisible);
	}

	public void DoFixedUpdate(float a_fDeltaTime)
	{
		if( m_refRoom != null )
		{
			m_refRoom.DoFixedUpdate(a_fDeltaTime);
		}
	}

	public Vector3 GetSpawnPos(eDir a_eDir)
	{
		return m_refRoom.m_arrSpawnRoot[a_eDir.ToIndex()].transform.localPosition;
	}
}
