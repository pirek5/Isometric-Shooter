using IsoShooter.Weapons;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WeaponId))]
public class WeaponAttributeDrawer : PropertyDrawer
{
   private int _selectedIndex;
   private string[] choices;


   private string[] Choices
   {
      get
      {
         if (choices == null)
         {
            CreateChoices();
         }

         return choices;
      }
   }
   
   public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
   {
      string currentValue = property.stringValue;
      for (int i = 0; i < Choices.Length; i++)
      {
         if(Choices[i] != currentValue)
            continue;

         _selectedIndex = i;
      }

      _selectedIndex = EditorGUI.Popup(position, property.displayName, _selectedIndex, choices);
   }

   private void CreateChoices()
   {
      choices = WeaponsDatabase.GetAllWeaponsIds().ToArray();
   }
}
