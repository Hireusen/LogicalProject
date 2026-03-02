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
    [SerializeField] private WormItemRemover _wormItemRemover;
    [SerializeField] private WormRotate _wormRotate;
    [SerializeField] private WormHeadMover _wormHeadMover;
    [SerializeField] private WormHeadFollower _wormHeadFollower;
    [SerializeField] private WormGraphic _wormGraphic;
    [SerializeField] private WormUI _wormUI;
    #endregion

    private WormData _data;

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    private void Update()
    {
        (_data.cameraMinPos, _data.cameraMaxPos) = URange.GetCameraBounds2D(_camera);
        _wormSpawner.WormDataSynchronize(_data);
        _wormItemSpawner.TrySpawnItem(_data);
        _wormItemSpawner.WormDataSynchronize(_data);
        // Remover
        _wormRotate.TryRotate(_data);
        _wormHeadMover.MoveHeadForward(_data);
        _wormHeadFollower.BuildWormPrevPos(_data);
        _wormHeadFollower.TransferWorms(_data);
        _wormGraphic.UpdateWormsGraphic(_data);
        _wormUI.UpdateUI(_data);
    }

    private void Awake()
    {
        // 유효성 검사
        if (De.IsNull(_camera)
            || De.IsNull(_wormSpawner)
            || De.IsNull(_wormItemSpawner)
            || De.IsNull(_wormItemRemover)
            || De.IsNull(_wormRotate)
            || De.IsNull(_wormHeadMover)
            || De.IsNull(_wormHeadFollower)
            || De.IsNull(_wormGraphic)
            || De.IsNull(_wormUI)
        ) {
            enabled = false;
        }
        // 지렁이 생성
        _data = new WormData();
        (_data.cameraMinPos, _data.cameraMaxPos) = URange.GetCameraBounds2D(_camera);
        _wormSpawner.WormDataSynchronize(_data);
        _wormSpawner.InitWorms(_data);
        _wormItemSpawner.WormDataSynchronize(_data);
        _wormItemSpawner.InitItemCount(_data);
    }
    #endregion
}
