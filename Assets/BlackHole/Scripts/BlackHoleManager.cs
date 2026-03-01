using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 데이터를 관리하고 스크립트를 호출합니다.
/// </summary>
public class BlackHoleManager : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Camera _camera;
    [SerializeField] private BlackHoleAction _blackHoleAction;
    [SerializeField] private BeadProcess _beadProcess;
    [SerializeField] private BeadRemover _beadRemover;
    [SerializeField] private BeadSpawner _beadSpawner;
    [SerializeField] private BeadRenderer _beadRenderer;
    [SerializeField] private BlackHoleUI _blackHoleUI;

    [Header("사용자 정의 설정")]
    [SerializeField] private int _maxBeadCount = 100;
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    private BeadData _data;
    #endregion

    #region ─────────────────────────▶ 내부 메서드 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    private void InitBeadData(int count)
    {
        // 플레이 도중 생성
        if (_data != null) {
            int oldCount = _data.capacity;
            int absorptionCount = _data.absorptionCount;
            int generatedCount = _data.generatedCount;
            _data = new BeadData(count);
            _data.absorptionCount = absorptionCount;
            _data.generatedCount = generatedCount;
            De.Print($"BeadData 재생성 완료 ({oldCount} → {count})");
        }
        // 최초 생성
        else {
            _data = new BeadData(count);
            De.Print($"BeadData 생성 완료 ({count})");
        }
    }
    #endregion

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    private void Update()
    {
        (Vector2 cameraMinPos, Vector2 cameraMaxPos) = URange.GetCameraBounds2D(_camera);
        _blackHoleAction.TryMove(cameraMinPos, cameraMaxPos);
        bool absorptionActive = _blackHoleAction.AbsorptionBeads(_data);
        Vector2 blackHolePos = _blackHoleAction.GetBlackHolePos();
        _beadProcess.TransferBeads(_data);
        _beadRemover.TryCleanBeads(_data, blackHolePos, absorptionActive, cameraMinPos, cameraMaxPos);
        _beadSpawner.TrySpawnBead(_data, cameraMinPos, cameraMaxPos);
        _beadRenderer.RenderBeads(_data.pos, _data.activeCount);
        _blackHoleUI.UpdateUI(_data);
    }

    private void Awake()
    {
        // 유효성 검사
        if (De.IsNull(_blackHoleAction)
            || De.IsNull(_beadProcess)
            || De.IsNull(_beadRemover)
            || De.IsNull(_beadSpawner)
            || De.IsNull(_beadRenderer)
            || De.IsNull(_blackHoleUI)
        ) {
            enabled = false;
        }
        // 데이터 생성
        InitBeadData(_maxBeadCount);
    }

    private void OnValidate()
    {
        InitBeadData(_maxBeadCount);
    }
    #endregion
}
