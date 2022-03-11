using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTimePlayerTest : MonoBehaviour
{
    [SerializeField] KeyTimePlayer scenarioPlayerBase = null;

    [SerializeField] float timeSec = 0;

    // Start is called before the first frame update
    void Start()
    {
        scenarioPlayerBase.eventScenario += ScenarioPlayerBase_eventScenario;
        scenarioPlayerBase.eventScenarioComplete += ScenarioPlayerBase_eventScenarioComplete;

        scenarioPlayerBase.Init();
    }

    private void ScenarioPlayerBase_eventScenarioComplete()
    {
        Debug.LogWarning("Complete");
    }

    private void ScenarioPlayerBase_eventScenario(KeyTimePlayer.KeyTime key, float timeSec)
    {
        Debug.LogWarning($"Action index: {key.timeSec} / timeSec: {timeSec}" );
    }

    // Update is called once per frame
    void Update()
    {
        if (!scenarioPlayerBase.IsComplete())
        {
            timeSec += Time.deltaTime;
            scenarioPlayerBase.Process(timeSec);
        }
    }

    [SerializeField] Rect drawRect = new Rect(10,10,400,400);
    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        if (GUILayout.Button("Rest"))
        {
            timeSec = 0;
            scenarioPlayerBase.Init();
        }
        GUILayout.Label(timeSec.ToString("0.00"));
        timeSec = GUILayout.HorizontalSlider(timeSec, 0, scenarioPlayerBase.EndTimeSec);
        GUILayout.EndArea();
    }
}
