using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 時間の経過で登録されたイベントを発行する
/// </summary>
[System.Serializable]
public class KeyTimePlayer
{
    [System.Serializable]
    public class KeyTime
    {
        public float timeSec = -1;

        public KeyTime(float timeSet)
        {
            this.timeSec = timeSet;
        }
    }

    public delegate void ScenarioHandler(KeyTime key, float timeSec);
    public event ScenarioHandler eventScenario;

    public delegate void ScenarioEventHandler();
    public event ScenarioEventHandler eventScenarioComplete;

    [SerializeField] List<KeyTime> keyList = new List<KeyTime>();

    [SerializeField] int scenarioIndex = -1;
    [SerializeField] float currentTimeSec = 0;

    public void Init()
    {
        scenarioIndex = -1;
        currentTimeSec = 0;
    }
    public void Init(List<KeyTime> keyList)
    {
        this.keyList?.Clear();
        this.keyList = keyList;

        Init();
    }

    [SerializeField] int debug_index = -1;
    [SerializeField, Range(0, 1)] float per = 0;
    [SerializeField, Range(0, 1)] float progress = 0;

    public float Progress => progress;

    public void Process(float timeSec)
    {
        //if (IsComplete()) { return; }
        if(keyList == null || keyList.Count <= 0) { return; }

        KeyTime end = keyList[keyList.Count - 1];
        progress = Mathf.InverseLerp(0, end.timeSec, timeSec);

        float deltaTime = timeSec - currentTimeSec;
        currentTimeSec = timeSec;

        int index = GetIndex(timeSec);
        if(index < 0) { return; }

        debug_index = index;

        int currentIndex = Mathf.Clamp(index, 0, keyList.Count - 1);
        int nextIndex = Mathf.Clamp(index + 1, 0, keyList.Count - 1);
        KeyTime current = keyList[currentIndex];
        KeyTime next = keyList[nextIndex];
        per = Mathf.InverseLerp(current.timeSec, next.timeSec, timeSec);// 今と次のScenarioの間 per(0 ~ 1.0f)

        // event
        if (scenarioIndex != index)
        {
            //Debug.LogWarning("change: " + index);
            int delta = index - scenarioIndex;
            if (delta > 1)
            {
                Debug.LogWarning("error: Scenarioのdeltaが2以上です。シナリオがスキップされた可能性があります。scenarioIndex: " + scenarioIndex + " / index: " + index);
            }
            //
            if (delta > 0)
            {
                // delta が複数の時はその間のイベントをすべて送出
                for (int i = 0; i < delta; i++)
                {
                    int _index = index + i;
                    eventScenario?.Invoke(keyList[_index], timeSec);
                }
            }
            else
            {
                // delta がマイナス変化（時間が遡る）のときは現在ののものみ送出
                eventScenario?.Invoke(keyList[index], timeSec);
            }
            //
            if (index == keyList.Count - 1)
            {
                // complete
                eventScenarioComplete?.Invoke();
            }
            //
            scenarioIndex = index;
        }
    }

    public bool IsComplete()
    {
        return scenarioIndex == keyList.Count - 1;
    }


    int GetIndex(float timeSec)
    {
        int index = -1;
        for (int i = 0; i < keyList.Count; i++)
        {
            KeyTime s = keyList[i];
            if (timeSec >= s.timeSec)
            {
                index = i;
            }
            else
            {
                return index;
            }
        }
        return index;
    }

    public int ScenarioCount => keyList.Count;

    public float EndTimeSec
    {
        get {
            if(keyList == null || keyList.Count == 0) { return 0; }
            return keyList[ScenarioCount-1].timeSec;
        }
    }
}
