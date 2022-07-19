using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour 
{
    public AudioClip DeathClip; // 사망시 재생할 오디오 클립
    public float JumpForce = 700f; // 점프 힘
    public int MaxJumpCount = 2;

    private int _jumpCount = 0; // 누적 점프 횟수
    private bool _isOnGround = true; // 바닥에 닿았는지 나타냄
    public bool _isDead = false; // 사망 상태

    private Rigidbody2D _rigidbody; // 사용할 리지드바디 컴포넌트
    private Animator _animator; // 사용할 애니메이터 컴포넌트
    private AudioSource _audioSource; // 사용할 오디오 소스 컴포넌트
    private Vector2 _zero;

    //C#에서 상수 만드는 방법(평가 시점이 다름)
    // 1. const     : 컴파일 시점
    // 2. readonly  : 런타임 시점(보통 생성자에서)

    private static class AnimationID   //static 붙이면 인스턴스 멤버는 만들지 못함.
    {
        public static readonly int IS_ON_GROUND = Animator.StringToHash("IsOnGround");
        public static readonly int DIE = Animator.StringToHash("Die"); 
    }

    //C++에서는 함수 내부에서도 static 사용 가능했음. C#에선 안됨.

    //public으로 빼서 허용 각도 조정하는 방법도 있을거임.
    public static readonly float MIN_NORMAL_Y = Mathf.Sin(45f * Mathf.Deg2Rad);

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();   
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _zero = Vector2.zero;
    }

    void Start()
    {

    }

    private void Update() 
    {
       // 사용자 입력을 감지하고 점프하는 처리
       // 이 게임은 마우스 입력
        if(_isDead)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            //최대 점프에 도달했으면 아무것도 안함.
            if(_jumpCount >= MaxJumpCount)
            {
                return;
            }
            
            ++_jumpCount;
            _rigidbody.velocity = _zero;
            _rigidbody.AddForce(new Vector2(0f, JumpForce));
            _audioSource.Play();
        }

        

        if(Input.GetMouseButtonUp(0))
        {
            if(_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity *= 0.5f;
            }
        }
        
        _animator.SetBool(AnimationID.IS_ON_GROUND, _isOnGround);
    }

   private void Die() 
   {
        // 사망 처리
        // 재시작 있다.
        // 1. _isDead = true
        // 2. 애니메이션 업데이트
        // 3. 플레이어 케릭터 멈추기
        // 4. 죽을 때 소리도 재생
        _isDead = true;
        _animator.SetTrigger(AnimationID.DIE);
        _rigidbody.velocity = _zero;
        _audioSource.PlayOneShot(DeathClip);     //한번만 재생
        //죽을때 처리 끝
        GameManager.Instance.End();

   }

   private void OnTriggerEnter2D(Collider2D other) 
   { 
        if(other.tag == "Dead")
        {
            if(_isDead == false)
            {
                Die();
            }
        }
   }

    //왜 콜리전?? -> 어느 방향으로 부딪혔는지 판정할 수 있어야 하니까.
    // contacts -> 부딪힌 포인트들의 배열
   private void OnCollisionEnter2D(Collision2D collision) 
   {
        //바닥에 닿았을 때를 감지(플랫폼 위로 안착하면)
        ContactPoint2D point = collision.GetContact(0);
        if(point.normal.y >= MIN_NORMAL_Y)
        {
            _isOnGround = true;
            _jumpCount = 0;

        }
   }

   private void OnCollisionExit2D(Collision2D collision) 
   {
       _isOnGround = false;
   }
}