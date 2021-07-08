// define player controls

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  #region public members

    public int positionMultiplier = 1;
    public GameObject bulletPrefab;
    public float verticalSpeed = 10f;
  
  #endregion

  #region private members
  
    private int m_laneId = 0; // one of: -1, 0, 1; -1 = top lane, 1 = bottom lane
    private Rigidbody2D m_rb;
    private bool m_isMoving = false; // set to true if player is switching between lanes
    private Vector2 m_target; // target position when player switches lanes

  #endregion

  #region MonoBehaviour methods

    private void Start() {
      this.m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
      _CheckForInputs();
    }

    private void FixedUpdate() {
      _VerticalMovement();
    }

    private void OnCollisionEnter2D(Collision2D other) {
      Debug.Log("collided with: " + other.gameObject.tag);
    }

  #endregion

  #region private methods

    private void _CheckForInputs () {
      // press 'W' / 'S' or up / down arrow keys to switch between lanes
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
        _SwitchLanes(1);
      }
      if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
        _SwitchLanes(-1);
      }

      // press 'F' to fire bullets
      if (Input.GetKeyDown(KeyCode.F)) {
        _Fire();
      }
    }

    // move up or down lanes
    private void _SwitchLanes (int delta) {
      if (delta == -1) {
        m_laneId = Mathf.Max(m_laneId - 1, -1);
      } else if (delta == 1) {
        m_laneId = Mathf.Min(m_laneId + 1, 1);
      }

      // update internal variables
      m_target = new Vector2(transform.position.x, m_laneId * positionMultiplier);
      m_isMoving = true;
    }

    private void _Fire () {
      GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    // movement required for lane switching
    private void _VerticalMovement () {
      if (m_isMoving) {
        Vector2 direction = (m_target - (Vector2)transform.position);
        float distance = direction.magnitude;
        float speedPerFrame = verticalSpeed * Time.fixedDeltaTime;

        // set exact position when player is very near to target position
        if (distance <= speedPerFrame) {
          m_rb.MovePosition(m_target);
          m_isMoving = false;
          return;
        }

        Vector2 normal = direction.normalized;
        m_rb.MovePosition((Vector2)transform.position + normal * speedPerFrame);
      }
    }

  #endregion
}
