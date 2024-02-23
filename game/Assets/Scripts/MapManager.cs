using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nammu.Utils;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Transform mapTransform;
    [SerializeField] private List<GameObject> mapList;
    [SerializeField]
    private List<BlockController> _blockList;

    private void Start()
    {
        _blockList = mapTransform.GetComponentsInChildren<BlockController>().ToList();
    }

    public void ResetBlock()
    {
        foreach (var block in _blockList)
        {
            block.gameObject.SetActive(true);
        }
    }
}
