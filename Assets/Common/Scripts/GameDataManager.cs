using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Parse;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameDataManager : MonoSingleton<GameDataManager>
{
	// singleton property
	public static GameDataManager Instance
	{
		get
		{
			if (instance == null)
			{
				Debug.Log("GameDataManager의 instance를 생성합니다.");
				instance = new GameObject("_GameDataManager").AddComponent<GameDataManager>();
			}

			return instance;
		}
	}


	protected override void Awake()
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);

		//PlayGamesPlatform.DebugLogEnabled = true;
#if !UNITY_EDITOR
		PlayGamesPlatform.Activate();
#endif
	}


	public ParseUser User
	{
		get
		{
			return ParseUser.CurrentUser;
		}
	}


	public Task LoginAsync()
	{
		var tcs = new TaskCompletionSource<string>();

		// try social login
		Social.localUser.Authenticate((success) =>
		{
			if (success)
				tcs.TrySetResult(Social.localUser.userName);
			else
				tcs.TrySetCanceled();
		});

		var task = tcs.Task.ContinueWith(t =>
		{
			return ParseUser.Query.WhereEqualTo("username", t.Result).CountAsync();
		}).Unwrap().ContinueWith(t =>
		{
			if (t.Result > 0)
				return ParseUser.LogInAsync(Social.localUser.id, Social.localUser.id);
			ParseUser user = new ParseUser()
			{
				Username = Social.localUser.id,
				Password = Social.localUser.id,
			};
			print(Social.localUser.id);
			return user.SignUpAsync();
		}).Unwrap();

		return task;
	}


	public void Logout() { }
}
