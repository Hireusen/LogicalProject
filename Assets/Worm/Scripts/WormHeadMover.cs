using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 머리 지렁이가 현재 바라보는 방향으로 움직이며, 벽에 반사됩니다.
/// </summary>
public class WormHeadMover : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private float _moveSpeed = 10f;
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────

    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    private static void TryCollideWall(Vector2 pos, Vector2 dir)
    {
        
    }

    public void MoveForward(WormData worms)
    {
        // 변수 빌드 & 캐싱
        Vector2 pos = worms.pos[0];
        Vector2 dir = worms.headDir;
        float size = worms.size[0];
        float remainDistance = _moveSpeed * Time.deltaTime;
        // 이동 한계 빌드
        float minX = worms.cameraMinPos.x + size;
        float maxX = worms.cameraMaxPos.x - size;
        float minY = worms.cameraMinPos.y + size;
        float maxY = worms.cameraMaxPos.y - size;
        // 이동을 완료할때까지 반복
        while(remainDistance > 0f) {
            Vector2 desiredPos = worms.pos[0] * dir * remainDistance;
            // 벽 충돌
            if(desiredPos.x < minX || desiredPos.x > maxX || desiredPos.y < minY || desiredPos.y > maxY) {

            }
        }
    }
    #endregion
}
