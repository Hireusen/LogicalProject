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

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    public void MoveHeadForward(WormData worms)
    {
        // 변수 빌드 & 캐싱
        Vector2 pos = worms.pos[0];
        Vector2 dir = worms.headDir;
        float size = worms.size[0];
        float remainDistance = _moveSpeed * (1f / 300f);
        Vector2 desiredPos = pos;
        // 이동 한계 변수 빌드
        float minX = worms.cameraMinPos.x + size;
        float maxX = worms.cameraMaxPos.x - size;
        float minY = worms.cameraMinPos.y + size;
        float maxY = worms.cameraMaxPos.y - size;
        // 맵을 벗어났을 경우 원점으로 복귀
        if (pos.x < minX || pos.x > maxX || pos.y < minY || pos.y > maxY) {
            dir = (Vector2.zero - pos).normalized;
            desiredPos = pos + (dir * remainDistance);
            remainDistance = 0f;
        }
        // 안전장치
        int safe = 10;
        // 이동을 완료할때까지 반복
        while (remainDistance > 0f && safe > 0) {
            safe--;
            // 변수 준비
            Vector2 vDist = new Vector2(float.MaxValue, float.MaxValue); // 벽까지의 거리
            Vector2 normalX = Vector2.zero;
            Vector2 normalY = Vector2.zero;
            // X축 교차점 계산
            if (dir.x < 0) {
                vDist.x = (minX - pos.x) / dir.x; // 거리를 방향으로 나누어  빗변 길이 계산
                normalX = Vector2.right;
            }
            else if (dir.x > 0) {
                vDist.x = (maxX - pos.x) / dir.x;
                normalX = Vector2.left;
            }
            // Y축 교차점 계산
            if (dir.y < 0) {
                vDist.y = (minY - pos.y) / dir.y;
                normalY = Vector2.up;
            }
            else if (dir.y > 0) {
                vDist.y = (maxY - pos.y) / dir.y;
                normalY = Vector2.down;
            }
            // 어째서?
            if(vDist == Vector2.zero) {
                De.Print("현재 지렁이의 방향이 존재하지 않습니다.", LogType.Error);
                break;
            }
            // 어떤 벽에 부딪히는가?
            float distance = 0f;
            Vector2 normal;
            if(vDist.x < vDist.y) {
                distance = vDist.x;
                normal = normalX;
            } else {
                distance = vDist.y;
                normal = normalY;
            }
            // 부딪히지 않고 도착
            if (distance >= remainDistance) {
                desiredPos = pos + (dir * remainDistance);
                remainDistance = 0f;
                break;
            }
            // 경계까지 이동 및 방향 변경
            pos = pos + (dir * distance);
            desiredPos = pos;
            dir = Vector2.Reflect(dir, normal).normalized; // 혹시 모르니까 정규화
            remainDistance -= distance;
        }
        // 이동 최종 적용
        worms.pos[0] = desiredPos;
        worms.headDir = dir;
        // 디버그
        if(safe <= 0) {
            De.Print("한 프레임에 충돌이 10번 이상 발생했습니다.", LogType.Error);
        }
    }
    #endregion
}
