using UnityEngine;
using System.Collections;

public abstract class PassiveSkillBase : ScriptableObject, ISkill
{
	[SerializeField]
	[Multiline()]
	private string description;


	public string Description
	{
		get { return description; }
	}

	public abstract void Update();
	public abstract void Use();
}
