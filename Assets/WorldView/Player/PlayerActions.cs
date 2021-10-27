using UnityEngine;
using UnityEngine.InputSystem;

/// the player's actions (wraps input)
public class PlayerActions {
    // -- props --
    /// the move action
    InputAction m_Move;

    /// the move action
    InputAction m_Look;

    // -- lifetime --
    /// create a new actions wrapper
    public PlayerActions(PlayerInput input) {
        m_Move = input.currentActionMap["Move"];
        m_Look = input.currentActionMap["Look"];
    }

    // -- queries --
    /// the move position
    public Vector2 Move {
        get => m_Move.ReadValue<Vector2>();
    }

    /// the move position
    public Vector2 Look {
        get => m_Look.ReadValue<Vector2>();
    }
}
