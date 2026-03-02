using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 머리를 제외한 모든 지렁이 구슬이 기록된 좌표로 이동합니다.
/// </summary>
public class WormHeadFollower : MonoBehaviour
{
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
        // 모든 지렁이 순회
        for (int i = 0; i < length - 1; ++i) {
            prevPos[i].Enqueue(myPos[i]);
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
        // 머리를 제외한 지렁이 구슬 역순회
        for (int i = length - 1; i >= 1; --i) {
            myPos[i] = prevPos[i - 1].Dequeue();
        }
    }
    #endregion
}
