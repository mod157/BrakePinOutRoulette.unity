using System;
using System.Collections.Generic;
using System.Diagnostics;
using Nammu.Utils;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameManager : Singleton<GameManager>
{
    [Header("Component")] 
    [SerializeField] private List<PinBall> pinBallsList;
    [SerializeField] private UIManager uiManager;
    
    [Space(15)]
    [Header("BaseObject")]
    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject blockObject;
    [SerializeField] private Transform ballObjectdParentTransform;

    [Space(15)] [Header("Option")] 
    [SerializeField] private int blockLife = 1;
    [SerializeField] private int mapIndex = 0;

    private bool _isStart = false;
    private float _playTime;
    private Stopwatch _stopwatch;
    
    private void Awake()
    {
        pinBallsList = new List<PinBall>();
        _stopwatch = new Stopwatch();
    }

    private void OnEnable()
    {
        Debug.Assert(uiManager != null);
        uiManager.respawnBallAction += RespawnBall;
    }

    private void OnDisable()
    {
        uiManager.respawnBallAction += RespawnBall;
    }

    public void GameStart()
    {
        _stopwatch.Start();
        _isStart = true;
        
        foreach (var ball in pinBallsList)
        {
            ball.EnableSimulated();
        }
    }

    public void GameEnd()
    {
        float eventValuePlayTime = Mathf.Clamp(_stopwatch.ElapsedMilliseconds / 10f, 0, 3f);
        Debug.Assert(eventValuePlayTime < 3f);

        _isStart = false;

        uiManager.UIReset();
        _stopwatch.Stop();
    }

    private void RespawnBall(int index, string name)
    {
        if (index >= pinBallsList.Count)
        {
            PinBall pinBall = Instantiate(ballObject, ballObjectdParentTransform).GetComponent<PinBall>();
            pinBall.SetName(name);
            pinBallsList.Add(pinBall);
            return;
        }
        
        pinBallsList[index].SetName(name);
    }

    public void RemoveBall(PinBall removeBall)
    {
        //pinBallsList.Remove(removeBall);
    }
    

    public bool IsGameStart() => _isStart;
    //Play 시간에 따른 속도 조절
    public float PlayTime() => Mathf.Clamp(_stopwatch.ElapsedMilliseconds / 10f, 0, 3f);
}
