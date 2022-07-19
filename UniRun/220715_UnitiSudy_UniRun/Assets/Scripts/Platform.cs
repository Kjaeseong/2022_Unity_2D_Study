using UnityEngine;

/*
pool : 웅덩이
Object Pool : 오브젝트들이 모여있는거
지금까지는 오브젝트가 필요하면 생성하고 파괴했음. 이 연산은 부담이 많이 감.

미리 만들어놓고 풀에서 꺼내서 쓰는게 바람직함.
(자주 생성되고 사라지는 오브젝트는 오브젝트풀링 하는것이 좋음)

ver.2021 부터 유니티 공식 지원.

입사하게 될 회사에서 2021이상 버전을 쓴다면 오브젝트 풀 사용가능.
이전 버전이면 직접 구현해야 함.

유니런에서는 직접 구현하는 방향으로 할거임.


*/

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour 
{
    private int _obstacleCount;
    public GameObject[] _obstacles; // 장애물 오브젝트들
    private bool _isStepped = false; // 플레이어 캐릭터가 밟았었는가
    private int _obstacleAct;
    
    [SerializeField]
    private int obstacleActPer = 25;

    void Awake()
    {
        _obstacleCount = transform.childCount;
        _obstacles = new GameObject[_obstacleCount];
        for(int i = 0; i < _obstacleCount; ++i)
        {
            _obstacles[i] = transform.GetChild(i).gameObject;
        }
    }
    
    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable() 
    {
        // 발판을 리셋하는 처리
        _isStepped = false;
        // 장애물을 활성/비활성, 확률은 본인 마음 50%
        
        for(int i = 0; i < _obstacleCount; ++i)
        {
            _obstacleAct = Random.Range(1, 101);
                                    //이상, 미만
            if(_obstacleAct > obstacleActPer)
            {
                _obstacles[i].SetActive(false);
            }
            else
            {
                _obstacles[i].SetActive(true);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Player")
        {
            if(_isStepped == false)
            {
                _isStepped = true;
                GameManager.Instance.AddScore();
            }
        }
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        // 플레이어가 자신을 밟았을때 로직 처리
    }
}