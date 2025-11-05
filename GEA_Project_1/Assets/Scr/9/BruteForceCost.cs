using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteForceCost : MonoBehaviour
{
    int maxcost = 15;
    int maxdamage = -1;
    int fdamage = 0;
    int fcost = 0;

    string ac = " ";
    string uo = " ";
    string uc = " ";
    string fd = " ";

    int[] damage = { 6, 8, 16, 24 };
    int[] cost = { 2, 3, 5, 7 };
    int[] card = {2, 2, 1, 1};

    void Start()
    {
        for (int q = 0; q <= card[0]; q++)
        {
            for (int h = 0; h <= card[1]; h++)
            {
                for (int m = 0; m <= card[2]; m++)
                {
                    for (int t = 0; t <= card[3]; t++)
                    {
                        fcost = q * cost[0] + h * cost[1] + m * cost[2] + t * cost[3];
                        if (fcost <= maxcost)
                        {
                            fdamage = q * damage[0] + h * damage[1] + m * damage[2] + t * damage[3];
                            if (fdamage > maxdamage)
                            {
                                maxdamage = fdamage;
                                ac = $"사용 할 카드 목록 : 퀵샷{q}장, 해비 샷{h}장, 멀티샷{m}장, 트리플 샷{t}장";
                                uo = $"소모한 코스트 : {fcost}";
                                uc = $"소모한 카드 장 수 : {q + h + m + t}장";
                                fd = $"총 데미지 : {maxdamage}";
                            }
                        }
                    }
                }
            }
        }
        Debug.Log(ac);
        Debug.Log(uo);
        Debug.Log(uc);
        Debug.Log(fd);
    }
}
