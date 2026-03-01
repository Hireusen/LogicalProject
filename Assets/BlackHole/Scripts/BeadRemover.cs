using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 구슬을 순환하며 삭제 조건을 만족한 구슬을 삭제합니다.
/// 1. 카메라 범위 밖 / 2. 블랙홀이 켜져있고 블랙홀에 근접해있음
/// </summary>
public class BeadRemover : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Camera _camera;

    [Header("사용자 정의 설정")]
    [SerializeField] private float _removeDistance = 1f;
    #endregion

    #region ─────────────────────────▶ 내부 메서드 ◀─────────────────────────
    private static bool IsInBlackHole(Vector2 beadPos, Vector2 blackHolePos, float removeDistance)
    {
        return URange.InCircle(beadPos, blackHolePos, removeDistance);
    }

    private static bool IsOutsideCamera(Vector2 beadPos, Vector2 cameraMinPos, Vector2 cameraMaxPos)
    {
        return !URange.InRect(beadPos, cameraMinPos, cameraMaxPos);
    }
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void CleanBeads(Vector2[] beadsPos, bool[] beadsIsActive, Vector2 blackHolePos, bool blackHoleIsActive, int maxCount)
    {
        // 변수 빌드 & 캐싱
        (Vector2 cameraMinPos, Vector2 cameraMaxPos) = URange.GetCameraBounds2D(_camera);
        float removeDistance = _removeDistance;
        // 모든 구슬 반복
        for (int i = 0; i < maxCount; ++i) {
            // 비활성화 구슬 거르기
            if (!beadsIsActive[i]) {
                continue;
            }
            Vector2 beadPos = beadsPos[i];
            // 블랙홀에 흡수
            if (blackHoleIsActive && IsInBlackHole(beadPos, blackHolePos, removeDistance)) {
                beadsIsActive[i] = false;
                continue;
            }
            // 카메라 밖에서 삭제
            if (IsOutsideCamera(beadPos, cameraMinPos, cameraMaxPos)) {
                beadsIsActive[i] = false;
            }
        }
    }
    #endregion
}
