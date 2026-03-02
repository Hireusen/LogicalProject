using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 머리를 제외한 모든 지렁이 구슬이 기록된 좌표로 이동합니다.
/// </summary>
public class WormHeadFollower : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("사용자 정의 설정")]
    [SerializeField] private int _waitQueue = 5;
    #endregion

    #region ─────────────────────────▶ 메서드 ◀─────────────────────────
    public void BuildWormPrevPos(WormData worms)
    {
        int length = worms.length;
        if (length <= 1) {
            return;
        }
        // 캐싱
        var prevPos = worms.prevPos;
        var myPos = worms.pos;
        int needCount = _waitQueue;
        // 모든 지렁이 순회
        prevPos[0].Enqueue(myPos[0]);
        for (int i = 1; i < length - 1; ++i) {
            // 다음 지렁이 구슬이 큐를 쌓을 때까지 대기
            if (prevPos[i - 1].Count >= needCount) {
                prevPos[i].Enqueue(myPos[i]);
            }
        }
    }

    public void TransferWorms(WormData worms)
    {
        int length = worms.length;
        if(length <= 1) {
            return;
        }
        // 캐싱
        var prevPos = worms.prevPos;
        var myPos = worms.pos;
        int needCount = _waitQueue;
        // 머리를 제외한 지렁이 구슬 역순회
        for (int i = length - 1; i >= 1; --i) {
            Queue<Vector2> posQueue = prevPos[i - 1];
            // 다음 구슬이 큐를 쌓을때까지 대기
            if(posQueue.Count >= needCount) {
                myPos[i] = prevPos[i - 1].Dequeue();
            }
        }
    }
    #endregion
}
