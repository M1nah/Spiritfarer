using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Obj_Pool : MonoBehaviour
{

    [SerializeField] private GameObject prefabs;
    [SerializeField] private GameObject[] stars;

    [SerializeField] private int star_Count;

    [SerializeField] Vector3 pooling_Vec;

    private int count_Plus;

    [SerializeField] private float min_Pos_X;
    [SerializeField] private float max_Pos_X;
    // Start is called before the first frame update
    public int rand_num;
    public float time;
    public float rand;
    private void Awake()
    {
        stars = new GameObject[star_Count];
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = Instantiate(prefabs, pooling_Vec, Quaternion.identity, transform);
            stars[i].SetActive(false);
        }
        Spawn_Vec();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time > 2)
        {
            for(int i =0; i < star_Count; i++)
            {
                rand = Random.Range(min_Pos_X, max_Pos_X);
                rand_num = Random.Range(0, star_Count -1);

                stars[i].transform.position = new Vector3(rand, transform.position.y, 0);
                stars[rand_num].SetActive(true);
                time = 0;
            }
        }
    }
    void Spawn_Vec()
    {
        while (true)
        {
            rand = Random.Range(min_Pos_X, max_Pos_X);
            stars[count_Plus].SetActive(true);
            stars[count_Plus].transform.position = new Vector3(rand, transform.position.y);

            count_Plus++;

            if (count_Plus >= star_Count)
            {
                count_Plus = 0;
                return;
            }
        }
    }
}
