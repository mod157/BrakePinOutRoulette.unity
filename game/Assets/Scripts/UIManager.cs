using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField if_ballNames;
    [SerializeField] private Button btn_Start;
    [SerializeField] private TextMeshProUGUI text_Title;
    [SerializeField] private Button btn_ModeLastWin;
    [SerializeField] private Button btn_ModeBlockCountWin;
    
    private CanvasGroup _cg;

    public Action<int, string> respawnBallAction;
    
    //3EFF00
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
        
        btn_ModeLastWin.onClick.AddListener(() =>
        {
            btn_ModeBlockCountWin.image.color = Color.white;
            btn_ModeLastWin.image.color = Color.green;

            GameManager.Instance.GameMode = GameManager.GameModeOption.LastWin;
        });
        
        btn_ModeBlockCountWin.onClick.AddListener(() =>
        {
            btn_ModeLastWin.image.color = Color.white;
            btn_ModeBlockCountWin.image.color = Color.green;

            GameManager.Instance.GameMode = GameManager.GameModeOption.BlockCountWin;
        });
        
#if UNITY_EDITOR
        if_ballNames.text = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15";
#endif
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
        OnInputFieldValueChanged(if_ballNames.text);
    }
    

    public void SetWinTitle(PinBall winBall)
    {
        text_Title.text = $"{winBall.Name} Win!";
        text_Title.color = winBall.Color;
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
