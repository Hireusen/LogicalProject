using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 데이터 값을 게임 오브젝트의 Transform에 대입합니다.
/// </summary>
public class WormGraphic : MonoBehaviour
{
    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void UpdateWormsGraphic(WormData worms)
    {
        // 캐싱
        int wormCount = worms.length;
        // 지렁이 구슬 순회
        for (int i = 0; i < wormCount; ++i) {
            // 좌표 갱신
            worms.tr[i].position = worms.pos[i];
            // 크기 갱신
            float size = worms.size[i];
            Vector3 scale = new Vector3(size, size, 1f);
            worms.tr[i].localScale = scale;
        }
    }
    #endregion
}