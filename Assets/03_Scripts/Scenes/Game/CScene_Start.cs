using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScene_Start : CScene
{
	#region INSPECTOR

	#endregion

	// 메세지 처리기
	public void OnClickStart()
	{
		CSceneMng.Ins.ChangeScene(Global_Define.eScene.Town);
	}

}
