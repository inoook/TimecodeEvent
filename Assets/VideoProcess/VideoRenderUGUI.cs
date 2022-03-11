using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoRenderUGUI : MonoBehaviour
{
    [SerializeField] RawImage gui = null;
    [SerializeField] Button playBtn = null;
    [SerializeField] Slider progressSlider = null;
    [SerializeField] Text infoText = null;
    [SerializeField] VideoPlayer videoPlayer = null;

    [SerializeField] bool isDrag = false;
    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(() => {
            if (!videoPlayer.isPaused)
            {
                videoPlayer.Pause();
            }
            else {
                videoPlayer.Play();
            }
        });

        progressSlider.onValueChanged.AddListener((v) => {
            Debug.Log("seek");
            videoPlayer.time = v * videoPlayer.length;
        });
    }

    // Update is called once per frame
    void Update()
    {
        gui.texture = videoPlayer.texture;

        float value = (float)((float)videoPlayer.time / (float)videoPlayer.length);
        if (!float.IsNaN(value))
        {
            progressSlider.SetValueWithoutNotify(value);
            infoText.text = TimelineProcess.ToTimecode((float)videoPlayer.time);
        }
    }

    public float CurrentTimeSec => (float)(videoPlayer.time);

    public void SetVideo(VideoClip clip)
    {
        videoPlayer.clip = clip;
    }
    public void SetVideo(string filePath)
    {
        videoPlayer.url = filePath;
    }
}
