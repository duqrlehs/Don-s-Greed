using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class Portal : InteractionObj
{
	#region INSPECTOR

	public eScene m_eScene;

	#endregion

	private void Awake()
	{
		if( m_eScene == eScene.None )
		{
			Debug.LogError("logic error - inspector value setting");
		}
	}

	public override void Interaction(MoveObject a_obj)
	{
		CSceneMng.Ins.ChangeScene(m_eScene);
	}
}
