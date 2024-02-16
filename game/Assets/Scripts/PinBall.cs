using UnityEngine;

public class PinBall : MonoBehaviour
{
    [SerializeField] private float force = 100f;

    private Rigidbody2D _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 콜라이더를 가지고 있는지 확인
        if (collision.collider != null)
        {
            // 충돌한 오브젝트의 방향 벡터를 가져옴
            Vector2 reflectDirection = Vector2.Reflect(transform.position - collision.transform.position, collision.contacts[0].normal).normalized;
            
            // 충돌한 오브젝트의 Rigidbody2D를 가져옴
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // 오브젝트가 Rigidbody2D를 가지고 있다면 반사된 방향으로 힘을 가함
                rb.velocity = reflectDirection * 150f;
                Debug.DrawRay(collision.transform.position, reflectDirection * 10f, Color.gray, 5f);
            }
        }
    }
    
    private void Reflect(Vector3 currentPosition)
    {
        Vector3 reflectDirection = currentPosition - transform.position;
        float result = reflectDirection.x > 0 ? 1.0f : -1.0f;

        _rigidbody.AddForce(new Vector3(force * result, 50f, 0f));
    }
}
