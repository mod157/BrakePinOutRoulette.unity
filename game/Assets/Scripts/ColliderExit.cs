using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderExit : MonoBehaviour
{
    private RectTransform _rect;
    private Collider2D _collider2D;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _collider2D = GetComponent<Collider2D>();
        // 초기 설정 콜라이더 크기 조정
        ResizeCollider();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Assert(collision != null);
        if (collision.transform.CompareTag("Ball"))
        {
            PinBall enterObj = collision.transform.GetComponent<PinBall>();
            enterObj.DeadBall();
        }
    }
    
    private void ResizeCollider()
    {
        // RectTransform의 크기를 Collider2D에 적용
        if (_rect != null && _collider2D != null)
        {
            // RectTransform의 크기를 가져옴
            Vector2 size = _rect.rect.size;

            // BoxCollider2D인 경우
            if (_collider2D is BoxCollider2D)
            {
                BoxCollider2D boxCollider = (BoxCollider2D)_collider2D;
                boxCollider.size = size;
            }
            // CircleCollider2D인 경우
            else if (_collider2D is CircleCollider2D)
            {
                CircleCollider2D circleCollider = (CircleCollider2D)_collider2D;
                // CircleCollider2D는 반지름을 사용하므로 가로/세로 크기의 평균을 반지름으로 설정
                circleCollider.radius = Mathf.Max(size.x, size.y) * 0.5f;
            }
            // PolygonCollider2D인 경우
            else if (_collider2D is PolygonCollider2D)
            {
                // PolygonCollider2D는 좌표를 직접 설정해야 하므로 여기에 해당하는 코드 작성
                Debug.LogWarning("PolygonCollider2D의 크기를 자동으로 조정할 수 없습니다.");
            }
            // 기타 콜라이더 형식인 경우
            else
            {
                Debug.LogWarning("이 스크립트는 BoxCollider2D, CircleCollider2D에만 사용할 수 있습니다.");
            }
        }
    }
}
