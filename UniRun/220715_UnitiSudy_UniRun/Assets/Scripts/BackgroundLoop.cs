using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour 
{
    private float _width;// 배경의 가로 길이
    private Vector2 _resetPosition;

    private void Awake() 
    {

        // 가로 길이를 측정하는 처리
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        _width = bc.size.x;

        Vector2 offset = new Vector2(_width * 2f, 0f);
        _resetPosition = (Vector2)transform.position * offset;
    }

    private void Update() 
    {
        Debug.Log(_width);
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을때 위치를 리셋
        transform.Translate(-10 * Time.deltaTime, 0f, 0f);
        if(transform.position.x <= -1 * _width)
        {
            resetPosition();   
        }

    }

    // 위치를 리셋하는 메서드
    private void resetPosition() 
    {
        transform.position = _resetPosition;
    }
}