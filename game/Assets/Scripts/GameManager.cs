using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cysharp.Threading.Tasks;
using Nammu.Utils;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [Header("Component")] 
    [SerializeField] private List<PinBall> pinBallsList;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GridLayoutGroup gridLayout;
    
    [Space(15)]
    [Header("BaseObject")]
    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject blockObject;
    [SerializeField] private Transform ballObjectdParentTransform;

    [Space(15)] [Header("Option")] 
    [SerializeField] private int blockLife = 1;
    [SerializeField] private int mapIndex = 0;

    private bool _isStart;
    private float _playTime;
    private List<int> _removeIndexList;
    private Stopwatch _stopwatch;
    private MapManager _mapManager;
    private GameModeOption _mode;

    public enum GameModeOption
    {
        LastWin,
        BlockCountWin
    }
    
    private void Awake()
    {
        pinBallsList = new List<PinBall>();
        _stopwatch = new Stopwatch();
        _removeIndexList = new List<int>();
    }

    private void OnEnable()
    {
        UnityEngine.Debug.Assert(uiManager != null);
        uiManager.respawnBallAction += RespawnBall;
    }

    private void OnDisable()
    {
        uiManager.respawnBallAction -= RespawnBall;
    }

    public async void GameStart()
    {
        //ShuffleList(pinBallsList);
        for (int i = 0; i < pinBallsList.Count; i++)
        {
            int randomIndex = Random.Range(0, i + 1);
            pinBallsList[i].transform.SetSiblingIndex(randomIndex);
            pinBallsList[i].EnableSimulated();
        }
        
        await UniTask.DelayFrame(1);
        _stopwatch.Start();
        _isStart = true;
        gridLayout.enabled = false;
    }

    private void RespawnBall(string[] names)
    {
        int i;
        for (i = 0; i < names.Length; i++)
        {
            if (names.Length > pinBallsList.Count)
            {
                PinBall pinBall = Instantiate(ballObject, ballObjectdParentTransform).GetComponent<PinBall>();
                pinBallsList.Add(pinBall);
            }

            pinBallsList[i].SetName(names[i]);
            pinBallsList[i].gameObject.SetActive(true);
        }
        
        for (int j = i; j < pinBallsList.Count; j++)
        {
            Debug.Log("Disable Ball - " + j + " / " + pinBallsList[j].gameObject.name);
            pinBallsList[j].gameObject.SetActive(false);
        }
    }

    public void RemoveBall(PinBall removeBall)
    {
        int removeIndex = pinBallsList.IndexOf(removeBall);
        
        Debug.Log($"[{removeIndex}]-{removeBall.name}");
        _removeIndexList.Add(removeIndex);
        
        if (pinBallsList.Count(x => x.gameObject.activeSelf) == 1)
        {
            GameEnd();
        }
    }
    
    public void GameEnd()
    {
        Debug.Log("GameEnd");
        gridLayout.enabled = true;
        _isStart = false;

        Winner();
        
        foreach (var ball in pinBallsList)
        {
            ball.DisableSimulated();
        }
        
        _stopwatch.Stop();
        _removeIndexList.Clear();
        uiManager.UIReset();
        MapManager.Instance.ResetBlock();
    }

    public void Winner()
    {
        PinBall winnerBall = null;
        switch (_mode)
        {
            case GameModeOption.LastWin:
                winnerBall = pinBallsList.Find(x => x.gameObject.activeSelf);
                
                break;
            case GameModeOption.BlockCountWin:
                winnerBall = pinBallsList[0];
                for (int i = 1; i < pinBallsList.Count; i++)
                {
                    var pinball = pinBallsList[i];
                    if (pinball.BrokenCount > winnerBall.BrokenCount)
                    {
                        winnerBall = pinball;
                    }

                    if (pinball.BrokenCount == winnerBall.BrokenCount)
                    {
                        int highCountindex = pinBallsList.IndexOf(winnerBall);
                        int currentindex = pinBallsList.IndexOf(pinball);

                        if (_removeIndexList.IndexOf(currentindex) > _removeIndexList.IndexOf(highCountindex))
                            winnerBall = pinball;
                    }
                }
                break;
        }
        
        Debug.Log("Winner - " + winnerBall.Name + " / " + winnerBall.BrokenCount);
        uiManager.SetWinTitle(winnerBall);
    }

    
    /*private void BallShuffle()
    {
        //Fisher-Yates 알고리즘
        for (int i = pinBallsList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = pinBallsList[i];
            pinBallsList[i] = pinBallsList[randomIndex];
            pinBallsList[randomIndex] = temp;
        }
    }*/
    
    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    

    public bool IsGameStart => _isStart;
    //Play 시간에 따른 속도 조절
    public float PlayTime => Mathf.Clamp(_stopwatch.ElapsedMilliseconds / 10f, 0, 3f);

    public GameModeOption GameMode
    {
        get => _mode;
        set => _mode = value;
    }
}
