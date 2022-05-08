using UnityEditor;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Utils
{
    public class Layers
    {
#if UNITY_EDITOR
        

        private static int maxTags = 10000;
        private static int maxLayers = 31;

/////////////////////////////////////////////////////////////////////

        public void AddNewLayer(string name)
        {
            CreateLayer(name);
        }

        public void DeleteLayer(string name)
        {
            RemoveLayer(name);
        }


////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Adds the layer.
        /// </summary>
        /// <returns><c>true</c>, if layer was added, <c>false</c> otherwise.</returns>
        /// <param name="layerName">Layer name.</param>
        public static bool CreateLayer(string layerName)
        {
            // Open tag manager
            SerializedObject tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            // Layers Property
            SerializedProperty layersProp = tagManager.FindProperty("layers");
            if (!PropertyExists(layersProp, 0, maxLayers, layerName))
            {
                SerializedProperty sp;
                // Start at layer 9th index -> 8 (zero based) => first 8 reserved for unity / greyed out
                for (int i = 8, j = maxLayers; i < j; i++)
                {
                    sp = layersProp.GetArrayElementAtIndex(i);
                    if (sp.stringValue == "")
                    {
                        // Assign string value to layer
                        sp.stringValue = layerName;
                        Debug.Log("Layer: " + layerName + " has been added");
                        // Save settings
                        tagManager.ApplyModifiedProperties();
                        return true;
                    }

                    if (i == j)
                        Debug.Log("All allowed layers have been filled");
                }
            }
            else
            {
                //Debug.Log ("Layer: " + layerName + " already exists");
            }

            return false;
        }

        public static string NewLayer(string name)
        {
            if (name != null || name != "")
            {
                CreateLayer(name);
            }

            return name;
        }

        /// <summary>
        /// Removes the layer.
        /// </summary>
        /// <returns><c>true</c>, if layer was removed, <c>false</c> otherwise.</returns>
        /// <param name="layerName">Layer name.</param>
        public static bool RemoveLayer(string layerName)
        {
            // Open tag manager
            SerializedObject tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

            // Tags Property
            SerializedProperty layersProp = tagManager.FindProperty("layers");

            if (PropertyExists(layersProp, 0, layersProp.arraySize, layerName))
            {
                SerializedProperty sp;

                for (int i = 0, j = layersProp.arraySize; i < j; i++)
                {
                    sp = layersProp.GetArrayElementAtIndex(i);

                    if (sp.stringValue == layerName)
                    {
                        sp.stringValue = "";
                        Debug.Log("Layer: " + layerName + " has been removed");
                        // Save settings
                        tagManager.ApplyModifiedProperties();
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks to see if layer exists.
        /// </summary>
        /// <returns><c>true</c>, if layer exists, <c>false</c> otherwise.</returns>
        /// <param name="layerName">Layer name.</param>
        public static bool LayerExists(string layerName)
        {
            // Open tag manager
            SerializedObject tagManager =
                new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

            // Layers Property
            SerializedProperty layersProp = tagManager.FindProperty("layers");
            return PropertyExists(layersProp, 0, maxLayers, layerName);
        }

        /// <summary>
        /// Checks if the value exists in the property.
        /// </summary>
        /// <returns><c>true</c>, if exists was propertyed, <c>false</c> otherwise.</returns>
        /// <param name="property">Property.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="value">Value.</param>
        private static bool PropertyExists(SerializedProperty property, int start, int end, string value)
        {
            for (int i = start; i < end; i++)
            {
                SerializedProperty t = property.GetArrayElementAtIndex(i);
                if (t.stringValue.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }
#endif
    }
}