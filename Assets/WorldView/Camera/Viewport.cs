using UnityEngine;

/// the camera's viewport
[ExecuteAlways]
public class Viewport: MonoBehaviour {
    // -- tuning --
    [Header("tuning")]
    [Tooltip("the camera size in pixels")]
    [SerializeField] float m_Size = 1.0f;

    // -- nodes --
    [Header("nodes")]
    [Tooltip("the camera")]
    [SerializeField] Camera m_Camera;

    // -- lifecycle --
    void Update() {
        var w = Screen.width;
        var r = m_Camera.pixelRect;
        r.x = w / 2.0f - m_Size;
        r.width = m_Size;
        m_Camera.pixelRect = r;
    }
}
