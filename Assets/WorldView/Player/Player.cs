using UnityEngine;
using UnityEngine.InputSystem;

/// the player
public class Player: MonoBehaviour {
    // -- statics --
    /// the being layer
    int s_BeingLayer = -1;

    // -- tuning --
    [Header("tuning")]
    [Tooltip("the move speed, a force")]
    [SerializeField] float m_MoveSpeed = 0.5f;

    [Tooltip("the look speed, a torque")]
    [SerializeField] float m_LookSpeed = 0.5f;

    [Tooltip("the max range of the hearing cone")]
    [SerializeField] float m_HearingRange = 0.5f;

    [Tooltip("the min threshold for the hearing cone")]
    [SerializeField] float m_HearingThreshold = 0.5f;

    // -- nodes --
    [Header("nodes")]
    [Tooltip("the root transform")]
    [SerializeField] Transform m_Root;

    [Tooltip("the player's rigidbody")]
    [SerializeField] Rigidbody m_Body;

    [Tooltip("the input system input")]
    [SerializeField] PlayerInput m_Input;

    // -- props --
    /// the current move mag
    float m_Move;

    /// the current look mag
    float m_Look;

    /// the list of heard beings
    Collider[] m_Heard = new Collider[50];

    /// the player's inputs
    PlayerActions m_Actions;

    // -- lifecycle --
    void Awake() {
        // set props
        m_Actions = new PlayerActions(m_Input);

        // set statics
        if (s_BeingLayer == -1) {
            s_BeingLayer = LayerMask.NameToLayer("Being");
        }
    }

    void Update() {
        // read input
        ReadMove();
        ReadLook();
    }

    void FixedUpdate() {
        // move player
        Move();
        Look();

        // listen for beings
        Hear();
    }

    // -- commands --
    /// read move input
    void ReadMove() {
        var move = m_Actions.Move;
        m_Move = Vector2.Dot(move, Vector2.up);
    }

    /// read move input
    void ReadLook() {
        var look = m_Actions.Look;
        m_Look = Vector2.Dot(look, Vector2.up);
    }

    /// move player
    void Move() {
        var move = m_Move * m_MoveSpeed * Time.deltaTime * m_Root.forward;
        m_Body.AddForce(move);
    }

    /// move camera
    void Look() {
        var turn = m_Look * m_LookSpeed * Time.deltaTime * Vector3.left;
        m_Body.AddRelativeTorque(turn);
    }

    // listen for beings
    void Hear() {
        var pos = m_Root.position;

        // check for any beings in range
        var nHits = Physics.OverlapSphereNonAlloc(
            pos,
            m_HearingRange,
            m_Heard,
            1 << s_BeingLayer
        );

        // for each being
        for (var i = 0; i < nHits; i++) {
            var hit = m_Heard[i];

            // get the direction to the being
            var dir = Vector3.Normalize(hit.transform.position - pos);

            // if it's within the hearing cone
            var alignment = Vector3.Dot(m_Root.forward, dir);
            if (alignment < m_HearingThreshold) {
                continue;
            }

            // set it to audible
            var being = hit.GetComponent<Being>();
            if (being != null) {
                being.SetAudible(true);
            }
        }
    }
}