using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;


public class FloorData
{
	public FloorData(int a_nFloor, int a_nRoomCount, bool a_bWaepon, bool a_bDining, int a_nBossID, int a_nNPC)
	{
		nID = a_nFloor;
		nRoomCount = a_nRoomCount;

		bHasWaeponRoom = a_bWaepon;
		bHasDiningRoom = a_bDining;

		nBossID = a_nBossID;

		nNPC = a_nNPC;
	}

	public int nID; // 층수와 동일
	public int nRoomCount;
	
	public bool bHasWaeponRoom;
	public bool bHasDiningRoom;
	public int nBossID;
	
	public int nNPC;
}

public static class FloorTable
{
	static Dictionary<int, FloorData> m_mapFloor = new Dictionary<int, FloorData>();

	static FloorTable()
	{
		FloorData data = null;

		data = new FloorData(1, 15, true, true, 0, (int)eNPC.Waepon);
		m_mapFloor.Add(data.nID, data);
		
		data = new FloorData(2, 12, true, true, 0, (int)eNPC.SkillPoint);
		m_mapFloor.Add(data.nID, data);

		data = new FloorData(3, 3, false, false, (StatTable.nCategoryGap * (int)eStatCategory.Boss) + 1, (int)eNPC.None);
		m_mapFloor.Add(data.nID, data);
	}

	public static FloorData GetData(int a_nFloor)
	{
		if (m_mapFloor.ContainsKey(a_nFloor) == false)
		{
			return null;
		}

		return m_mapFloor[a_nFloor];
	}
}