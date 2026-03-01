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
    // 구슬이 블랙홀 내부에 존재
    private static bool IsInBlackHole(Vector2 beadPos, Vector2 blackHolePos, float removeDistance)
    {
        return URange.InCircle(beadPos, blackHolePos, removeDistance);
    }

    // 구슬이 카메라 밖에 존재
    private static bool IsOutsideCamera(Vector2 beadPos, Vector2 cameraMinPos, Vector2 cameraMaxPos)
    {
        return !URange.InRect(beadPos, cameraMinPos, cameraMaxPos);
    }

    // 스왑 팝으로 덮어씌우기
    private static void RemoveBead(int index, BeadData beads)
    {
        beads.activeCount--;
        int activeCount = beads.activeCount;
        beads.pos[index] = beads.pos[activeCount];
        beads.velocity[index] = beads.velocity[activeCount];
    }
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void CleanBeads(BeadData beads, Vector2 blackHolePos, bool blackHoleActive)
    {
        // 변수 빌드 & 캐싱
        (Vector2 cameraMinPos, Vector2 cameraMaxPos) = URange.GetCameraBounds2D(_camera);
        float removeDistance = _removeDistance;
        // 모든 구슬 역순회
        for (int i = beads.activeCount - 1; i >= 0; --i) {
            Vector2 beadPos = beads.pos[i];
            // 블랙홀에 흡수
            if (blackHoleActive && IsInBlackHole(beadPos, blackHolePos, removeDistance)) {
                RemoveBead(i, beads);
                continue;
            }
            // 카메라 밖에서 삭제
            if (IsOutsideCamera(beadPos, cameraMinPos, cameraMaxPos)) {
                RemoveBead(i, beads);
            }
        }
    }

    private void Awake()
    {
        De.IsNull(_camera);
    }
    #endregion
}
?