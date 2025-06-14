/*
 * X축 방향으로 반복되는 일반적인 배경 타일
 * 기준 좌표보다 scrollWidth만큼 왼쪽으로 벗어나면 오른쪽으로 이동
 */
using UnityEngine;

public class LoopBackground : BaseLoopBackground //BaseLoopBackground를 상속받아 기능 사용
{
    protected override void f_CheckAndLoop()
    {
        //기준 위치 기준으로 배경이 왼쪽으로 충분히 벗어나면 오른쪽으로 재배치
        if (transform.position.x + fScrollWidth < gReferenceTarget.position.x)
        {
            //화면 왼쪽을 벗어났다면 오른쪽으로 2배 이동시킴
            transform.position += Vector3.right * fScrollWidth * 2f;
        }
    }
}
