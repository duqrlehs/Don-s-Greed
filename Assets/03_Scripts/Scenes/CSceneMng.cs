using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;
using Global_Define;


public class CSceneMng : MonoBehaviour
{
	#region SINGLETON
	public static bool destroyThis = false;

	static CSceneMng _instance = null;

	public static CSceneMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(CSceneMng)) as CSceneMng;
				if (_instance == null)
				{
					_instance = new GameObject("CSceneMng", typeof(CSceneMng)).GetComponent<CSceneMng>();
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

	// 
	// Transition.StartFade();

	CScene m_refScene = null;
	NPCText m_objText = null;

	public GameObject objRoot
	{
		get { return m_refScene.m_objRoot; }
	}

	public NPCText textBox
	{
		get
		{
			if (m_objText == null)
			{
				var newObj = Camera.main.gameObject.Instantiate_asChild("NPCText");
				m_objText = newObj.GetComponent<NPCText>();
			}

			return m_objText;
		}
	}

	public eScene eNowScene
	{
		get { return m_refScene.m_eScene; }
	}

	public void ChangeScene(eScene a_eScene)
	{
		m_refScene = null;

		ResetPopup();

		Transition.LoadLevel(a_eScene.ToDesc(), 0.5f, Color.black);
	}

	public void FadeIn(System.Action a_fpCallback)
	{
		m_refScene.CloseAllPopup();

		Transition.FadeAction(0.5f, Color.black, a_fpCallback);
	}

	public void SetScene(CScene a_refScene)
	{
		m_refScene = a_refScene;

		if( m_refScene.m_eScene == eScene.Town )
		{
			GameMng.Ins.EnterTown();
		}
	}

	public void ResetPopup()
	{
		// 삭제는 매우 비용이 쎕니다.// 씬 전환이 보통 삭제관련 넣기 가장 좋은 때 입니다. 그 때만 지웁니다.
		if ( m_objText != null ) { Destroy(m_objText); m_objText = null; }
	}
}
