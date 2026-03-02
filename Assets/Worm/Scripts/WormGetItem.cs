using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 각 지렁이 구슬의 크기 값 이내에 아이템이 있다면 획득하고 효과를 적용합니다.
/// </summary>
public class WormGetItem : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private Transform _player;

    [Header("사용자 정의 설정")]
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 0f);
    #endregion

    #region ─────────────────────────▶ 접근자 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 내부 메서드 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    // 인스펙터 유효성 검사
    public void Verification() {

    }

    // 스크립트 내부 변수 초기화
    public void Initialize() {

    }

    // 외부에 전달할 데이터 생성
    public void DataBuilder() {

    }
    #endregion

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    private void Awake()
    {

    }

    private void OnEnable()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void LateUpdate()
    {

    }

    private void OnDisable()
    {

    }

    private void OnDestroy()
    {
        
    }

    private void Reset()
    {
        
    }

    private void OnValidate()
    {

    }
    #endregion
}
