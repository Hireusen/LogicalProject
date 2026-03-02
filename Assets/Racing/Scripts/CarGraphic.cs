using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 플레이어의 Y 좌표를 중심으로 모든 자동차 오브젝트를 재배치합니다.
/// </summary>
public class CarGraphic : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private float _playerScreenY = -2f;
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void UpdateGraphic(CarData data, in CarFrameInfo frame)
    {
        // 캐싱
        int carCount = data.activeCarCount;
        float playerDataY = data.pos[0].y;
        float offsetY = _playerScreenY - playerDataY;
        // 모든 자동차 순회
        for (int i = 0; i < carCount; ++i) {
            // 좌표 갱신
            Vector2 pos = data.pos[i];
            Vector2 screenPos = new Vector2(pos.x, pos.y + offsetY);
            data.tr[i].position = screenPos;
            // 회전 갱신
            float rot = data.rotation[i];
            data.tr[i].rotation = Quaternion.Euler(0f, 0f, rot);
        }
    }
    #endregion
}