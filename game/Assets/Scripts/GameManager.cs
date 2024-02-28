using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Nammu.Utils;
using UnityEngine;
using UnityEngine.UI;

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

    public void GameStart()
    {
        _stopwatch.Start();
        _isStart = true;
        gridLayout.enabled = false;
        
        foreach (var ball in pinBallsList)
        {
            ball.EnableSimulated();
        }
    }

    public void GameEnd()
    {
        gridLayout.enabled = true;
        _isStart = false;

        Winner();
        
        foreach (var ball in pinBallsList)
        {
            ball.DisableSimulated();
        }
        
        _stopwatch.Stop();
        
        uiManager.UIReset();
        MapManager.Instance.ResetBlock();
    }

    public void Winner()
    {
        switch (_mode)
        {
            case GameModeOption.LastWin:
                uiManager.SetWinTitle(pinBallsList.Last());
                break;
            case GameModeOption.BlockCountWin:
                PinBall highCountPinBall = pinBallsList[0];
                for (int i = 1; i < pinBallsList.Count; i++)
                {
                    if (pinBallsList[i].BrokenCount > highCountPinBall.BrokenCount)
                    {
                        highCountPinBall = pinBallsList[i];
                    }

                    if (pinBallsList[i].BrokenCount == highCountPinBall.BrokenCount)
                    {
                        
                    }
                    
                }
                break;
        }
        
    }

    private void RespawnBall(int index, string name)
    {
        for (int i = pinBallsList.Count - 1; i > index; i--)
        {
            pinBallsList[i].gameObject.SetActive(false);
        }
        
        if (index >= pinBallsList.Count)
        {
            PinBall pinBall = Instantiate(ballObject, ballObjectdParentTransform).GetComponent<PinBall>();
            pinBall.SetName(name);
            pinBallsList.Add(pinBall);
            return;
        }
        
        pinBallsList[index].SetName(name);
        pinBallsList[index].gameObject.SetActive(true);
    }

    public void RemoveBall(PinBall removeBall)
    {
        int removeIndex = pinBallsList.IndexOf(removeBall);
        
        _removeIndexList.Add(removeIndex);

        foreach (var index in _removeIndexList)
        {
            UnityEngine.Debug.Log("Remove Index : " + index);
        }
        if (pinBallsList.Count(x => x.gameObject.activeSelf) == 1)
        {
            GameEnd();
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
