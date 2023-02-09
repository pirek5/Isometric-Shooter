using UnityEditor;
using UnityEngine;

namespace IsoShooter.Weapons.Editor
{
    [CustomPropertyDrawer(typeof(WeaponId))]
    public class WeaponAttributeDrawer : PropertyDrawer
    {
        private int _selectedIndex;
        private string[] _choices;


        private string[] Choices
        {
            get
            {
                if (_choices == null)
                {
                    CreateChoices();
                }

                return _choices;
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

            _selectedIndex = EditorGUI.Popup(position, property.displayName, _selectedIndex, _choices);
            if(_choices.Length == 0)
                return;
      
            property.stringValue = _choices[_selectedIndex];
        }

        private void CreateChoices()
        {
            _choices = WeaponsDatabase.GetAllWeaponsIds().ToArray();
        }
    }

}

