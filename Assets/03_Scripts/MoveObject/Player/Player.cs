using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public partial class Player : MoveObject
{
	#region INSPECTOR
	
	public Rigidbody2D	m_rb;
	public UISprite		m_spr;

	#endregion

	[System.NonSerialized]
	protected bool				m_bRight;

	[System.NonSerialized]
	protected bool				m_bJumping;

	[System.NonSerialized]
	protected bool				m_bGrounded;

	public Transform	groundCheck;
	public LayerMask	groundLayers;

	public float groundCheckRadius = 0.1f;
	
	protected System.Action m_fpFixedUpdate = null;
	protected System.Action<Collider2D> m_fpOnTriggerEnter2D = null;
	protected System.Action<Collision2D> m_fpOnCollisionEnter2D = null;
	

	// 1. 게임매니져가 씬을 받으면 기본 스탯 세팅
	// 2. 마을의 플레이어는 그걸로 세팅
	// 3. 인겜은 플레이어 상속해서 인겜으로 해서 ㄱㄱ

	public void SetStat(ST_GameStat a_refStat, StatData a_refEquipStat)
	{
		m_refStat.Copy(a_refStat);
		m_refStat.Add(a_refEquipStat);
	}

	void Awake()
	{
		GameMng.Ins.SetPlayer(this);

		Camera.main.gameObject.transform.parent = this.gameObject.transform;
	}

	void Start()
	{
		if (CSceneMng.Ins.eNowScene == eScene.Town)
		{
			m_fpFixedUpdate = FixedUpdate_InTown;
			m_fpOnTriggerEnter2D = OnTriggerEnter2D_InTown;
			m_fpOnCollisionEnter2D = OnCollisionEnter2D_InTown;
		}	
	}

	public void FixedUpdate()
	{
		m_fpFixedUpdate();
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		m_fpOnTriggerEnter2D(collision);
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		m_fpOnCollisionEnter2D(collision);
	}
}
