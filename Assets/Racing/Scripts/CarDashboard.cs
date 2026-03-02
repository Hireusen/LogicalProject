using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 라인 렌더러로 계기판 바늘을 그려 현재 속도를 표시합니다.
/// </summary>
public class CarDashboard : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private LineRenderer _line;

    [Header("사용자 정의 설정")]
    [SerializeField] private float _startAngle = 225f;
    [SerializeField] private float _endAngle = -45f;
    [SerializeField] private float _lineDistance = 1f;
    [SerializeField] private float _maxSpeed = 200f;
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    private Transform _lineTr;
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void UpdateNeedle(CarData data)
    {
        // 변수 빌드 & 캐싱
        float speed = data.velocity[0].y;
        Vector2 origin = _lineTr.position;
        float t = Mathf.Clamp01(speed / _maxSpeed);
        float angle = Mathf.Lerp(_startAngle, _endAngle, t) * Mathf.Deg2Rad;
        // 바늘 끝점 계산
        Vector2 endpoint = origin + new Vector2(
            Mathf.Cos(angle) * _lineDistance,
            Mathf.Sin(angle) * _lineDistance
        );
        // 라인 적용
        _line.SetPosition(0, origin);
        _line.SetPosition(1, endpoint);
    }
    #endregion

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    private void Awake()
    {
        if (De.IsNull(_line)) {
            enabled = false;
            return;
        }
        _lineTr = _line.transform;
        _line.positionCount = 2;
    }
    #endregion
}