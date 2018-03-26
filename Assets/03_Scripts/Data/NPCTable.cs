using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class NPCData
{
	public NPCData(int a_nID, string a_strName)
	{
		nID = a_nID;
		strName = a_strName;
	}

	public int		nID;
	public string	strName;
}

public static class NPCTable
{
	static Dictionary<eNPC, NPCData> m_mapNPC = new Dictionary<eNPC, NPCData>();

	static NPCTable()
	{
		m_mapNPC.Add(eNPC.VilleageChief,	new NPCData((int)eNPC.VilleageChief, "촌장"));
		m_mapNPC.Add(eNPC.Waepon,			new NPCData((int)eNPC.Waepon, "무기판매원"));
		m_mapNPC.Add(eNPC.SkillPoint,		new NPCData((int)eNPC.SkillPoint, "스킬"));
	}

	public static NPCData GetData(eNPC a_eNPC)
	{
		if( m_mapNPC.ContainsKey(a_eNPC) == false )
		{
			return null;
		}

		return m_mapNPC[a_eNPC];
	}
}
