using System;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 구슬을 순환하며 이동량을 적용하고 크기를 1로 감소시킵니다.
/// </summary>
public class BeadProcess : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private float _velocityMultiply = 1f;
    [SerializeField, Range(0f, 1f)] private float _velocityFriction = 0.1f;
    [SerializeField] private float _sizeSharpness = 5f;
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void UpdateBeads(BeadData beads)
    {
        // 변수 빌드 & 캐싱
        int activeCount = beads.activeCount;
        float velocityMultifly = _velocityMultiply * Time.deltaTime;
        float velocityFriction = 1f - (_velocityFriction * Time.deltaTime);
        velocityFriction = Mathf.Clamp01(velocityFriction);
        float t = Mathf.Exp(-_sizeSharpness * Time.deltaTime);
        // 모든 구슬 순회
        for (int i = 0; i < activeCount; ++i) {
            // 이동
            beads.pos[i] += beads.velocity[i] * velocityMultifly;
            // 감쇠
            beads.velocity[i] *= velocityFriction;
            // 크기
            float size = beads.sizeMultiply[i];
            if (size == 1f) {
                continue;
            }
            if (size > 1.01f) {
                beads.sizeMultiply[i] = Mathf.Lerp(size, 1f, t);
            } else {
                beads.sizeMultiply[i] = 1f;
            }
        }
    }
    #endregion
}
