using UnityEngine;
using System.Collections;

public abstract class ActiveSkillBase : ScriptableObject, ISkill
{
	[SerializeField]
	[Multiline()]
	private string description;


	public string Description
	{
		get { return description; }
	}


	public abstract void Use();
}
