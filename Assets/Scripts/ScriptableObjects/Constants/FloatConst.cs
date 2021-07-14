// NOTE:
// This is just a prototype.
// In practice, the list of constants will probably be fetched from a single JSON config file instead.

using UnityEngine;

[CreateAssetMenu(fileName = "New float constant", menuName = "Constants/Float")]
public class FloatConst : ScriptableObject {
  [field: SerializeField] public int Value { get; private set; }
  public string description;
}