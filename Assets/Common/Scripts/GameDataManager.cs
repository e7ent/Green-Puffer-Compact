using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
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


	private int cachedCoin;
	private int cachedBestScore;
	private List<string> cachedOwnedCharacters = new List<string>();


	public ParseUser User
	{
		get
		{
			if (ParseUser.CurrentUser == null)
				Debug.Log("User가 없습니다.");
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
			User.SaveAsync();
		}
	}


	public int BestScore
	{
		get { return cachedBestScore; }

		set
		{
			if (value <= cachedBestScore)
				return;

			User["bestScore"] = cachedBestScore = value;
			User.SaveAsync();
		}
	}


	public IEnumerable<PlayerCharacter> OwnedCharacters
	{
		get
		{
			// if no character
			// return default character
			if (User.ContainsKey("characters") == false)
				return new PlayerCharacter[] { GameSettings.Instance.defaultCharacters };


			// get all owned character guids
			var guids = from obj in User.Get<IEnumerable<object>>("characters")
						select obj as string;


			// make PlayerCharacter List
			var characters = from character in GameSettings.Instance.characters
							 where guids.Contains(character.ID.guid)
							 select character;

			return characters;
		}
	}


	public PlayerCharacter SelectedCharacter
	{
		get
		{
			// return default character
			if (User.ContainsKey("characters") == false)
				return GameSettings.Instance.defaultCharacters;

			// get saved character guid
			var selectedGuid = PlayerPrefs.GetString("SelectedCharacter", GameSettings.Instance.defaultCharacters.ID.guid);

			// compare owned character
			var characters = from character in OwnedCharacters
							where character.ID.guid == selectedGuid
							select character;
			if (characters.Count() <= 0)
				return GameSettings.Instance.defaultCharacters;

			return characters.First();

		}

		set
		{
			PlayerPrefs.SetString("SelectedCharacter", value.ID.guid);
		}
	}


	protected override void Awake()
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);

		//PlayGamesPlatform.DebugLogEnabled = true;
#if UNITY_ANDROID
		PlayGamesPlatform.Activate();
#endif
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
			{
#if UNITY_EDITOR
				tcs.TrySetResult(GameSettings.Instance.TestID);
#else
				tcs.TrySetException(new Exception("Social 로그인 실패."));
#endif
			}
		});

		var task = tcs.Task.ContinueWith(t =>
		{
			// if social login is failed will return prev task.
			if (t.IsFaulted)
				return t;


			// check username already exist
			return ParseUser.Query.WhereEqualTo("username", t.Result).CountAsync().ContinueWith(t2 =>
			{

				// if exist
				if (t2.Result > 0)
				{
					// try login
					Debug.LogFormat("Try Login username:{0}", t.Result);
					return ParseUser.LogInAsync(t.Result, t.Result);
				}

				// not exist then signup
				ParseUser user = new ParseUser()
				{
					Username = t.Result,
					Password = t.Result,
				};

				// try signup
				Debug.LogFormat("Try Signup username:{0}", user.Username);
				return user.SignUpAsync();

			}).Unwrap();


		}).Unwrap();

		// init some data
		task.ContinueWith(t =>
		{
			if (!t.IsCompleted || t.IsFaulted)
			{
				Debug.LogError("Login Faild");
				return;
			}
			Debug.LogFormat("Login Success {0}", User.Username);
			cachedCoin = User.Get<int>("coin");
			cachedBestScore = User.Get<int>("bestScore");
		});
		
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
