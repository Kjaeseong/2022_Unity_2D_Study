using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : SingletonBehaviour<GameManager>
{
    public int ScoreIncreaseAmount = 1;
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    //C# 이벤트....UnityAction??

    //UnityAction
    //UnityAction<T1>
    //UnityAction<T1, T2>
    //UnityAction<T1, T2, T3>
    //UnityAction<T1, T2, T3, T4>





    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();   //int타입의 인자를 전달할거임.(점수)
    public event UnityAction<int> OnScoreChanged2; // C#의 이벤트는 객체를 만들지 않아도 된다.

    public UnityEvent OnGameEnd = new UnityEvent();
    public event UnityAction OnGameEnd2;

    public int CurrentScore
    {
        get {
            return _currentScore;
        }
        set {
            _currentScore = value;
            OnScoreChanged.Invoke(_currentScore);

            //C# 이벤트 _ 동작방식이 조금 특이함, 구독자가 있는지 물어본다(null Check)
            //즉, Invoke()시 null Check를 항상 해줘야 한다.
            // ? -> null Check
            OnScoreChanged2?.Invoke(_currentScore);
        }
    }
    //여기서 value의 정체는 set 구현할때 일관된 이름으로 처리하기 위한거..프로퍼티 구현할때만 쓸 수 있음.
    
    private int _currentScore = 0; // 게임 점수
    private bool _isEnd = false;



/*
    public bool gameover
    {
        get
        {
            return _isEnd;
        }
        set
        {
            _isEnd = value;
            IsEnd.Invoke(_isEnd);
        }

    }
*/



    void Update() 
    {
        if(_isEnd && Input.GetKeyDown(KeyCode.R))
        {
            reset();
            SceneManager.LoadScene(0);
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore() 
    {
        CurrentScore += ScoreIncreaseAmount;
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void End() 
    {
        _isEnd = true;
        OnGameEnd.Invoke();
        OnGameEnd2?.Invoke();
    }

    private void reset()
    {
        _currentScore = 0;
        _isEnd = false;

    }


}