using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 키를 입력받아서 머리 지렁이의 회전 값을 변경합니다.
/// </summary>
public class WormRotate : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private float _rotateSpeed = 120f;
    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    public void TryRotate(WormData worms)
    {
        // 키 입력
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0f) {
            return;
        }
        // 회전량 적용
        float delta = -h * _rotateSpeed * Time.deltaTime; // 우측 입력 시 시계방향 회전 + 회전량
        Quaternion rotation = Quaternion.Euler(0f, 0f, delta);
        worms.headDir = (rotation * worms.headDir).normalized; // 오차가 누적될 수 있으므로 정규화
    }
    #endregion
}
