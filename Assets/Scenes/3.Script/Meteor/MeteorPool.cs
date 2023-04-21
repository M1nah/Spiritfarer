using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeteorPool : MonoBehaviour //오브젝트 풀링으로 별똥별 뿌리기 
{
    //프리펩들을 보관할 변수 1
    public GameObject[] meteorPrefabs;
    //풀 담당을 하는 리스트
    List<GameObject>[] meteorPools;

    private void Awake()
    {
        meteorPools = new List<GameObject>[meteorPrefabs.Length];

        for (int index = 0; index < meteorPools.Length; index++)
        {
            meteorPools[index] = new List<GameObject>();
        }
    }

    //↓게임오브젝트와 리턴을 미리 작성
    public GameObject Get(int index) //가져올 오브젝트 종류를 결정하는 매개변수 추가
    {
        GameObject select = null; //풀 안에서 놀고있는 오브젝트 하나를 '선택'해서 반환한다.

        //선택한 풀의 놀고 있는(비활성화 된) 게임 오브젝트 접근

        foreach(GameObject item in meteorPools[index])
        {
            if (!item.activeSelf) //내용물 오브젝트가(item) 비활성화(대기 상태)인지 확인
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //만약 풀에 남아있는 오브젝트를 못 찾았다면(활성화)
        if (select ==null)
        {
            //그렇다면 새롭게 생성하고 select 변수에 할당 //Instantiate=원본 오브젝트를 복제하여 장면에 생성하는 함수
            select = Instantiate(meteorPrefabs[index], transform);
            meteorPools[index].Add(select);
        }
        return select;
    }

}
