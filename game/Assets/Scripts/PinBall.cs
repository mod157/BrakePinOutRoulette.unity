using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PinBall : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp_Name;
    [SerializeField] private float force = 50f;

    private Image _image;
    private Rigidbody2D _rigidbody;
    private Color _color;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _color = SetColor();
        _image.color = _color;
        tmp_Name.color = _color;
    }
    
    public void EnableSimulated()
    {
        _rigidbody.simulated = true;
    }

    public void DisableSimulated()
    {
        _rigidbody.simulated = false;
    }

    public void DeadBall()
    { 
        gameObject.SetActive(false);
        GameManager.Instance.RemoveBall(this);
    }
    
    private Color SetColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value); // 무작위 색상 생성
        return randomColor; // 객체의 색상을 무작위 색상으로 설정
    }

    public void SetName(string name)
    {
        gameObject.name = $"Ball_{name}";
        tmp_Name.text = name;
        
        gameObject.SetActive(true);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 콜라이더를 가지고 있는지 확인
        Debug.Assert(collision != null);
        // 충돌한 오브젝트의 방향 벡터를 가져옴
        Vector2 reflectionDirection = Vector2.Reflect(transform.position - collision.transform.position, collision.contacts[0].normal).normalized;
        
        // 충돌한 오브젝트의 Rigidbody2D를 가져옴
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 오브젝트가 Rigidbody2D를 가지고 있다면 반사된 방향으로 힘을 가함
            rb.velocity = reflectionDirection * Random.Range(force, force * 1.5f);
            Debug.DrawRay(collision.transform.position, reflectionDirection * 10f, Color.gray, 5f);
        }
    }

    public Color Color => _color;

    public string Name => tmp_Name.text;
}
