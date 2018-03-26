using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class DungeonMap : MonoBehaviour
{
	#region INSPECTOR

	public List<UI_Room> m_liRoom;

	#endregion

	ST_DungeonInfo m_refData = null;

	public bool bIsVisible { get { return gameObject.activeSelf; } }

	public void SetVisible(bool a_bVisible)
	{
		if( gameObject.activeSelf != a_bVisible )
		{
			gameObject.SetActive(a_bVisible);
		}

		if( a_bVisible == true )
		{
			Refresh();
		}
	}

	public void SetData(ST_DungeonInfo a_refData)
	{
		for( int i=0; i<m_liRoom.Count; ++i )
		{
			m_liRoom[i].gameObject.SetActive(false);
		}

		m_refData = a_refData;


		var node = m_refData.m_mapRoom.GetEnumerator();

		if (m_liRoom.Count < m_refData.m_mapRoom.Count)
		{
			Debug.LogError("set more room in INSPECTOR");
			return;
		}

		int nIndex = 0;

		while (node.MoveNext())
		{
			var stRoom = node.Current.Value;

			m_liRoom[nIndex].SetData(stRoom, -500, -500);
			m_liRoom[nIndex].gameObject.SetActive(true);

			++nIndex;
		}
		
		Refresh();
	}

	void Refresh()
	{
		for( int i=0; i<m_liRoom.Count; ++i )
		{
			m_liRoom[i].SetMyMark();
		}
	}
}
