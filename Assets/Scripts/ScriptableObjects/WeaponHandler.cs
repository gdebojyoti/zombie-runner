// TODO: Find out a way to optimize this,
// since only one copy of this will ever be needed

using UnityEngine;

[CreateAssetMenu(fileName = "WeaponHandler", menuName = "Scriptable Objects/WeaponHandler")]
public class WeaponHandler : ScriptableObject {
  public Weapon weapon;
}