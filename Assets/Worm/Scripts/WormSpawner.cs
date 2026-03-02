using System;
using System.Collections.Generic;
using UnityEngine;
abstract;
/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 지렁이를 맵 바깥에서 생성하여 원점으로 향하도록 합니다.
/// </summary>
public class WormSpawner : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Transform _player;

    [Header("사용자 정의 설정")]
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 0f);
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────

    #endregion
}
