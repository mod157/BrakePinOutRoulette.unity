using UnityEngine;

public class BlockController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            // 충돌한 오브젝트의 방향 벡터를 가져옴
            Vector2 reflectionDirection = Vector2.Reflect(transform.position - collision.transform.position, collision.contacts[0].normal).normalized;

            PinBall currentPinBall = collision.collider.GetComponent<PinBall>();
            Rigidbody2D rb = currentPinBall.Rigidbody2D;

            if (rb != null)
            {
                // 오브젝트가 Rigidbody2D를 가지고 있다면 반사된 방향으로 힘을 가함
                rb.velocity = reflectionDirection * GameOption.FORCE;
                Debug.DrawRay(collision.transform.position, reflectionDirection * 10f, Color.gray, 5f);
            }

            currentPinBall.BrokenCount += 1;
            gameObject.SetActive(false);
        }
    }
}
