using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class NPC : InteractionObj
{
	#region INSPECTOR
	
	public eNPC			m_eNPC;
	public UILabel		m_lbName;
	public NPCBox		m_cNPCBox;

	#endregion

	NPCData				m_refTable;

	public void Awake()
	{
		if( m_eNPC == eNPC.None )
		{
			Debug.LogError("inspector error - set value in inspector");
		}

		m_refTable = m_eNPC.GetData();
		m_cNPCBox.SetInteraction(
			(a_obj) => {
				CSceneMng.Ins.textBox.SetData(m_refTable.strName, 1);
				CSceneMng.Ins.textBox.SetVisible(true);
			}
		);
	}

	public void Start()
	{
		m_lbName.text = m_refTable.strName;
	}

	public override void Interaction(MoveObject a_obj)
	{
		if(m_cNPCBox.gameObject.activeSelf == false )
		{
			m_cNPCBox.gameObject.SetActive(true);
		}
	}


}
