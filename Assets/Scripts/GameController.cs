using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameController");
                go.AddComponent<GameController>();
            }
            
            return instance;
        }
    }
    [SerializeField]
    PlayerController player;
    [SerializeField]
    DataManager data;
    [SerializeField]
    ManagerPool pool;

    [SerializeField]
    private MapConfig levelConfig = null;

    private List<IPlayerListener> listeners = new List<IPlayerListener>();
    Action<int> onScoreChange;

	public Vector3 spawnValues;
	int asteroidCount;
	float spawnWait;
	float waveWait;

    List<GameObject> listEnemy = new List<GameObject>();

    int unlockedLevels;
    public int UnlockedLevels
    {
        get => unlockedLevels;
        set => unlockedLevels = value;
    }

    private int lives;
    int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            GUIController.Instance.FoundScreen<ScreenGame>().UpdLives(lives);
        }
    }
    float timerWave;
    float timerSpawn;


    private PlayerState state = PlayerState.None;
    int countAst;
    int idMap;
	private int score;
    public int Score
    {
        get => score;
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
	{
        data.LoadGame();
        GUIController.Instance.ShowScreen<ScreenMap>();	
	}

    private void Update()
    {
        if(state == PlayerState.Game)
        {
            if (timerWave < 0)
            {
               if (timerSpawn < 0 && countAst < asteroidCount)
               {
                    Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    for (int i = 0; i < listEnemy.Count; i++)
                    {
                        if (!listEnemy[i].activeInHierarchy)
                        {
                            listEnemy[i].transform.position = spawnPosition;
                            listEnemy[i].SetActive(true);
                            break;
                        }
                    }
                    timerSpawn = 0.7f;
                    countAst++;
                }else 
                if (countAst >= asteroidCount)
                {
                    timerWave = waveWait;
                    countAst = 0;
                }
                timerSpawn -= Time.deltaTime;
            }
            timerWave -= Time.deltaTime;
        }
    }

	public void AddScore()
	{
        score--;
        if(score <= 0)
        {
            Restart(true);
        }
        onScoreChange(score);
	}

	public void LiveReduce()
	{
        Lives--;
        if (Lives <= 0)
        {
            GameOver();
        }
	}

    public void Restart(bool isUp = false)
    {
        if (isUp)
        {
            int index = idMap;
            index++;
            if(unlockedLevels < index)
            {
                unlockedLevels = index;
                data.SaveGame();
            }
            SetMap(index);
        }
        StartGame();
    }

    void GameOver()
    {
        ChangeState(PlayerState.None);
        GUIController.Instance.ShowScreen<ScreenLose>(true);
    }

    public void StartGame()
    {
        Lives = 3;
        waveWait = levelConfig.settings[idMap].waveSpawn;
        score = levelConfig.settings[idMap].countWin;
        pool.ClearPool(listEnemy);
        InitPool(20, levelConfig.settings[idMap].prefabAsteroid, listEnemy);
        timerWave = 2f;
        countAst = 0;
        asteroidCount = levelConfig.settings[idMap].waveAsteroids;
        ChangeState(PlayerState.Game);
        GUIController.Instance.ShowScreen<ScreenGame>(true);
        onScoreChange?.Invoke(score);
    }

    public void InitPool(int size, GameObject obj, List<GameObject> list)
    {
        pool.InitPool(size, obj, list);
    }

    public void SetMap(int id)
    {
        idMap = id;
        if(idMap >= levelConfig.settings.Length)
        {
            idMap = levelConfig.settings.Length - 1;
        }
    }

    public void AddPlayerListener(IPlayerListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void RemovePlayerListener(IPlayerListener listener)
    {
        listeners.Remove(listener);
    }

    public void ChangeState(PlayerState newState)
    {
        state = newState;
        listeners.ForEach(t => t.OnChangeState(newState));
    }

    public void AddChangeScore(Action<int> listener)
    {
        onScoreChange += listener;
    }

    public void RemoveChangeScore(Action<int> listener)
    {
        onScoreChange -= listener;
    }

}
