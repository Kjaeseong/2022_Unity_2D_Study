using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _ui;

    //점수를 게임매니저가 관리하고 ui반영시키는 방향이 바람직한 상황.
    
    void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();

    }

    void OnEnable()
    {
        //애드 전에는 한번 삭제해줄것.
        //코드가 길어지면 중복되는 코드로 인해 충돌날 수 있음.
        //리무브를 넣어도 없을 땐 아무일도 일어나지 않는다.
        //GameManager.Instance.OnScoreChanged.RemoveListener(UpdateText);
        //GameManager.Instance.OnScoreChanged.AddListener(UpdateText);

        //C#
        GameManager.Instance.OnScoreChanged2 -= UpdateText; //취소
        GameManager.Instance.OnScoreChanged2 += UpdateText; //구독
    }

    public void UpdateText(int score)
    {
        // _ui의 텍스트를 수정
        _ui.text = $"SCORE : {score}";
    }

    void OnDisable()
    {
        //GameManager.Instance.OnScoreChanged.RemoveListener(UpdateText);
        
        GameManager.Instance.OnScoreChanged2 -= UpdateText;

    }



}
