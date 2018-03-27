using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class DungeonMng : MonoBehaviour, IFixedUpdate
{
	#region SINGLETON

	public static bool destroyThis = false;

	static DungeonMng _instance = null;

	public static DungeonMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(DungeonMng)) as DungeonMng;
				if (_instance == null)
				{
					_instance = new GameObject("DungeonMng", typeof(DungeonMng)).GetComponent<DungeonMng>();

					GameObject root = _instance.gameObject.AddChild(true);
					root.name = "RoomRoot";

					_instance.m_objRoomRoot = root;
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

	public int m_nMapCoordX = 0;
	public int m_nMapCoordY = 0;

	public GameObject m_objRoomRoot = null;

	// 현재 던전정보
	public ST_DungeonInfo m_stDungeon = new ST_DungeonInfo();

	// 현재 방 정보
	public Dictionary<int, Room> m_mapRoom = new Dictionary<int, Room>();
	public Room m_refActiveRoom = null;

	// IFixedUpdate
	public void DoFixedUpdate(float a_fDeltaTime)
	{
		if(m_refActiveRoom != null )
		{
			m_refActiveRoom.DoFixedUpdate(a_fDeltaTime);
		}			
	}

	public void Clear()
	{
		m_stDungeon.Clear();
		m_mapRoom.Clear();
		m_refActiveRoom = null;
	}
	
	public void EnterTown(RoomInfo m_refInfo)
	{
		Clear();

		Room townRoom = new Room(0, 0);
		townRoom.SetData(m_refInfo);

		m_refActiveRoom = townRoom;

		GameMng.Ins.m_refPlayer.transform.localPosition = m_refActiveRoom.GetSpawnPos(eDir.Left);
	}

	public void EnterDungeon()
	{
		Clear();

		FloorSetting(1); // 1층 진입

		// 현 플레이어의 맵상의 좌표
		m_nMapCoordX = 0;
		m_nMapCoordY = 0;

		// 좌표별 Room 생성
		CreateRoom();

		// 방 입장
		ChangeRoom(0); // 0, 0입장
	}

	public void FloorSetting(int a_nFloor)
	{
		FloorData first = a_nFloor.GetFloorData();
		List<eNPC> liNPC = new List<eNPC>();

		if (first.bHasDiningRoom == true)
		{
			liNPC.Add(eNPC.DungeonDining);
		}

		if (first.bHasWaeponRoom == true)
		{
			liNPC.Add(eNPC.DungeonWaepon);
		}

		if (first.nNPC != (int)eNPC.None)
		{
			liNPC.Add((eNPC)first.nNPC);
		}

		m_stDungeon.CreateDungeon(first.nID, first.nRoomCount, liNPC);

		// 해당 층별 방아이디나 따로 가질 수 있지만. 대충 프리펩에서 로딩하는 형태라 일단 여기서 세팅

		var node = m_stDungeon.m_mapRoom.GetEnumerator();

		while( node.MoveNext() )
		{
			var st = node.Current.Value;
			st.nRoomID = Random.Range(1, 4); // 프리팹이 1~3개밖에없음
		}
	}

	public void CreateRoom()
	{
		var node = m_stDungeon.m_mapRoom.GetEnumerator();

		while ( node.MoveNext() )
		{
			var stInfo = node.Current.Value;
			
			int nX = stInfo.nX;
			int nY = stInfo.nY;

			var room = new Room(nX, nY);
			room.SetData(m_objRoomRoot, stInfo);

			m_mapRoom.Add(nX.GetCoordKey(nY), room);
		}
	}

	public void ChangeRoom(int a_nKey)
	{
		int nX = a_nKey % ST_DungeonInfo.nGap;
		int nY = a_nKey / ST_DungeonInfo.nGap;

		if (m_refActiveRoom != null)
		{
			m_refActiveRoom.SetVisible(false);
		}

		try
		{
			m_refActiveRoom = m_mapRoom[a_nKey];

			m_nMapCoordX = nX;
			m_nMapCoordY = nY;
		}
		catch
		{
			Debug.LogError(string.Format("x : {0}, y : {1}, Key : {2}", nX, nY, a_nKey));
		}

		m_refActiveRoom.SetVisible(true);
		
		GameMng.Ins.m_refPlayer.transform.localPosition = m_refActiveRoom.GetSpawnPos(eDir.Left);
	}

	public void ChangeRoom(eDir a_eDir)
	{
		int nKey = m_refActiveRoom.nCoordKye;
		nKey += a_eDir.GetKeyGap();
		
		ChangeRoom(nKey);
	}

	public void ChangeRoom_withPortal(int a_nKey)
	{
		if( m_nMapCoordX.GetCoordKey(m_nMapCoordY) == a_nKey )
		{
			return;
		}
		
		CSceneMng.Ins.FadeIn(
			() =>
			{
				ChangeRoom(a_nKey);
			}
		);
	}
}
