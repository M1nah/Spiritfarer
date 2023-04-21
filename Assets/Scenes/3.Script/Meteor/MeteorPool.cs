using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeteorPool : MonoBehaviour //������Ʈ Ǯ������ ���˺� �Ѹ��� 
{
    //��������� ������ ���� 1
    public GameObject[] meteorPrefabs;
    //Ǯ ����� �ϴ� ����Ʈ
    List<GameObject>[] meteorPools;

    private void Awake()
    {
        meteorPools = new List<GameObject>[meteorPrefabs.Length];

        for (int index = 0; index < meteorPools.Length; index++)
        {
            meteorPools[index] = new List<GameObject>();
        }
    }

    //����ӿ�����Ʈ�� ������ �̸� �ۼ�
    public GameObject Get(int index) //������ ������Ʈ ������ �����ϴ� �Ű����� �߰�
    {
        GameObject select = null; //Ǯ �ȿ��� ����ִ� ������Ʈ �ϳ��� '����'�ؼ� ��ȯ�Ѵ�.

        //������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���� ������Ʈ ����

        foreach(GameObject item in meteorPools[index])
        {
            if (!item.activeSelf) //���빰 ������Ʈ��(item) ��Ȱ��ȭ(��� ����)���� Ȯ��
            {
                //�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //���� Ǯ�� �����ִ� ������Ʈ�� �� ã�Ҵٸ�(Ȱ��ȭ)
        if (select ==null)
        {
            //�׷��ٸ� ���Ӱ� �����ϰ� select ������ �Ҵ� //Instantiate=���� ������Ʈ�� �����Ͽ� ��鿡 �����ϴ� �Լ�
            select = Instantiate(meteorPrefabs[index], transform);
            meteorPools[index].Add(select);
        }
        return select;
    }

}
