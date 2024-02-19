using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField if_ballNames;
    [SerializeField] private Button btn_Start;
    
    //3FFF00

    private CanvasGroup _cg;

    public Action<int, string> respawnBallAction;
    private void Awake()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if_ballNames.onValueChanged.AddListener(OnInputFieldValueChanged);
        btn_Start.onClick.AddListener(() =>
        {
            DisableCanvasGroup();
            GameManager.Instance.GameStart();
        });
    }

    void OnInputFieldValueChanged(string newValue)
    {
        // ','를 기준으로 문자열 분할
        string[] dataStrings = newValue.Split(',');

        // 분할된 데이터를 출력 또는 사용할 수 있습니다.
        for(int i = 0 ; i < dataStrings.Length; i++)
        {
            Debug.Assert(respawnBallAction != null);
            respawnBallAction.Invoke(i, dataStrings[i]);
        }
    }

    public void UIReset()
    {
        EnableCanvasGroup();
    }

    private void EnableCanvasGroup()
    {
        _cg.alpha = 1;
        _cg.interactable = true;
        _cg.blocksRaycasts = true;
    }

    private void DisableCanvasGroup()
    {
        _cg.alpha = 0;
        _cg.interactable = false;
        _cg.blocksRaycasts = false;
    }
}
