using UnityEngine;
using UnityEngine.InputSystem;

/// the player's actions (wraps input)
public class PlayerActions {
    // -- props --
    /// the move action
    InputAction m_Move;

    // -- lifetime --
    /// create a new actions wrapper
    public PlayerActions(PlayerInput input) {
        m_Move = input.currentActionMap["Move"];
    }

    // -- queries --
    /// the move position
    public Vector2 Move {
        get => m_Move.ReadValue<Vector2>();
    }
}
