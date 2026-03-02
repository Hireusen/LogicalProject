using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 데이터를 보유하고, 지렁이 스크립트를 호출합니다.
/// </summary>
public class WormManager : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Camera _camera;
    [SerializeField] private WormSpawner _wormSpawner;
    [SerializeField] private WormItemSpawner _wormItemSpawner;
    [SerializeField] private WormGetItem _wormGetItem;
    [SerializeField] private WormRotate _wormRotate;
    [SerializeField] private WormHeadMover _wormHeadMover;
    [SerializeField] private WormHeadFollower _wormHeadFollower;
    [SerializeField] private WormGraphic _wormGraphic;
    [SerializeField] private WormUI _wormUI;

    [Header("사용자 정의 설정")]
    [SerializeField] private int _wormLength = 30;
    #endregion

    private WormData _data;

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
        (_data.cameraMinPos, _data.cameraMaxPos) = URange.GetCameraBounds2D(_camera);
        
    }

    private void Awake()
    {
        // 유효성 검사
        if (De.IsNull(_camera)
            || De.IsNull(_wormSpawner)
            || De.IsNull(_wormItemSpawner)
            || De.IsNull(_wormGetItem)
            || De.IsNull(_wormRotate)
            || De.IsNull(_wormHeadMover)
            || De.IsNull(_wormHeadFollower)
            || De.IsNull(_wormGraphic)
            || De.IsNull(_wormUI)
        ) {
            enabled = false;
        }
    }

    private void OnValidate()
    {
        
    }
    #endregion
}
