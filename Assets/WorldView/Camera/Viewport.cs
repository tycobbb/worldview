using UnityEngine;
using UnityEngine.UI;

/// the camera's viewport
[ExecuteAlways]
public class Viewport: MonoBehaviour {
    // -- nodes --
    [Header("nodes")]
    [Tooltip("the camera")]
    [SerializeField] Camera m_Camera;

    // -- lifecycle --
    void Awake() {
        // resize render texture to match screen height
        // TODO: create new render texture on screen resize?
        var tex  = m_Camera.targetTexture;
        tex.height = Mathf.NextPowerOfTwo(Screen.height);
    }
}
