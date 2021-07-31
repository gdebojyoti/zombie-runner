using UnityEngine;

public class BulletController : MonoBehaviour {

  #region public members

    public float speed = 200f;
    public float maxRange = 8f; // max distance the bullet can travel before getting destroyed

  #endregion

  #region private members

    private Rigidbody2D m_rb;
    private Vector2 origin;

  #endregion
  
  private void Start () {
    m_rb = GetComponent<Rigidbody2D>();
    origin = transform.position;
  }

  private void Update () {
    _CheckIfRangeExceeded();
  }

  private void FixedUpdate () {
    _Travel();
  }

  private void OnCollisionEnter2D (Collision2D other) {
    string tag = other.gameObject.tag;

    // TODO: use constants for values like "Enemy"
    // on collision with enemy, destroy bullet & enemy
    // on collision with obstacle, discuss what should be done
    if (tag == "Obstacle" || tag == "Enemy") {
      Destroy(other.gameObject); // destroy enemy
      Destroy(gameObject); // destroy self
    } else {
      Debug.Log("Damaged: " + other.gameObject.tag);
    }
  }

  #region private methods

    private void _Travel () {
      // speed to be applied in current frame
      float speedPerFrame = speed * Time.fixedDeltaTime;

      // move self
      m_rb.velocity = new Vector2(1, 0) * speedPerFrame;
    }

    // if bullet has travelled beyond range, destroy it
    private void _CheckIfRangeExceeded () {
      if (((Vector2)transform.position - origin).magnitude >= maxRange) {
        Destroy(gameObject);
      }
    }

  #endregion
}