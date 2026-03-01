using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 블랙홀을 조작하여 이동하거나, 구슬을 흡수합니다.
/// </summary>
public class BlackHoleAction : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Transform _blackHole;

    [Header("사용자 정의 설정")]
    [SerializeField] private KeyCode _absorptionKey = KeyCode.Space;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _blackHoleForce = 11f;
    [SerializeField] private float _maxVelocity = 10f;
    [SerializeField] private Vector2 _blackHoleSize = new Vector2(0.5f, 0.5f);
    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    public Vector2 GetBlackHolePos() => _blackHole.position;

    public void TryMove(Vector2 cameraMinPos, Vector2 cameraMaxPos)
    {
        // 입력 받기
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // 입력이 있음
        if (h != 0f || v != 0f) {
            // 변수 빌드
            Vector2 axis = new Vector2(h, v);
            axis = Vector2.ClampMagnitude(axis, 1f);
            float movement = _moveSpeed * Time.deltaTime;
            // 목표 좌표 빌드
            Vector2 targetPos = _blackHole.position;
            float targetX = targetPos.x + axis.x * movement;
            float targetY = targetPos.y + axis.y * movement;
            targetPos.x = Mathf.Clamp(targetX, cameraMinPos.x + _blackHoleSize.x, cameraMaxPos.x - _blackHoleSize.x);
            targetPos.y = Mathf.Clamp(targetY, cameraMinPos.y + _blackHoleSize.y, cameraMaxPos.y - _blackHoleSize.y);
            // 적용
            _blackHole.position = targetPos;
        }
    }

    // 모든 구슬에 가속도 적용
    public bool AbsorptionBeads(BeadData beads)
    {
        // 키 입력이 없음
        if (!Input.GetKey(_absorptionKey)) {
            return false;
        }
        // 구슬이 없음
        int activeCount = beads.activeCount;
        if(activeCount <= 0) {
            return false;
        }
        // 변수 빌드 & 캐싱
        Vector2 blackHolePos = _blackHole.position;
        float blackHoleForce = _blackHoleForce * Time.deltaTime;
        float maxVelocity = _maxVelocity;
        float maxVelocitySQR = maxVelocity * maxVelocity;
        // 모든 구슬 순회
        for (int i = 0; i < activeCount; ++i) {
            // 구슬 → 블랙홀 변위
            Vector2 deltaPos = blackHolePos - beads.pos[i];
            float sqrDist = deltaPos.sqrMagnitude;
            // 가속도 적용
            if (sqrDist > 0.1f) {
                beads.velocity[i] += deltaPos * (blackHoleForce / sqrDist);
            }
            // 속도 제한
            if (beads.velocity[i].sqrMagnitude > maxVelocitySQR) {
                beads.velocity[i] = Vector2.ClampMagnitude(beads.velocity[i], maxVelocity);
            }
        }
        return true;
    }

    private void Awake()
    {
        // 유효성 검사
        if (De.IsNull(_blackHole)) {
            enabled = false;
        }
    }
    #endregion

    /* 제곱근 연산을 하는 예전 코드
    // 구슬 → 블랙홀 방향 빌드
    Vector2 dir = (blackHolePos - beads.pos[i]).normalized;
    // 가속도 계산
    Vector2 acceleration = blackHoleForce * dir;
    // 적용
    beads.velocity[i] = beads.velocity[i] + acceleration;
    */
}
