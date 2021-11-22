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

    // 점점 투명해지는 효과 함수
    private IEnumerator FadeOut(SpriteRenderer spriteRenderer, float amount)
    {
        Color color = spriteRenderer.color;     // 투명도 가져오기
        while (color.a > 0.0f)                  // 아직 완전 투명이 아니라면
        {
            color.a -= amount;                  // 투명도 낮추기
            spriteRenderer.color = color;       // 투명도 적용하기
            yield return new WaitForSeconds(amount);
        }
    }
}
