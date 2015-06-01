using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using HutongGames.PlayMaker;
using Parse;

public class DataManager : MonoSingleton<DataManager>
{
	// singleton property
	public static DataManager Instance
	{
		get
		{
			if (instance == null)
			{
				Debug.Log("DataManager의 instance를 생성합니다.");
				instance = new GameObject("_Data Manager").AddComponent<DataManager>();
			}

			return instance;
		}
	}


	private FsmInt bestScoreFsm;
	private FsmInt allCoinFsm;
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
			User["coin"] = value;
			User.SaveAsync().ContinueWith(t => {
				RecordCoinLog(cachedCoin, value);
				allCoinFsm.Value = cachedCoin = value;
			});
		}
	}


	public int BestScore
	{
		get { return cachedBestScore; }

		set
		{
			if (value <= cachedBestScore)
				return;

			User["bestScore"] = value;
			User.SaveAsync().ContinueWith(t =>
			{
				RecordCoinLog(cachedBestScore, value);
				bestScoreFsm.Value = cachedBestScore = value;
			});
		}
	}


	protected override void Awake()
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);

		bestScoreFsm = FsmVariables.GlobalVariables.FindFsmInt("Best Score");
		allCoinFsm = FsmVariables.GlobalVariables.FindFsmInt("All Coin");
	}


	public IEnumerable<PlayerCharacter> OwnedCharacters
	{
		get
		{
			// if no have character
			// return default character
			if (User.ContainsKey("characters") == false)
			{
				AddCharacter(GameSettings.Instance.defaultCharacters);
				return new PlayerCharacter[] { GameSettings.Instance.defaultCharacters };
			}


			// get all owned character guids
			var guids = from obj in User.Get<IEnumerable<object>>("characters")
						select obj as string;


			// make PlayerCharacter List
			List<PlayerCharacter> characters = new List<PlayerCharacter>();
			foreach (var guid in guids)
			{
				var character = from c in GameSettings.Instance.characters
								where c.ID.guid == guid
								select c;
				characters.Add(character.First());
			}

			return characters;
		}
	}


	public PlayerCharacter SelectedCharacter
	{
		get
		{
			// return default character
			if (User.ContainsKey("characters") == false)
			{
				AddCharacter(GameSettings.Instance.defaultCharacters);
				return GameSettings.Instance.defaultCharacters;
			}

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


	public Task Init()
	{
		var tcs = new TaskCompletionSource<string>();

#if !UNITY_EDITOR

		// try social login
		UM_GameServiceManager.OnPlayerConnected = () =>
		{
			tcs.TrySetResult(UM_GameServiceManager.instance.player.PlayerId);
			// tcs.TrySetResult(GameSettings.Instance.TestID);
		};

		UM_GameServiceManager.instance.Connect();

#else
		tcs.TrySetResult(GameSettings.Instance.TestID);
#endif
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
					return ParseUser.LogInAsync(t.Result, t.Result);
				}

				// not exist then signup
				ParseUser user = new ParseUser()
				{
					Username = t.Result,
					Password = t.Result,
				};

				// try signup
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
			Debug.LogFormat("Login Success");
			allCoinFsm.Value = cachedCoin = User.Get<int>("coin");
			bestScoreFsm.Value = cachedBestScore = User.Get<int>("bestScore");
		});
		
		return task;
	}


	public void Logout()
	{
		ParseUser.LogOut();
	}


	public Task AddCharacter(PlayerCharacter pc)
	{
		User.AddToList("characters", pc.ID.guid);
		return User.SaveAsync();
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
