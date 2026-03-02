using TMPro;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 텍스트를 갱신합니다.
/// </summary>
public class WormUI : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private TextMeshProUGUI _playTimeText; // 플레이 시간
    [SerializeField] private TextMeshProUGUI _wormCountText; // 현재 씬에 존재하는 지렁이 구슬 수
    [SerializeField] private TextMeshProUGUI _itemSpawnCountText; // 현재까지 소환된 아이템 수
    [SerializeField] private TextMeshProUGUI _frameText; // 프레임

    [Header("사용자 정의 설정")]
    [SerializeField] private float _fpsUpdateInterval = 0.1f;
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    private float _playStartTime;
    private int _playLastTime = -1;
    private int _wormLastCount = 0;
    private int _itemSpawnLastCount = 0;
    private float _fpsTimer = 0f;
    private int _frameCount = 0;
    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    public void UpdateUI(WormData worms)
    {
        // 플레이 시간
        {
            int second = Mathf.CeilToInt(Time.time - _playStartTime);
            if (_playLastTime != second) {
                _playTimeText.SetText("플레이 시간 : {0}초", second);
                _playLastTime = second;
            }
        }
        // 현재 씬에 존재하는 지렁이 구슬 수
        {
            int count = worms.length;
            if (_wormLastCount != count) {
                _wormCountText.SetText("지렁이 구슬 수 : ({0})", count);
                _wormLastCount = count;
            }
        }
        // 현재까지 소환된 아이템 수
        {
            int count = worms.itemSpawnCount;
            if (_itemSpawnLastCount != count) {
                _itemSpawnCountText.SetText("소환된 구슬 수 : ({0})", count);
                _itemSpawnLastCount = count;
            }
        }
        // FPS 측정
        {
            // 프레임 누적
            _fpsTimer += Time.unscaledDeltaTime;
            _frameCount++;
            // 쿨타임 검사
            if (_fpsTimer >= _fpsUpdateInterval) {
                // 평균 FPS
                int fps = (int)(_frameCount / _fpsTimer);
                // 텍스트 대입
                _frameText.SetText("{0} FPS", fps);
                // 프레임 초기화
                _fpsTimer = 0f;
                _frameCount = 0;
            }
        }
    }

    private void Awake()
    {
        _playStartTime = Time.time;
        // 유효성 검사
        if (De.IsNull(_playTimeText)
            || De.IsNull(_wormCountText)
            || De.IsNull(_itemSpawnCountText)
            || De.IsNull(_frameText)
        ) {
            enabled = false;
        }
    }
    #endregion
}
