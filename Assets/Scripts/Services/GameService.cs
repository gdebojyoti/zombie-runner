public static class GameService {
  
  private static float m_worldMovementMultiplier = 1f; // since the world will be moving while the player is actually stagnant

  public static float GetWorldMovementMultiplier () {
    return m_worldMovementMultiplier;
  }
}