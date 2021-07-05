using UnityEngine;

public class BulletController : MonoBehaviour {

  #region private members

    private Rigidbody2D m_rb;
    // private float m_maxDistance = 5f;
    public float speed = 200f;

  #endregion
  
  private void Start () {
    Debug.Log("Fired!");
    m_rb = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    _Travel();
  }

  private void OnCollisionEnter2D (Collision2D other) {
    string tag = other.gameObject.tag;
    Debug.Log("Damaged: " + other.gameObject.tag);

    // TODO: use constants for values like "Enemy"
    // on collision with enemy, destroy bullet & enemy
    if (tag == "Enemy") {
      Destroy(other.gameObject); // destroy enemy
      Destroy(gameObject); // destroy self
    }
  }

  #region private methods

    private void _Travel () {
      // speed to be applied in current frame
      float speedPerFrame = speed * Time.deltaTime;

      // move self
      m_rb.velocity = new Vector2(1, 0) * speedPerFrame;
    }

  #endregion
}