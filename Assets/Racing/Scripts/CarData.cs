using UnityEngine;

/// <summary>
/// 자동차 게임에 관한 데이터를 담는 클래스입니다.
/// </summary>
public class CarData
{
    // 자동차
    public Vector2[] pos;
    public Vector2[] velocity;
    public float[] rotation; // 현재 회전값을 데이터로도 저장
    public float[] targetSpeed; // 컴퓨터 전용 : 컴퓨터 차량 목표 속도
    public Transform[] tr;
    public GameObject[] go;
    public int activeCarCount;
    // 스킬
    public bool shieldActive;
    public bool rushActive;
    public float shieldEndTime; // 스킬 종료 시간
    public float rushEndTime;
    public float shieldNextTime; // 다음 스킬 사용 가능 시간
    public float rushNextTime;
    // 도로
    public float roadMinX;
    public float roadMaxX;
    // 통계
    public int collisionCount; // 충돌 횟수
    public float traveledDistance; // 주행한 거리

    public CarData()
    {
        this.pos = null;
        this.velocity = null;
        this.rotation = null;
        this.targetSpeed = null;
        this.tr = null;
        this.go = null;
        this.activeCarCount = 0;
        this.shieldActive = false;
        this.rushActive = false;
        this.shieldEndTime = 0f;
        this.rushEndTime = 0f;
        this.shieldNextTime = 0f;
        this.rushNextTime = 0f;
        this.roadMinX = 0f;
        this.roadMaxX = 0f;
        this.collisionCount = 0;
        this.traveledDistance = 0f;
    }
}