using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorPoolingMove : MonoBehaviour //별똥별 뿌리는 오브젝트 움직임 
{
    public float starTime;

    public float mintX, maxX;
    public float moveSpeed;
    
    private int sign = -1;

    private void Update()
    {
        if (Time.time >= starTime)
        {
            //이동로직 처리
            transform.position += new Vector3(moveSpeed * Time.deltaTime * sign, 0, 0);
        
            if(transform.position.x <=mintX || transform.position.x >= maxX )
            {
                sign *= -1;
            }
        }
    }

}
