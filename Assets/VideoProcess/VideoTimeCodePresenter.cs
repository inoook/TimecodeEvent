using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTimeCodePresenter : MonoBehaviour
{
    // -----
    [System.Serializable]
    public class Content
    {
        public string videoFile = "";
        public string oscJson = "osc.json";
    }
    [System.Serializable]
    public class Setting
    {
        public Content[] contents = null;
    }

    //[SerializeField] Setting setting = null;
    // -----
    [System.Serializable]
    public class AppSetting
    {
        public OSCSetting osc = null;
        public Content[] contents = null;
    }
    [System.Serializable]
    public class OSCSetting
    {
        public string ip = "127.0.0.1";
        public int port = 8080;
    }
    [SerializeField] string appSettingFile = "appSetting.json";
    [SerializeField] AppSetting appSetting = null;

    AppSetting LoadAppSetting(string settingFile)
    {
        string path = System.IO.Path.Combine(settingPath, settingFile);
        Debug.Log("path: " + path);
        string jsonStr = FileUtils.Read(path);
        if (!string.IsNullOrEmpty(jsonStr))
        {
            return JsonUtility.FromJson<AppSetting>(jsonStr);
        }
        return null;
    }

    [ContextMenu("SaveAppSetting")]
    void SaveAppSetting()
    {
        string path = System.IO.Path.Combine(settingPath, appSettingFile);
        string jsonStr = JsonUtility.ToJson(appSetting, true);
        FileUtils.Write(path, jsonStr);
    }

    void ApplyAppSetting(AppSetting setting)
    {
        OSCSetting oscSetting = setting.osc;
        timelineProcessOSC.SetSetting(oscSetting.ip, oscSetting.port);
        //
        // content setting
        contentSettingList = new List<TimelineProcess.Setting>();
        for (int i = 0; i < setting.contents.Length; i++)
        {
            Content content = setting.contents[i];
            TimelineProcess.Setting contentSetting = LoadSetting(content.oscJson);
            contentSettingList.Add(contentSetting);
        }
    }
    // -----

    [SerializeField] VideoRenderUGUI videoPlayer = null;
    [SerializeField] TimelineProcess timelineProcess = null;
    [SerializeField] TimelineProcessOSC timelineProcessOSC = null;

    string settingPath => Application.dataPath + "/../../_Externals/";
    TimelineProcess.Setting LoadSetting(string settingFile)
    {
        string path = System.IO.Path.Combine(settingPath, settingFile);
        Debug.Log("path: " + path);
        string jsonStr = FileUtils.Read(path);
        if (!string.IsNullOrEmpty(jsonStr))
        {
            return JsonUtility.FromJson<TimelineProcess.Setting>(jsonStr);
        }
        return null;
    }

    List<TimelineProcess.Setting> contentSettingList;

    [SerializeField] int currentId = 0;

    // Start is called before the first frame update
    void Awake()
    {
        // app setting
        appSetting = LoadAppSetting(appSettingFile);
        if (appSetting != null)
        {
            ApplyAppSetting(appSetting);
        }


    }

    private void Start()
    {
        // apply setting
        SetContent(currentId);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    void SetContent(int id)
    {
        currentId = id;
        Content content = appSetting.contents[id];

        string path = System.IO.Path.Combine(settingPath, content.videoFile);
        if (System.IO.File.Exists(path))
        {
            videoPlayer.SetVideo(path);
        }
        else
        {
            Debug.LogWarning($"存在しないPath: {path}");
        }

        if (contentSettingList != null && id < contentSettingList.Count)
        {
            timelineProcess.ApplySetting(contentSettingList[id]);
        }
    }

    public void DrawGUI()
    {
        for (int i =0; i < appSetting.contents.Length; i++) {
            if (GUILayout.Button($"{appSetting.contents[i].videoFile}"))
            {
                SetContent(i);
            }
        }
    }

    [SerializeField] Rect drawRect = new Rect(300,10,300,300);
    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);

        timelineProcessOSC.DrawGUI();

        DrawGUI();
        GUILayout.EndArea();
    }
}
