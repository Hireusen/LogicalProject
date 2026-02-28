using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 자동차의 상태를 변경합니다.
/// </summary>
public class CarMover : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("자동차 설정")]
    [Tooltip("차체 최소 Z각도")]
    [SerializeField] private float _minAngle = 240f;
    [Tooltip("차체 최대 Z각도")]
    [SerializeField] private float _maxAngle = 300f;
    [Tooltip("X 이동 시 초당 각도 변화량")]
    [SerializeField] private float _anglePerSeconds = 20f;
    [Tooltip("가속, 감속 시 초당 속도 변화량")]
    [SerializeField] private float _speedPerSeconds = 5f;
    [Tooltip("X 이동량 배율")]
    [SerializeField] private float _horizontalMultifly = 1f;
    [Tooltip("Y 이동량 배율")]
    [SerializeField] private float _verticalMultifly = 1f;
    #endregion

    #region ─────────────────────────▶ 접근자 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 내부 메서드 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    private bool CollisionCheck(int carCount)
    {
        for (int i = 0; i < carCount; ++i) {

        }
        return true;
    }

    private float CalcSpeedDelta(float applySpeed)
    {
        return applySpeed * _speedPerSeconds * Time.deltaTime;
    }

    private float CalcMoveYDelta(float speed)
    {
        return speed * _verticalMultifly * Time.deltaTime;
    }

    private float CalcMoveXDelta(float applyX)
    {
        return applyX * _horizontalMultifly * Time.deltaTime;
    }

    private float CalcAngle(float angle, float desiredAngle)
    {
        if (angle < desiredAngle) {
            angle += _anglePerSeconds * Time.deltaTime;
        } else if (angle > desiredAngle) {
            angle -= _anglePerSeconds * Time.deltaTime;
        }
        return Mathf.Clamp(angle, _minAngle, _maxAngle);
    }

    public void UpdateCars(CarInfo[] cars, int carCount)
    {
        // 이동
        for (int i = 0; i < carCount; ++i) {
            ref CarInfo car = ref cars[i];
            // 죽음 검사
            if (car.isDead) {
                continue;
            }
            // 이동
            car.speed += CalcSpeedDelta(car.applySpeed);
            car.posY += CalcMoveYDelta(car.speed);
            car.posX += CalcMoveXDelta(car.applyX);
            car.angle = CalcAngle(car.angle, car.desiredAngle);
            // 충돌
            if (CollisionCheck(carCount)) {

            }
        }
    }
    #endregion

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    private void OnValidate()
    {
        if (_anglePerSeconds <= 0f) {
            _anglePerSeconds = 0.1f;
        }
        if (_speedPerSeconds <= 0f) {
            _speedPerSeconds = 0.1f;
        }
        if (_horizontalMultifly <= 0f) {
            _horizontalMultifly = 0.1f;
        }
        if (_verticalMultifly <= 0f) {
            _verticalMultifly = 0.1f;
        }
    }
    #endregion
}
