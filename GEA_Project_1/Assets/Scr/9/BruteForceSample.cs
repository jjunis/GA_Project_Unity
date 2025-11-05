using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class BruteForceSample : MonoBehaviour
{
    public Button startButton;  //눌러서 브루트포스 시작 
    string secretPin;
    Coroutine runningRoutine;

    void Start()
    {
        //랜덤 4자리 생성 PIN 생성 (0000~9999)
        secretPin = Random.Range(0, 10000).ToString("D4");
        Debug.Log($"[Auth] 생성된 PIN = {secretPin}");
    }

    public void OnStartButtonClicked()
    {
        if (runningRoutine != null)
        {
            Debug.Log("[Brute] 이미 실행중입니다.");
            return;
        }
        runningRoutine = StartCoroutine(BruteForceRoutine());
    }
    IEnumerator BruteForceRoutine()
    {
        Debug.Log("[Brute] 시뮬레이션 시작");

        Stopwatch sw = new Stopwatch();
        sw.Start();

        int tryCount = 0;
        int max = 10000;    //0000~9999

        for (int i = 0; i < max; i++)
        {
            string tryString = i.ToString("D4");
            tryCount++;

            if (tryString == secretPin)
            {
                sw.Stop();
                double seconds = sw.Elapsed.TotalSeconds;
                Debug.Log($"[Brute] 성공! PIN={tryString} 시도수={tryCount} 소요={seconds:F3}초");
                runningRoutine = null;
                yield break;
            }
            if(i % 100 == 0)
            {
                yield return null;  //한 프레임 쉬기
            }
        }

        sw.Stop();
        Debug.Log($"[Brute] 모든 조합 시도 완료(발견 실패). 소요={sw.Elapsed.TotalSeconds:F3}초");
        runningRoutine = null;
    }

    void Update()
    {
        
    }
}
