using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelectInput : MonoBehaviour
{
    public delegate void SelectSceneHandler(int id);
    public event SelectSceneHandler eventSelectScene;

    [Header("表示しているGameObjectのみイベント登録を行う")]
    [SerializeField] InputBase[] inputs = null;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var input in inputs)
        {
            if (input.gameObject.activeSelf)
            {
                // 表示しているGameObjectのみイベント登録
                input.eventSelectScene += SelectScene;
            }
        }
    }


    void SelectScene(int id)
    {
        eventSelectScene?.Invoke(id);
    }
}
