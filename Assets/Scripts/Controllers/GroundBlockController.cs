using UnityEngine;

public class GroundBlockController : MonoBehaviour {
  
  #region public members

    public float baseMovementSpeed = -2f;
    public float limit = -14f; // position.x after crossing which block is to be placed at the right-most end
    public float buffer = 25f; // distance by which block is to be moved (reset) after it crosses `limit`

  #endregion


  private void FixedUpdate() {
    _Move();
  }

  #region private methods

    private void _Move() {
      // speed to be applied in current frame
      float speedPerFrame = baseMovementSpeed * GameService.GetWorldMovementMultiplier() * Time.deltaTime;

      // move self
      transform.Translate(speedPerFrame, 0, 0);

      // place self at the end (on right side) if self has moved too far to the left
      if (transform.position.x < limit) {
        transform.position = new Vector2(transform.position.x + buffer, 0);
      }
    }

    #endregion
}