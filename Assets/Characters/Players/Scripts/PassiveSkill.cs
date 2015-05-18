using UnityEngine;
using System.Collections;

public abstract class PassiveSkill : ScriptableObject, ISkill
{
	[SerializeField]
	[Multiline()]
	private string description;


	public string Description
	{
		get { return description; }
	}

	public abstract void Update(MonoBehaviour user);
	public abstract void Use();
}
