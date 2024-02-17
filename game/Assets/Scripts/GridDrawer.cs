using UnityEngine;
using UnityEngine.UI;

public class GridDrawer : MonoBehaviour
{
    public int width = 512; // 텍스처의 가로 크기
    public int height = 512; // 텍스처의 세로 크기
    public int rows = 20; // 그리드 행 수
    public int columns = 30; // 그리드 열 수

    void Start()
    {
        Texture2D gridTexture = CreateGridTexture();
        ApplyTextureToUI(gridTexture);
    }

    Texture2D CreateGridTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 각 셀의 색상을 결정
                Color color = (x % (width / rows) == 0 || y % (height / columns) == 0) ? Color.white : Color.clear;
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    void ApplyTextureToUI(Texture2D texture)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetParent(transform);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;

        Image image = GetComponent<Image>();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }
}