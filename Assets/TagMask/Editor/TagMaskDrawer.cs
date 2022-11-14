using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace GameOn.TagMaskField
{
	[CustomPropertyDrawer(typeof(TagMask))]
	public class TagMaskDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var tags = UnityEditorInternal.InternalEditorUtility.tags;
			var mask = property.FindPropertyRelative("mask");
			var tagsProperty = property.FindPropertyRelative("tags");
			string[] currentTags = new string[tagsProperty.arraySize];

            for (int i = 0; i < currentTags.Length; i++)
            {
				currentTags[i] = tagsProperty.GetArrayElementAtIndex(i).stringValue;
            }

			EditorGUI.BeginProperty(position, GUIContent.none, property);
			EditorGUI.BeginChangeCheck();

			int actualMask = mask.intValue == 0 || mask.intValue == -1 ? mask.intValue : GetIntMask(currentTags);
			var newMask = EditorGUI.MaskField(position, new GUIContent(ObjectNames.NicifyVariableName(property.name)), actualMask, tags);

			if (EditorGUI.EndChangeCheck())
			{
				mask.intValue = GetMaskSimplified(newMask);
				var newMaskTags = new List<string>();

				for (int i = 0; i < tags.Length; i++)
				{
					if (BitmaskContainsValue(newMask, i))
					{
						newMaskTags.Add(tags[i]);
					}
				}

				tagsProperty.arraySize = newMaskTags.Count;

				for (int i = 0; i < newMaskTags.Count; i++)
				{
					tagsProperty.GetArrayElementAtIndex(i).stringValue = newMaskTags[i];
				}
			}

			EditorGUI.EndProperty();
		}

		private int GetMaskSimplified(int sourceMask)
        {
			if (sourceMask == -1 || sourceMask == 0)
            {
				return sourceMask;
            }
			else
            {
				return 1;
            }
        }

		private int GetIntMask(params string[] tags)
		{
			if (tags == null && tags.Length <= 0)
			{
				return 0;
			}

			int resultMask = 0;

			foreach (var t in tags)
			{
				if (t == "Everything")
				{
					return -1;
				}
				else if (t == "Nothing")
				{
					return 0;
				}
				else
				{
					int index = NameToIndex(t);

					if (index >= 0)
					{
						resultMask |= (1 << index);
					}
				}
			}

			return resultMask;
		}

		private int NameToIndex(string tag)
		{
			var tags = GetTags();
			var index = System.Array.IndexOf(tags, tag);
			return index;
		}

		private string[] GetTags()
		{
			return UnityEditorInternal.InternalEditorUtility.tags;
		}

		internal static bool BitmaskContainsValue(int mask, int value)
		{
			return mask == (mask | (1 << value));
		}
	}
}
