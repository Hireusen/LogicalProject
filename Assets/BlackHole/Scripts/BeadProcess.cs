using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 구슬을 순환하며 이동량을 적용합니다.
/// </summary>
public class BeadProcess : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private float _velocityMultiply = 1f;
    [SerializeField, Range(0f, 1f)] private float _velocityFriction = 0.1f;
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void TransferBeads(BeadData beads)
    {
        // 변수 빌드 & 캐싱
        int activeCount = beads.activeCount;
        float velocityMultifly = _velocityMultiply * Time.deltaTime;
        float velocityFriction = 1f - (_velocityFriction * Time.deltaTime);
        velocityFriction = Mathf.Clamp01(velocityFriction);
        // 모든 구슬 순회
        for (int i = 0; i < activeCount; ++i) {
            beads.pos[i] += beads.velocity[i] * velocityMultifly;
            beads.velocity[i] *= velocityFriction;
        }
    }
    #endregion
}
