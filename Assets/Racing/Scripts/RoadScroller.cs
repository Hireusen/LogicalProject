using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 플레이어의 속도에 비례하여 두 장의 도로 이미지를 상하 스크롤합니다.
/// </summary>
public class RoadScroller : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Transform _roadA;
    [SerializeField] private Transform _roadB;
    #endregion

    private float _imageHeight;

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void InitRoad(CarData data)
    {
        // 스프라이트 높이 계산
        SpriteRenderer sr = _roadA.GetComponent<SpriteRenderer>();
        if (De.IsNull(sr)) {
            De.Print($"SpriteRenderer 어디갔나요?");
            return;
        }
        _imageHeight = sr.bounds.size.y;
        // 초기 배치 (A를 기준으로 B를 위에 배치)
        Vector2 posA = _roadA.position;
        Vector2 posB = posA;
        posB.y = posA.y + _imageHeight;
        _roadB.position = posB;
    }

    public void UpdateScroll(CarData data, in CarFrameInfo frame)
    {
        // 변수 빌드 & 캐싱
        float speed = data.velocity[0].y;
        float scrollDelta = speed * frame.deltaTime;
        Vector2 posA = _roadA.position;
        Vector2 posB = _roadB.position;
        // 이동
        posA.y -= scrollDelta;
        posB.y -= scrollDelta;
        // 화면 아래로 벗어난 이미지를 위로 재배치
        if (posA.y <= -_imageHeight) {
            posA.y = posB.y + _imageHeight;
        } else if (posB.y <= -_imageHeight) {
            posB.y = posA.y + _imageHeight;
        }
        // 적용
        _roadA.position = posA;
        _roadB.position = posB;
    }

    private void Awake()
    {
        if (De.IsNull(_roadA) || De.IsNull(_roadB)) {
            enabled = false;
        }
    }
    #endregion
}