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
    #endregion

    private float _nextSpawnTime = Time.time;

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void TrySpawnBead(BeadData beads, Vector2 cameraMinPos, Vector2 cameraMaxPos)
    {
        // 구슬 개수가 최대치 도달
        int activeCount = beads.activeCount;
        if(activeCount >= beads.capacity) {
            return;
        }
        // 생성 쿨타임
        if(_nextSpawnTime > Time.time) {
            return;
        }
        _nextSpawnTime += _spawnInterval;
        // 변수 빌드
        float randX = Random.Range(cameraMinPos.x, cameraMaxPos.x);
        float randY = Random.Range(cameraMinPos.y, cameraMaxPos.y);
        // 생성
        beads.pos[activeCount] = new Vector2(randX, randY);
        beads.velocity[activeCount] = Vector2.zero;
        beads.activeCount++;
        beads.generatedCount++;
    }
    #endregion
}
