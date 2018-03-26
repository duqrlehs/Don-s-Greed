using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class CScene : MonoBehaviour
{
	#region INSPECTOR

	public GameObject	m_objRoot;
	public eScene		m_eScene;

	#endregion

	protected void Awake()
	{
		CSceneMng.Ins.SetScene(this);
	}
}
