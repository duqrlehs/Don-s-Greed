using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class GameMng : MonoBehaviour
{
	#region SINGLETON
	public static bool destroyThis = false;

	static GameMng _instance = null;

	public static GameMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(GameMng)) as GameMng;
				if (_instance == null)
				{
					_instance = new GameObject("GameMng", typeof(GameMng)).GetComponent<GameMng>();
				}
			}

			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	#endregion

	// 플레이어
	public Player m_refPlayer = null;

	// 플레이어 스탯
	public ST_GameStat m_PlayerStat = new ST_GameStat();

	// 인벤토리
	public LinkedList<int> m_liInvenItems = new LinkedList<int>();

	// 이큅 정보
	public ST_EquipInfo m_stEquip = new ST_EquipInfo();

	// 현재 선택 이큅 슬롯
	public int m_nEquipSlot = 0;

	public void EnterTown()
	{
		m_nEquipSlot = 0;
		m_PlayerStat.Clear();

		// 최초 인벤 ( 칼한자루 ), 최초 스탯 세팅
		m_liInvenItems.Clear();
		m_liInvenItems.AddLast(StatTable.nDefaultWaeponID);

		// 기본스탯 세팅
		m_PlayerStat.Copy(((int)eStatCategory.Hero+1).GetStatData());

		// 스탯 - 이큅추가
		m_stEquip.SetEquip(StatTable.nDefaultWaeponID, 0);

		// 스탯 적용
		m_refPlayer.SetStat(m_PlayerStat, m_stEquip.GetStat(m_nEquipSlot));
	}

	public void EnterDungeon()
	{
		DungeonMng.Ins.EnterDungeon();
	}
	
	public void SetPlayer(Player a_refPlayer)
	{
		m_refPlayer = a_refPlayer;
	}
}
