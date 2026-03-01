using UnityEngine;
using TMPro;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// UI 텍스트를 갱신합니다.
/// </summary>
public class BlackHoleUI : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private TextMeshProUGUI _playTimeText; // 플레이 시간
    [SerializeField] private TextMeshProUGUI _absorptionCountText; // 블랙홀로 흡수한 구슬 수
    [SerializeField] private TextMeshProUGUI _activeBeadCountText; // 현재 씬에 존재하는 구슬 수
    [SerializeField] private TextMeshProUGUI _generatedBeadCountText; // 현재까지 생성된 구슬 수
    [SerializeField] private TextMeshProUGUI _frameText; // 프레임

    [Header("사용자 정의 설정")]
    [SerializeField] private float _fpsUpdateInterval = 0.1f;
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    private float _playStartTime;
    private int _playLastTime = -1;
    private int _absorptionLastCount = 0;
    private int _activeLastCount = 0;
    private int _generatedLastCount = 0;
    private float _fpsTimer = 0f;
    private int _frameCount = 0;
    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    public void UpdateUI(BeadData beads)
    {
        // 플레이 시간
        {
            int second = Mathf.CeilToInt(Time.time - _playStartTime);
            if (_playLastTime != second) {
                _playTimeText.SetText("플레이 시간 : {0}초", second);
                _playLastTime = second;
            }
        }
        // 블랙홀로 흡수한 구슬 수
        {
            int count = beads.absorptionCount;
            if (_absorptionLastCount != count) {
                _absorptionCountText.SetText("흡수한 구슬 수 : ({0})", count);
                _absorptionLastCount = count;
            }
        }
        // 현재 씬에 존재하는 구슬 수
        {
            int count = beads.activeCount;
            if (_activeLastCount != count) {
                _activeBeadCountText.SetText("존재하는 구슬 수 : ({0} / {1})", count, beads.capacity);
                _activeLastCount = count;
            }
        }
        // 현재까지 생성된 구슬 수
        {
            int count = beads.generatedCount;
            if (_generatedLastCount != count) {
                _generatedBeadCountText.SetText("생성된 구슬 수 : ({0})", count);
                _generatedLastCount = count;
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
                int fpsMulTen = (int)(_frameCount / _fpsTimer) * 10;
                // 텍스트 대입
                _frameText.SetText("{0} FPS", fpsMulTen * 0.1f);
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
            || De.IsNull(_absorptionCountText)
            || De.IsNull(_activeBeadCountText)
            || De.IsNull(_generatedBeadCountText)
            || De.IsNull(_frameText)
        ) {
            enabled = false;
        }
    }
    #endregion
}
