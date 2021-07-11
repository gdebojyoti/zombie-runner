// NOTE: Deprecated file. To be replaced by GroundController.cs.

using UnityEngine;

public class GroundBlockController : MonoBehaviour {
  
  #region public members

    public float baseMovementSpeed = -2f;
    public float limit = -14f; // position.x after crossing which block is to be placed at the right-most end
    public float buffer = 25f; // distance by which block is to be moved (reset) after it crosses `limit`
    public GameObject zombiePrefab;

  #endregion

  #region private members

    private bool m_ObstaclesInitialized = false;

  #endregion


  private void FixedUpdate() {
    _Move();
  }

  #region private methods

    private void _Move() {
      // speed to be applied in current frame
      float speedPerFrame = baseMovementSpeed * GameService.GetWorldMovementMultiplier() * Time.fixedDeltaTime;

      // move self
      transform.Translate(speedPerFrame, 0, 0);

      // place self at the end (on right side) if self has moved too far to the left
      if (transform.position.x < limit) {
        _Initialize();
      }
    }

    private void _Initialize () {
      // TODO: regenerate obstacles
      // generate obstacles if not already done
      if (!m_ObstaclesInitialized) {
        _InitObstacles();
        m_ObstaclesInitialized = true;
      }

      // reposition self
      transform.position = new Vector2(transform.position.x + buffer, 0);
    }

    private void _InitObstacles () {
      GameObject child1 = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
      child1.transform.SetParent(transform);
      child1.transform.localPosition = new Vector2(1, 0);
      
      GameObject child2 = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
      child2.transform.SetParent(transform);
      child2.transform.localPosition = new Vector2(3, 0);
    }

  #endregion
}