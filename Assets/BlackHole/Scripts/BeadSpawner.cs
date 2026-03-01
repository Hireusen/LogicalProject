using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 구슬을 카메라 범위 내에서 활성화시킵니다.
/// </summary>
public class BeadSpawner : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnSizeMultiply = 2f;
    #endregion

    private float _nextSpawnTime;

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void TrySpawnBead(BeadData beads, Vector2 cameraMinPos, Vector2 cameraMaxPos)
    {
        float spawnInterval = _spawnInterval;
        float spawnSizeMultiply = _spawnSizeMultiply;
        // 다음 쿨타임까지 무한 반복
        while (_nextSpawnTime < Time.time) {
            // 쿨타임 적용
            _nextSpawnTime += spawnInterval;
            // 캐싱
            int activeCount = beads.activeCount;
            // 구슬 개수가 최대치 도달
            if (activeCount >= beads.capacity) {
                return;
            }
            // 변수 빌드
            float randX = Random.Range(cameraMinPos.x, cameraMaxPos.x);
            float randY = Random.Range(cameraMinPos.y, cameraMaxPos.y);
            // 생성
            beads.pos[activeCount] = new Vector2(randX, randY);
            beads.velocity[activeCount] = Vector2.zero;
            beads.sizeMultiply[activeCount] = spawnSizeMultiply;
            beads.activeCount++;
            beads.generatedCount++;
        }
    }

    private void Awake()
    {
        _nextSpawnTime = Time.time;
    }
    #endregion
}
