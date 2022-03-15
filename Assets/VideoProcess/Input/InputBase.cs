using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 外部からの入力の基底Class
/// </summary>
public class InputBase : MonoBehaviour
{
    public event SceneSelectInput.SelectSceneHandler eventSelectScene;

    protected void SelectScene(int id)
    {
        eventSelectScene?.Invoke(id);
    }
}
