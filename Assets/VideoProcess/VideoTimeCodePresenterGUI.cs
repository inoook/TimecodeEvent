using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoTimeCodePresenterGUI : MonoBehaviour
{
    [SerializeField] VideoTimeCodePresenter presenter = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] Rect drawRect = new Rect(300, 10, 300, 300);
    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);

        presenter.DrawGUI();
        GUILayout.EndArea();
    }
}
