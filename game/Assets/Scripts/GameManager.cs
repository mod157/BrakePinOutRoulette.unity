using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Nammu.Utils;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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
        gridLayout.enabled = false;
        
        foreach (var ball in pinBallsList)
        {
            ball.EnableSimulated();
        }
    }

    public void GameEnd()
    {
        Debug.Log(_stopwatch.ElapsedMilliseconds / 1000f);

        gridLayout.enabled = true;
        _isStart = false;

        foreach (var ball in pinBallsList)
        {
            ball.DisableSimulated();
        }
        
        uiManager.SetWinTitle(pinBallsList.Last());
        uiManager.UIReset();
        _stopwatch.Stop();
    }

    private void RespawnBall(int index, string name)
    {
        Debug.Assert(_isStart == false);
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
        int removeIndex = pinBallsList.IndexOf(removeBall);
        
        if (pinBallsList.Count(x => x.gameObject.activeSelf) == 1)
        {
            GameEnd();
        }
    }
    

    public bool IsGameStart() => _isStart;
    //Play 시간에 따른 속도 조절
    public float PlayTime() => Mathf.Clamp(_stopwatch.ElapsedMilliseconds / 10f, 0, 3f);
}
