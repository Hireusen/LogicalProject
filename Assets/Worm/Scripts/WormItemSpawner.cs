using System;
using UnityEngine;

/// <summary>
/// 빈 오브젝트에 부착하는 C# 스크립트입니다.
/// 카메라 범위 내에 랜덤 위치에 아이템을 생성합니다.
/// </summary>
public class WormItemSpawner : MonoBehaviour
{
    #region ─────────────────────────▶ 인스펙터 ◀─────────────────────────
    [Header("필수 요소 등록")]
    [SerializeField] private GameObject _itemPrefab;

    [Header("사용자 정의 설정")]
    [SerializeField] private float _firstItemSpawnTime = 5f;
    [SerializeField] private float _spawnInterval = 10f;
    [SerializeField] private float _increaseSize = 0.1f;
    [SerializeField] private float _itemSpawnPadding = 0.1f;
    [SerializeField] private int _itemMaxCount = 10;
    #endregion

    #region ─────────────────────────▶ 내부 변수 ◀─────────────────────────
    private float _nextSpawnTime;
    private WormData _worms;
    private bool _isMaxCount;
    #endregion

    #region ─────────────────────────▶ 내부 메서드 ◀─────────────────────────
    private void DestoryItemObjects(WormData worms, int startIndex, int length)
    {
        for (int i = startIndex; i < length; ++i) {
            if(worms.itemGO[i] != null) {
                GameObject.Destroy(worms.itemGO[i]);
                worms.itemGO[i] = null;
            }
        }
    }

    private void ResizeItemCount(WormData worms, int count)
    {
        // 어떻게 왔지?
        if (worms == null) {
            De.Print("존재하지 않는 WormData를 ResizeItemCount에서 받았습니다.", LogType.Assert);
            return;
        }
        // 캐싱
        int oldCount = worms.itemGO.Length;
        // 생성
        if (worms.itemGO == null) {
            worms.itemGO = new GameObject[count];
            worms.itemPos = new Vector2[count];
            worms.activeItemCount = 0;
            De.Print($"아이템 배열을 새로 생성했습니다. (count == {count})");
        }
        // 재생성
        else if (oldCount != count) {
            // 새 배열 길이가 활성화된 아이템 수보다 적을 경우 작업이 필요
            int activeCount = worms.activeItemCount;
            int copyCount = activeCount;
            if (activeCount > count) {
                copyCount = count; // 새 배열 길이를 넘어서 복사하지 않도록
                DestoryItemObjects(worms, copyCount, oldCount); // 새 배열 길이를 초과한 아이템 오브젝트 파괴
            }
            // 새 배열 생성
            GameObject[] newGO = new GameObject[count];
            Vector2[] newPos = new Vector2[count];
            // 요소 복사
            Array.Copy(worms.itemGO, newGO, copyCount);
            Array.Copy(worms.itemPos, newPos, copyCount);
            // 새 배열로 포인터 교체
            worms.itemGO = newGO;
            worms.itemPos = newPos;
            // 활성화 아이템 수 재설정
            worms.activeItemCount = copyCount;
            De.Print($"아이템 배열을 재생성했습니다. ({oldCount} → {count})");
        } else {
            De.Print($"아이템 배열 재생성을 시도했지만 이미 배열 크기가 동일합니다. ({count})");
        }
    }
    #endregion

    #region ─────────────────────────▶ 외부 메서드 ◀─────────────────────────
    // 시작 시 최초 1회 실행
    public void InitItemCount(WormData worms)
    {
        worms.itemGO = new GameObject[_itemMaxCount];
        worms.itemPos = new Vector2[_itemMaxCount];
        worms.activeItemCount = 0;
        De.Print($"아이템 배열을 새로 생성했습니다. (count == {_itemMaxCount})");
    }

    // 인스펙터 실시간 수정 호환성
    public void WormDataSynchronize(WormData worms)
    {
        if (worms != _worms) {
            _worms = worms;
            De.Print($"WormData 주소를 새로 설정했습니다. ({_worms} → {worms})");
        }
    }

    public void TrySpawnItem(WormData worms)
    {
        // 쿨타임 확인
        if (_nextSpawnTime > Time.time) {
            return;
        }
        // 최대 개수 확인
        int curCount = worms.activeItemCount;
        if (curCount >= _itemMaxCount) {
            _isMaxCount = true;
            return;
        }
        // 쿨타임 적용
        if (_isMaxCount) {
            _nextSpawnTime = Time.time + _spawnInterval;
            _isMaxCount = false;
        } else {
            _nextSpawnTime += _spawnInterval;
        }
        // 카메라 내 랜덤 위치
        float minX = worms.cameraMinPos.x + _itemSpawnPadding;
        float maxX = worms.cameraMaxPos.x - _itemSpawnPadding;
        float minY = worms.cameraMinPos.y + _itemSpawnPadding;
        float maxY = worms.cameraMaxPos.y - _itemSpawnPadding;
        float x = UnityEngine.Random.Range(minX, maxX);
        float y = UnityEngine.Random.Range(minY, maxY);
        Vector2 pos = new Vector2(x, y);
        // 아이템 생성
        worms.itemGO[curCount] = Instantiate(_itemPrefab, pos, Quaternion.identity);
        worms.itemPos[curCount] = pos;
        worms.activeItemCount++;
        worms.itemSpawnCount++;
    }
    #endregion

    #region ─────────────────────────▶ 메시지 함수 ◀─────────────────────────
    private void Awake()
    {
        _nextSpawnTime = Time.time + _firstItemSpawnTime;
        _isMaxCount = false;
    }

    private void OnValidate()
    {
        if (Application.isPlaying) {
            ResizeItemCount(_worms, _itemMaxCount);
        }
        // 유효성 검사
        if (_firstItemSpawnTime < 0f) {
            _firstItemSpawnTime = 0f;
        }
        if (_spawnInterval <= 0f) {
            _spawnInterval = 0.1f;
        }
    }
    #endregion
}
