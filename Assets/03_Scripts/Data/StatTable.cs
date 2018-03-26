using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class StatData
{
	public int nID;
	public int nCategory; // eStatCategory

	public string strName;
	public string strInfo;

	public int nMaxHP;
	public int nMaxHungry; // 포만감
	public int nDashCount;

	public int nDmg; // 공격력최소
	public int nDmgMax; // 공격력최대
	public int nAtkSpd; // 공격속도
	public int nPower; // 위력

	public int nDef; // 방어력
	public int nBlock; // 방어
	public int nFixDef; // 강인함

	public int nCri; // 크리 확률
	public int nCriDmg; //크리댐지

	public int nAvoid; // 회피
	public int nMove; // 이동속도
	public int nDashAtk; // 대쉬공격시 추가댐지
	public int nMaxJump;

	public virtual void Clear()
	{
		nID = 0;
		nCategory = 0;

		strName = string.Empty;
		strInfo = string.Empty;

		ClearStat();
	}

	public void ClearStat()
	{
		nMaxHP = 0;
		nMaxHungry = 0;
		nDashCount = 0;

		nDmg = 0;
		nAtkSpd = 0;
		nPower = 0;

		nDef = 0;
		nBlock = 0;
		nFixDef = 0;

		nCri = 0;
		nCriDmg = 0;

		nAvoid = 0;
		nMove = 0;
		nDashAtk = 0;
		nMaxJump = 0;
	}

	public void Copy(StatData a_refStat)
	{
		nMaxHP		= a_refStat.nMaxHP;
		nMaxHungry	= a_refStat.nMaxHungry;
		nDashCount	= a_refStat.nDashCount;

		nDmg		= a_refStat.nDmg;
		nDmgMax		= a_refStat.nDmgMax;
		nAtkSpd		= a_refStat.nAtkSpd;
		nPower		= a_refStat.nPower;

		nDef		= a_refStat.nDef;
		nBlock		= a_refStat.nBlock;
		nFixDef		= a_refStat.nFixDef;

		nCri		= a_refStat.nCri;
		nCriDmg		= a_refStat.nCriDmg;

		nAvoid		= a_refStat.nAvoid;
		nMove		= a_refStat.nMove;
		nDashAtk	= a_refStat.nDashAtk;
		nMaxJump	= a_refStat.nMaxJump;
	}

	public void Add(StatData a_refStat)
	{
		nMaxHP		+= a_refStat.nMaxHP;
		nMaxHungry	+= a_refStat.nMaxHungry;
		nDashCount	+= a_refStat.nDashCount;

		nDmg		+= a_refStat.nDmg;
		nDmgMax		+= a_refStat.nDmgMax;
		nAtkSpd		+= a_refStat.nAtkSpd;
		nPower		+= a_refStat.nPower;

		nDef		+= a_refStat.nDef;
		nBlock		+= a_refStat.nBlock;
		nFixDef		+= a_refStat.nFixDef;

		nCri		+= a_refStat.nCri;
		nCriDmg		+= a_refStat.nCriDmg;

		nAvoid		+= a_refStat.nAvoid;
		nMove		+= a_refStat.nMove;
		nDashAtk	+= a_refStat.nDashAtk;
		nMaxJump	+= a_refStat.nMaxJump;
	}
}

public static class StatTable
{
	public const int nCategoryGap = 10000;
	public static int nDefaultWaeponID = (nCategoryGap * (int)eStatCategory.Equipment_OneHandWaepon) + 1;

	public static Dictionary<int, StatData> m_mapStat = new Dictionary<int, StatData>();

	static StatTable()
	{
		int nID = 0;
		StatData st = null;
		int nCategory = 0;

		// 영웅
		nCategory = (int)eStatCategory.Hero;
		nID = nCategoryGap * nCategory;

		st = new StatData();

		st.ClearStat();
		st.nID = ++nID; // ++nID에 주의
		st.nCategory = nCategory;
		st.strName = "모험가";
		st.strInfo = "";

		st.nMaxHP = 100;
		st.nMaxHungry = 100;
		st.nDashCount = 2;
		st.nMaxJump = 1;

		m_mapStat.Add(st.nID, st);


		// 무기
		nCategory = (int)eStatCategory.Equipment_OneHandWaepon;
		nID = nCategoryGap * nCategory;

		st = new StatData();

		st.ClearStat();
		st.nID = ++nID; // ++nID에 주의
		st.nCategory = nCategory;
		st.strName = "숏소드";
		st.strInfo = "보통 칼";

		st.nDmg = 7;
		st.nDmgMax = 9;

		m_mapStat.Add(st.nID, st);


		// 몹
		nCategory = (int)eStatCategory.Enemy;
		nID = nCategoryGap * nCategory;

		st = new StatData();

		st.ClearStat();
		st.nID = ++nID; // ++nID에 주의
		st.nCategory = nCategory;
		st.strName = "해골병사";
		st.strInfo = "해골병사";

		st.nDmg = 5;
		st.nDmgMax = 8;

		m_mapStat.Add(st.nID, st);

		// 보스
		nCategory = (int)eStatCategory.Boss;
		nID = nCategoryGap * nCategory;

		st = new StatData();

		st.ClearStat();
		st.nID = ++nID; // ++nID에 주의
		st.nCategory = nCategory;
		st.strName = "벨리얼";
		st.strInfo = "첫 보스";

		st.nDmg = 20;
		st.nDmgMax = 22;

		m_mapStat.Add(st.nID, st);
	}

	public static StatData GetData(int a_nID)
	{
		if (m_mapStat.ContainsKey(a_nID) == false)
		{
			return null;
		}

		return m_mapStat[a_nID];
	}
}
