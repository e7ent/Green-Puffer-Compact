using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	protected static T instance;


	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				Debug.LogError(typeof(T).Name + "의 instance가 존재하지 않습니다.");
				return null;
			}

			return instance;
		}
	}


	protected virtual void Awake()
	{
		instance = this as T;
	}


	protected virtual void OnDestroy()
	{
		if (instance == this) instance = null;
	}
}