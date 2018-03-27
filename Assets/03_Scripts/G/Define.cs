using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

namespace Global_Define
{
	// const 값, enum  -----------------------------------------------------------
	static public partial class Define
	{
		public const int nEQUIP_SWAP_COUNT = 2; // 장비 스왑	

		public static int GetCoordKey(int a_nX, int a_nY)
		{
			return ((a_nY * ST_DungeonInfo.nGap) + a_nX);
		}
	}

	static public class Path
	{
		public const string PREFAB_PATH		= "Prefab\\";
		public const string PREFAB_PATH_ADD	= "Prefab\\{0}";
	}
	
	public enum eNPC
	{
		None = -1,

		VilleageChief = 0,
		Waepon,
		SkillPoint,

		DungeonWaepon = 100,
		DungeonDining,
	}

	public enum eScene
	{
		None = -1,

		[Description("Start")]
		Start = 0,

		[Description("Town")]
		Town = 1,

		[Description("Dungeon")]
		Dungeon = 2,
	}

	public enum eEquipPos
	{
		e1stMainHand01,
		e1stSubHand01,

		eMainHand02,
		eSubHand02,

		eAccessary01,
		eAccessary02,
		eAccessary03,
		eAccessary04,
		eAccessary05,
		eAccessary06,
	}
	
	[Flags]
	public enum eDir
	{
		None = 0,

		Left = 1,
		Top = 2,
		Right = 4,
		Bottom = 8,
	}

	public enum eStatCategory
	{
		None = -1,

		Hero = 0,
		Enemy,
		Boss,

		Equipment_OneHandWaepon,
		Equipment_TwoHandWaepon,
		Equipment_SubWaepon,
		Equipment_Accessary,
	}

	public enum eRoomState
	{
		None = -1,

		First,
		Last,
		NPC,
		Boss,
	}

	// 인터페이스 -----------------------------------------------------------

	public interface IUpdate
	{
		void DoUpdate(float a_fDelta);
	}

	public interface IFixedUpdate
	{
		void DoFixedUpdate(float a_fDelta);
	}

	// 구조체성 클래스 -----------------------------------------------------------

	// Save, Load 및 기본 플레이어 데이터
	public class ST_Player
	{
		public List<eNPC>	liRescuedNPC = new List<eNPC>();
		public string		strFloor;
		public int			nMoney;
	}

	public class ST_GameStat : StatData
	{
		public int			nNowHP;
		public int			nNowHungry;
		public int			nNowDash;
		public int			nNowJumpCount;

		public override void Clear()
		{
			base.Clear();

			nNowHP = 90;
			nNowHungry = 0;
			nNowDash = 0;
			nNowJumpCount = 0;
		}

		public void Copy(ST_GameStat a_refStat)
		{
			base.Copy(a_refStat);

			nNowHP			= a_refStat.nNowHP;
			nNowHungry		= a_refStat.nNowHungry;
			nNowDash		= a_refStat.nNowDash;
			nNowJumpCount	= a_refStat.nNowJumpCount;
		}

		public void Add(ST_GameStat a_refStat)
		{
			base.Add(a_refStat);

			nNowHP			+= a_refStat.nNowHP;
			nNowHungry		+= a_refStat.nNowHungry;
			nNowDash		+= a_refStat.nNowDash;
			nNowJumpCount	+= a_refStat.nNowJumpCount;
		}
	}

	public class ST_EquipInfo // 실제 장비한 장비정보
	{
		Dictionary<eEquipPos, StatData> mapEquip = new Dictionary<eEquipPos, StatData>();
	
		StatData		stEquipStat = new StatData();
		List<StatData>	liStat = new List<StatData>();
		StatData		stAccessary = new StatData();

		public ST_EquipInfo()
		{
			for( int i=0; i<Define.nEQUIP_SWAP_COUNT; ++i )
			{
				liStat.Add(new StatData());
			}
		}

		public void Clear()
		{
			mapEquip.Clear();

			for (int i = 0; i < Define.nEQUIP_SWAP_COUNT; ++i)
			{
				liStat[i].Clear();
			}

			stAccessary.Clear();
		}

		public void SetEquip(int a_nID, int a_nSlot = -1)
		{

		}
		
		public StatData GetStat(int a_nSlot)
		{
			stEquipStat.Clear();
			
			stEquipStat.Add(stAccessary);
			stEquipStat.Add(liStat[a_nSlot]);

			return stEquipStat;
		}
	}
	
	public class ST_Room
	{
		public int nX;
		public int nY;

		public int nRoomID;
		public eRoomState eState;

		public eNPC	eExistNPC;
		public eDir	eOpenDir;
		public bool bPortal;

		public ST_Room()
		{
			Clear();
		}

		public void Clear()
		{
			nX = 0;
			nY = 0;

			nRoomID = 0;
			eState = eRoomState.None;

			eExistNPC = eNPC.None;
			eOpenDir = eDir.None;
			bPortal = false;
		}

		public bool bNoNPC
		{ 
			get
			{
				return ( (eState == eRoomState.First) ||
				(eState == eRoomState.Last) ||
				(eState == eRoomState.Boss) );
			} 
		}
		public bool bHasNPC { get { return (eExistNPC != eNPC.None); } }
		public bool bHasPortal { get { return bPortal; } }
	}

	public class ST_DungeonInfo
	{
		public const int nGap = 1000;
		
		public Dictionary<int, ST_Room> m_mapRoom = new Dictionary<int, ST_Room>();
		public List<int> m_liCoordKey = new List<int>();
		
		public int m_nFloor = 0;
		public int m_nRoomCount = 0;
		public int m_nStartRoomKey = 0;
		
		public void CreateDungeon(int a_nFloor, int a_nRoomCount, List<eNPC> a_liNPC)
		{
			Clear();
			m_nFloor = a_nFloor;
			m_nRoomCount = a_nRoomCount;
			m_nStartRoomKey = Define.GetCoordKey(0, 0);

			RoomMaker.CreateRoom(ref m_mapRoom, ref m_liCoordKey, a_nFloor, a_nRoomCount);

			m_mapRoom[m_nStartRoomKey].eState = eRoomState.First;
			m_mapRoom[m_nStartRoomKey].bPortal = true;

			// 마지막 방 설정
			var node = m_mapRoom.GetEnumerator();

			ST_Room lastRoom = null;
			while ( node.MoveNext() )
			{
				lastRoom = node.Current.Value;
			}
			
			lastRoom.eState = eRoomState.Last;
			lastRoom.bPortal = true;
			
			m_liCoordKey.Shuffle();

			int nIndex = 0;
			for( int i=0 ; i<a_liNPC.Count; ++i )
			{
				var room = m_mapRoom[m_liCoordKey[nIndex]];

				if( (room.bHasNPC == true) ||
					(room.bNoNPC == true) )
				{
					++nIndex;
					--i;

					continue;
				}

				room.eExistNPC = a_liNPC[i];
				
				// 식당, 무기상인한테는 무조건 포탈 존재
				if(room.eExistNPC == eNPC.DungeonDining || room.eExistNPC == eNPC.DungeonWaepon)
				{
					room.bPortal |= true;
				}
			} 
		}

		public void Clear()
		{
			m_mapRoom.Clear();
			m_liCoordKey.Clear();

			m_nFloor = 0;
			m_nRoomCount = 0;
			m_nStartRoomKey = 0;
		}
	}

	static public class RoomMaker
	{
		// 방생성시 필요한 인자
		private static int m_nRoomCheck = 0;
		private static Dictionary<int, ST_Room> m_refMapRoom = null;
		private static List<int> m_refLiCoord = null;

		public static void CreateRoom(ref Dictionary<int, ST_Room> a_refInfo, ref List<int> a_liCoord, int a_nFloor, int a_nRoomCount)
		{
			Clear();
			a_refInfo.Clear();
			a_liCoord.Clear();
			
			m_refMapRoom = a_refInfo;
			m_refLiCoord = a_liCoord;
			m_nRoomCheck = a_nRoomCount;

			ST_Room stRoom = AddRoom(0, 0);
			
			// 방 생성
			while( true )
			{
				RecursiveCreateRoom(ref stRoom);

				if( m_nRoomCheck <= 0 )
				{
					break;
				}
			}
			
			Clear();
		}

		public static void Clear()
		{
			m_nRoomCheck = 0;
			m_refMapRoom = null;
		}

		public static void RecursiveCreateRoom(ref ST_Room a_stRoom, eDir a_eDir = eDir.None)
		{
			if( a_eDir != eDir.None ) { a_stRoom.eOpenDir |= a_eDir; }

			// 위
			if ( (UnityEngine.Random.Range(0, 1.0f) > 0.5f) )
			{
				var room = AddRoom(a_stRoom.nX, a_stRoom.nY + 1);

				if( room != null )
				{
					a_stRoom.eOpenDir |= eDir.Top;
					RecursiveCreateRoom(ref room, eDir.Bottom);
				}
			}

			// 왼쪽
			if( (a_stRoom.nX > 0) &&
				(UnityEngine.Random.Range(0, 1.0f) > 0.5f) )
			{
				var room = AddRoom(a_stRoom.nX - 1, a_stRoom.nY);

				if (room != null)
				{
					a_stRoom.eOpenDir |= eDir.Left;
					RecursiveCreateRoom(ref room, eDir.Right);
				}
			}

			// 오른쪽
			if ((UnityEngine.Random.Range(0, 1.0f) > 0.5f) )
			{
				var room = AddRoom(a_stRoom.nX + 1, a_stRoom.nY);

				if (room != null)
				{
					a_stRoom.eOpenDir |= eDir.Right;
					RecursiveCreateRoom(ref room, eDir.Left);
				}
			}

			// 아래
			if( (a_stRoom.nY > 0) &&
				(UnityEngine.Random.Range(0, 1.0f) > 0.5f) )
			{
				var room = AddRoom(a_stRoom.nX, a_stRoom.nY - 1);

				if (room != null)
				{
					a_stRoom.eOpenDir |= eDir.Bottom;
					RecursiveCreateRoom(ref room, eDir.Top);
				}
			}
		}

		public static ST_Room AddRoom(int a_nX, int a_nY)
		{
			if( m_nRoomCheck <= 0 ) { return null; }

			ST_Room stReturn = null;

			int nKey = a_nX.GetCoordKey(a_nY);

			if (m_refMapRoom.ContainsKey(nKey) == true)
			{
				return m_refMapRoom[nKey];
			}

			stReturn = new ST_Room();
			stReturn.nX = a_nX;
			stReturn.nY = a_nY;

			// 랜덤하게 포탈 세팅. 좌표가 아닌 방의 속성을 정하는 것이기에 여기보단 위쪽에서 하는게 맞지만 반복문이 여기서 돌아 걍 여기서함
			stReturn.bPortal = (UnityEngine.Random.Range(0, 1.0f) > 0.7f);

			m_refLiCoord.Add(nKey);
			m_refMapRoom.Add(nKey, stReturn); 

			--m_nRoomCheck;

			return stReturn;
		}
	}
}
