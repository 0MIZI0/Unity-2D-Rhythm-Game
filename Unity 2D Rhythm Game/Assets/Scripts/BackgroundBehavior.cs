using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    public GameObject gameBackground;
    private SpriteRenderer gameBackgroundSpriteRenderer;

    private void Start()
    {
        gameBackgroundSpriteRenderer = gameBackground.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut(gameBackgroundSpriteRenderer, 0.005f));
    }

    // ���� ���������� ȿ�� �Լ�
    private IEnumerator FadeOut(SpriteRenderer spriteRenderer, float amount)
    {
        Color color = spriteRenderer.color;     // ���� ��������
        while (color.a > 0.0f)                  // ���� ���� ������ �ƴ϶��
        {
            color.a -= amount;                  // ���� ���߱�
            spriteRenderer.color = color;       // ���� �����ϱ�
            yield return new WaitForSeconds(amount);
        }
    }
}
