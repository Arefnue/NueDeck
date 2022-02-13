using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace NueDeck.Editor
{
    public class EditorExample : EditorWindow
    {
        public enum GridType
        {
            Cartesian,
            Polar
        }
        
        
        [MenuItem("NueTools/Learning/Window/Snap Tool")]
        public static void OpenTheThing() => GetWindow<EditorExample>("Snapper");

        private const string UNDO_STR_SNAP = "Snap Objects";
        const float TAU = 6.28318530718f;
        
        
        public float gridSize = 1f;
        public GridType gridType = GridType.Cartesian;
        public int angularDivisions = 24;
        
        
        
        private SerializedObject _so;
        private SerializedProperty _propGridSize;
        private SerializedProperty _propGridType;
        private SerializedProperty _propAngularDivisions;
        
        
        //private SerializedProperty _propPoints;
        //public Vector3[] points = new Vector3[4];
        private void OnEnable()
        {
            _so = new SerializedObject(this);
            _propGridSize = _so.FindProperty("gridSize");
            _propGridType = _so.FindProperty("gridType");
            _propAngularDivisions = _so.FindProperty("angularDivisions");

            gridSize = EditorPrefs.GetFloat("SNAPPER_TOOL_gridSize",1f);
            gridType = (GridType)EditorPrefs.GetInt("SNAPPER_TOOL_gridType",0);
            angularDivisions = EditorPrefs.GetInt("SNAPPER_TOOL_angularDivision",24);
            
            //_propPoints = _so.FindProperty("points");
            Selection.selectionChanged += Repaint;
            SceneView.duringSceneGui += DuringSceneGUI;
        }

        private void OnDisable()
        {
            EditorPrefs.SetFloat("SNAPPER_TOOL_gridSize",gridSize);
            EditorPrefs.SetInt("SNAPPER_TOOL_gridType",(int)gridType);
            EditorPrefs.SetInt("SNAPPER_TOOL_angularDivision",angularDivisions);
            
            Selection.selectionChanged -= Repaint;
            SceneView.duringSceneGui -= DuringSceneGUI;
        }

        private void DuringSceneGUI(SceneView sceneView)
        {
            // // Array
            // _so.Update();
            // points = Handles.PositionHandle(points, Quaternion.identity);
            // for (int i = 0; i < _propPoints.arraySize; i++)
            // {
            //     SerializedProperty prop = _propPoints.GetArrayElementAtIndex(i);
            //     prop.vector3Value = Handles.PositionHandle(prop.vector3Value, Quaternion.identity);
            // }
            //
            // _so.ApplyModifiedProperties();
            
            
            if (Event.current.type == EventType.Repaint)
            {
                Handles.zTest = CompareFunction.LessEqual;
            
                const float gridDrawExtent = 16;

                switch (gridType)
                {
                    case GridType.Cartesian:
                        DrawGridCartesian(gridDrawExtent);
                        break;
                    case GridType.Polar:
                        DrawGridPolar(gridDrawExtent);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
           
        }
        
        private void DrawGridPolar(float gridDrawExtent)
        {
            int ringCount = Mathf.RoundToInt(gridDrawExtent / gridSize);
            float radiusOuter = (ringCount-1) * gridSize;
            
            //rings
            for (int i = 0; i < ringCount; i++)
            {
                Handles.DrawWireDisc(Vector3.zero,Vector3.up,i*gridSize);
            }

            
            //lines
            for (int i = 0; i < angularDivisions; i++)
            {
                float t = i / (float) angularDivisions;
                float angRad = t * TAU; //turns to radians

                float x = Mathf.Cos(angRad);
                float y = Mathf.Sin(angRad);

                Vector3 dir = new Vector3(x, 0,y);
                
                Handles.DrawAAPolyLine(Vector3.zero,dir*radiusOuter);
            }
        }
        

        private void DrawGridCartesian(float gridDrawExtent)
        {
            int lineCount = Mathf.RoundToInt((gridDrawExtent * 2) / gridSize);
            if (lineCount %2 == 0)
            {
                lineCount++;
            }
            int halfLineCount = lineCount / 2;
            
            
            for (int i = 0; i < lineCount; i++)
            {
                int intOffset = i-halfLineCount;
                float xCoord = intOffset * gridSize;
                float zCoord0 = halfLineCount * gridSize;
                float zCoord1 = -halfLineCount * gridSize;

                Vector3 p0 = new Vector3(xCoord, 0f, zCoord0);
                Vector3 p1 = new Vector3(xCoord, 0f, zCoord1);
                Handles.DrawAAPolyLine(p0,p1);
                
                p0 = new Vector3(zCoord0, 0f, xCoord);
                p1 = new Vector3( zCoord1, 0f,xCoord);
                Handles.DrawAAPolyLine(p0,p1);
            }
        }
        
        
        private void OnGUI()
        {

            _so.Update();
            EditorGUILayout.PropertyField(_propGridType);
            EditorGUILayout.PropertyField(_propGridSize);
            if (gridType == GridType.Polar)
            {
                EditorGUILayout.PropertyField(_propAngularDivisions);
                _propAngularDivisions.intValue = Mathf.Max(4, _propAngularDivisions.intValue);
            }
            _so.ApplyModifiedProperties();
            
            using (new EditorGUI.DisabledScope( Selection.gameObjects.Length == 0))
            {
                if (GUILayout.Button("Snap Selection"))
                {
                    SnapSelection();
                }
            }
        }

        private void SnapSelection()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                Undo.RecordObject(go.transform,UNDO_STR_SNAP);
                go.transform.position = GetSnappedPosition(go.transform.position);
            }
        }

        Vector3 GetSnappedPosition(Vector3 posOriginal)
        {
            if (gridType == GridType.Cartesian)
            {
                //return posOriginal.Round(gridSize);
            }
            
            if (gridType == GridType.Polar)
            {
                // Vector2 vec = new Vector2(posOriginal.x, posOriginal.z);
                // float dist = vec.magnitude;
                // float distSnapped = dist.Round(gridSize);
                //
                // float angRad = Mathf.Atan2(vec.y, vec.x); //0 to TAU
                // float angTurns = angRad / TAU; // 0 to 1
                // float angTurnsSnapped = angTurns.Round(1f / angularDivisions);
                // float angRadSnapped = angTurnsSnapped*TAU;
                //
                //
                // Vector2 dirSnapped = new Vector2(Mathf.Cos(angRadSnapped), Mathf.Sin(angRadSnapped));
                // Vector2 snappedVec = dirSnapped * distSnapped;
                //
                // return new Vector3(snappedVec.x, posOriginal.y, snappedVec.y);

            }

            return default;
        }
    }
}