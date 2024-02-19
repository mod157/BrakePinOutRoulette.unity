using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorGuard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Error Reset Position");
        Debug.Assert(collision != null);
        // 충돌한 오브젝트의 방향 벡터를 가져옴
        collision.transform.localPosition = Vector3.zero;
    }
}
