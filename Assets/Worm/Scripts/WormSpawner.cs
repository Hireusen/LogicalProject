using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 지렁이를 맵 바깥에서 생성하여 원점으로 향하도록 합니다.
/// </summary>
public class WormSpawner : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private GameObject _wormPrefab;

    [Header("사용자 정의 설정")]
    [SerializeField] private int _wormLength = 30;
    [SerializeField] private float _wormSize = 1f;
    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    private static Vector2 GetRandomCameraOutPos(Vector2 cameraMinOutPos, Vector2 cameraMaxOutPos)
    {
        Vector2 pos;
        // 상하 벽
        if (Tool.Chance(0.5f)) {
            pos.x = Random.Range(cameraMinOutPos.x, cameraMaxOutPos.x);
            pos.y = Tool.Chance(0.5f) ? cameraMinOutPos.y : cameraMaxOutPos.y;
        }
        // 좌우 벽
        else {
            pos.x = Random.Range(cameraMinOutPos.x, cameraMaxOutPos.x);
            pos.y = Tool.Chance(0.5f) ? cameraMinOutPos.y : cameraMaxOutPos.y;
        }
        return pos;
    }

    public void CreateWorms(WormData worms)
    {
        float size = _wormSize;
        int length = _wormLength;
        // 생성 좌표 빌드
        Vector2 cameraMinOutPos = worms.cameraMinPos;
        Vector2 cameraMaxOutPos = worms.cameraMaxPos;
        cameraMinOutPos.x -= size;
        cameraMinOutPos.y -= size;
        cameraMaxOutPos.x += size;
        cameraMaxOutPos.y += size;
        Vector2 pos = GetRandomCameraOutPos(cameraMinOutPos, cameraMaxOutPos);
        // 방향 빌드
        Vector2 dir = (Vector2.zero - pos).normalized;
        // 데이터 적용
        {
            // 기본 정보
            worms.length = length;
            worms.headDir = dir;
            // 좌표
            worms.pos = new Vector2[length];
            System.Array.Fill(worms.pos, pos);
            // 초기화
            worms.prevPos = new Queue<Vector2>[length];
            // 크기
            worms.size = new float[length];
            System.Array.Fill(worms.size, size);
            // 트랜스폼 및 생성
            worms.tr = new Transform[length];
            for (int i = 0; i < length; ++i) {
                GameObject go = Instantiate(_wormPrefab, pos, Quaternion.identity);
                TextMeshProUGUI tmp = go.GetComponentInChildren<TextMeshProUGUI>();
                tmp.SetText(i.ToString());
                worms.tr[i] = go.transform;
            }
        }
    }
    #endregion
}
