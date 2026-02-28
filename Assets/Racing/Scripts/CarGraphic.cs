using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ~ 오브젝트에 부착하는 C# 스크립트입니다.
/// ~ 합니다.
/// </summary>
public class CarGraphic : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("사용자 정의 설정")]
    [SerializeField] private Vector2 _screenCenter = new Vector2(0f, 1f);
    [SerializeField] private float _screenWidth = 100f;
    [SerializeField] private float _screenHeight = 100f;
    [SerializeField] private float _lineStartWidth = 0.1f;
    [SerializeField] private float _lineEndWidth = 0.15f;
    #endregion

    #region ─────────────────────────▶ 접근자 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    private Vector2 _centerPos;
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    #endregion

    #region ─────────────────────────▶ 내부 메서드 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    public void UpdateCenterPos(float x, float y)
    {
        _centerPos.x = x;
        _centerPos.y = y;
    }

    public void UpdateCarGraphic(ref CarInfo[] cars, int carCount)
    {
        for (int i = 0; i < carCount; ++i) {
            ref CarInfo car = ref cars[i];
            if (car.isDead) {
                continue;
            }
            // 그래픽 그리기
        }
    }

    public void DrawScreenBounds()
    {
        if (De.IsNull(_lineRenderer)) {
            return;
        }
        _lineRenderer.SetPosition(0, new Vector3(_minX, _minY, 0f)); // 좌하단
        _lineRenderer.SetPosition(1, new Vector3(_minX, _maxY, 0f)); // 좌상단
        _lineRenderer.SetPosition(2, new Vector3(_maxX, _maxY, 0f)); // 우상단
        _lineRenderer.SetPosition(3, new Vector3(_maxX, _minY, 0f)); // 우하단
    }

    [ContextMenu("라인 렌더러 갱신")]
    private void Start()
    {
        // 라인 렌더러
        if (De.IsNull(_lineRenderer)) {
            return;
        }
        _lineRenderer.positionCount = 4;
        _lineRenderer.loop = true;
        _lineRenderer.startWidth = _lineStartWidth;
        _lineRenderer.endWidth = _lineEndWidth;
        // 캐싱
        float halfX = _screenWidth * 0.5f;
        float halfY = _screenHeight * 0.5f;
        _minX = _screenCenter.x - halfX;
        _maxX = _screenCenter.x + halfX;
        _minY = _screenCenter.y - halfY;
        _maxY = _screenCenter.y + halfY;
    }
    #endregion
}
