using UnityEngine;

public class GroundBlockController : MonoBehaviour {
  
  public float baseMovementSpeed = -1f;
  public float limit = -14f;
  public float buffer = 25f;


  private void FixedUpdate() {
    _Move();
  }

  #region private methods

    private void _Move() {
      // speed to be applied in current frame
      float speedPerFrame = baseMovementSpeed * DetailsService.GetWorldMovementMultiplier() * Time.deltaTime;

      // move self
      transform.Translate(speedPerFrame, 0, 0);

      // place self at the end (on right side) if self has moved too far to the left
      if (transform.position.x < limit) {
        transform.position = new Vector2(transform.position.x + buffer, 0);
      }
    }

    #endregion
}