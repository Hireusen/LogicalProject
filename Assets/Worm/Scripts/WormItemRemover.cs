using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 각 지렁이 구슬의 크기 값 이내에 아이템이 있다면 획득하고 효과를 적용합니다.
/// 아이템이 화면을 벗어나면 삭제 처리합니다.
/// </summary>
public class WormItemRemover : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private float _increaseSize = 0.1f;
    [SerializeField] private float _rangeMultiply = 1.5f;
    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    private static void RemoveItem(int index, WormData worms)
    {
        int last = worms.activeItemCount - 1;
        int number = worms.itemNumber[index];
        Destroy(worms.itemGO[index]);
        worms.itemGO[index] = worms.itemGO[last];
        worms.itemPos[index] = worms.itemPos[last];
        worms.itemNumber[index] = worms.itemNumber[last];
        worms.activeItemCount = last;
        De.Print($"{number}번 아이템을 삭제했습니다.");
    }

    public void TryRemoveItem(WormData worms)
    {
        // 캐싱
        Vector2 minPos = worms.cameraMinPos;
        Vector2 maxPos = worms.cameraMaxPos;
        Vector2[] itemsPos = worms.itemPos;
        int itemCount = worms.activeItemCount;
        // 아이템 순환
        for (int i = itemCount - 1; i >= 0; --i) {
            // 아이템이 카메라 바깥에 존재
            if (itemsPos[i].x < minPos.x
                || itemsPos[i].x > maxPos.x
                || itemsPos[i].y < minPos.y
                || itemsPos[i].y > maxPos.y
            ) {
                RemoveItem(i, worms);
            }
        }
    }

    public void ProcessItem(WormData worms)
    {
        // 캐싱
        int wormCount = worms.length;
        Vector2[] itemsPos = worms.itemPos;
        int[] itemsNumber = worms.itemNumber;
        // 지렁이 구슬 순회
        for (int i = 0; i < wormCount; ++i) {
            // 변수 빌드
            Vector2 pos = worms.pos[i];
            float dist = worms.size[i] * _rangeMultiply;
            int itemCount = worms.activeItemCount;
            // 아이템 순회
            for (int j = itemCount - 1; j >= 0; --j) {
                // 아이템 접촉 성공
                if (URange.InCircle(pos, itemsPos[j], dist)) {
                    worms.size[itemsNumber[j]] += _increaseSize;
                    RemoveItem(j, worms);
                }
            }
        }
    }
    #endregion
}
