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

    private WormData _worms;

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
            pos.x = Random.Range(cameraMinOutPos.y, cameraMaxOutPos.y);
            pos.y = Tool.Chance(0.5f) ? cameraMinOutPos.x : cameraMaxOutPos.x;
        }
        return pos;
    }

    // 인스펙터 실시간 수정 호환성
    public void WormDataSynchronize(WormData worms)
    {
        if (worms != _worms) {
            _worms = worms;
            De.Print($"WormSpawner에서 WormData 주소를 새로 설정했습니다. ({_worms} → {worms})");
        }
    }

    private void DestoryWorms(WormData worms)
    {
        if(worms == null) {
            return;
        }
        int length = worms.length;
        for (int i = 0; i < length; ++i) {
            Destroy(worms.tr[i].gameObject);
        }
    }

    public void InitWorms(WormData worms)
    {
        if(worms == null) {
            return;
        }
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
            // 반복
            worms.tr = new Transform[length];
            for (int i = 0; i < length; ++i) {
                // 큐 초기화
                worms.prevPos[i] = new Queue<Vector2>(2);
                // 인스턴스, 트랜스폼
                GameObject go = Instantiate(_wormPrefab, pos, Quaternion.identity);
                worms.tr[i] = go.transform;
                // TMP 적용
                TextMeshPro tmp = go.GetComponentInChildren<TextMeshPro>();
                if(tmp == null) {
                    De.Print($"{i}번째 TMP가 존재하지 않습니다.", LogType.Error);
                    continue;
                }
                tmp.SetText(i.ToString());
            }
            De.Print($"좌표({pos})에 지렁이(* {length})를 생성했습니다.");
        }
    }

    private void OnValidate()
    {
        if (Application.isPlaying) {
            DestoryWorms(_worms);
            InitWorms(_worms);
        }
    }
    #endregion
}
