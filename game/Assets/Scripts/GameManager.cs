using System;
using System.Collections.Generic;
using Nammu.Utils;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Component")] 
    [SerializeField] private List<PinBall> pinBallsList;
    
    [Space(15)]
    [Header("BaseObject")]
    [SerializeField] private GameObject ballObject;
    [SerializeField] private GameObject blockObject;

    [Space(15)] [Header("Option")] 
    [SerializeField] private int blockLife = 1;
    [SerializeField] private int mapIndex = 0;

    private void Awake()
    {
        pinBallsList = new List<PinBall>();
    }

    public void GameStart()
    {
        
    }

    public void GameEnd()
    {
        
    }
}
