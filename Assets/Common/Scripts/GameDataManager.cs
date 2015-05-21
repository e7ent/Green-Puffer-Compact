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

		ParseClient.Initialize(GameSettings.Instance.appID, GameSettings.Instance.dotnetID);
		//PlayGamesPlatform.DebugLogEnabled = true;
#if UNITY_ANDROID
		PlayGamesPlatform.Activate();
#endif
	}


	private int cachedCoin;


	public ParseUser User
	{
		get
		{
			return ParseUser.CurrentUser;
		}
	}


	public int Coin
	{
		get { return cachedCoin; }

		set
		{
			RecordCoinLog(cachedCoin, value);
			User["coin"] = cachedCoin = value;
			User.SignUpAsync();
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
#if UNITY_EDITOR
			user.Password = user.Username = "Test";
#endif
			return user.SignUpAsync();
		}).Unwrap();

		return task;
	}


	public void Logout()
	{
		ParseUser.LogOut();
	}


	public void RecordPlayLog(float startTime, float endTime, int rewardCoin)
	{
		var logObject = new ParseObject("PlayLog");
		logObject["startTime"] = startTime;
		logObject["endTime"] = endTime;
		logObject["playTime"] = endTime - startTime;
		logObject["rewardCoin"] = rewardCoin;
		logObject["createdBy"] = User;
		logObject.SaveAsync();
	}


	public void RecordCoinLog(int prevCoin, int newCoin)
	{
		var logObject = new ParseObject("CoinLog");
		logObject["prevCoin"] = prevCoin;
		logObject["newCoin"] = newCoin;
		logObject["interval"] = newCoin - prevCoin;
		logObject["createdBy"] = User;
		logObject.SaveAsync();
	}
}
