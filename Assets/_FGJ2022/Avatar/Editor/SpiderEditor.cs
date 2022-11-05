using Game.Avatar.SpiderImpl;
using UnityEditor;
using UnityEngine;

namespace Game.Avatar.Editor {
    [CustomEditor(typeof(Spider))]
    sealed class SpiderEditor : UnityEditor.Editor {

        Spider spider;

        static bool showDebug = true;

        static float debugIconScale = 0.05f;
        static bool showOrientations = true;
        static bool showCentroid = true;

        public void OnEnable() {
            spider = (Spider)target;
            if (showDebug && !EditorApplication.isPlaying) {
                spider.Awake();
            }
        }

        public override void OnInspectorGUI() {
            if (spider == null) {
                return;
            }

            EditorDrawing.DrawHorizontalLine(Color.gray);
            EditorGUILayout.LabelField("Debug Drawing", EditorStyles.boldLabel);
            showDebug = EditorGUILayout.Toggle("Show Debug Drawings", showDebug);
            if (showDebug) {
                EditorGUI.indentLevel++;
                debugIconScale = EditorGUILayout.Slider("Drawing Scale", debugIconScale, 0.01f, 0.1f);
                showOrientations = EditorGUILayout.Toggle("Draw Orienations", showOrientations);
                showCentroid = EditorGUILayout.Toggle("Draw Centroid", showCentroid);
                EditorGUI.indentLevel--;
            }
            EditorDrawing.DrawHorizontalLine();

            base.OnInspectorGUI();

            EditorDrawing.DrawHorizontalLine();

            EditorGUILayout.LabelField("Found IK Legs", EditorStyles.boldLabel);
            GUI.enabled = false;
            for (int i = 0; i < spider.legs.Length; i++) {
                EditorGUILayout.ObjectField(spider.legs[i], typeof(IKChain), false);
            }
            GUI.enabled = true;

            EditorDrawing.DrawHorizontalLine();

            if (showDebug && !EditorApplication.isPlaying) {
                spider.Awake();
            }
        }

        void OnSceneGUI() {
            if (!showDebug || !spider) {
                return;
            }

            //Draw the current transform.up and the bodys current Y orientation
            if (showOrientations) {
                var end = spider.transform.position + 2f * spider.getColliderRadius() * spider.transform.up;
                var endLocal = spider.transform.position + 2f * spider.getColliderRadius() * spider.root.TransformDirection(spider.bodyY);

                Handles.color = Color.blue;
                Handles.DrawLine(spider.transform.position, endLocal);
                EditorDrawing.DrawText(endLocal, "Body\nOrientation", Color.blue);

                Handles.color = Color.green;
                Handles.DrawLine(spider.transform.position, end);
                EditorDrawing.DrawText(end, "Controller\nOrientation", Color.green);
            }

            //Draw the Centroids 
            if (showCentroid) {
                var centroid = spider.getDefaultCentroid();
                var legcentroid = spider.getLegsCentroid();
                var colbottom = spider.getColliderBottomPoint();

                Handles.color = Color.magenta;
                Handles.DrawWireCube(centroid, debugIconScale * Vector3.one);
                EditorDrawing.DrawText(centroid, "Body\nCentroid", Color.magenta);

                Handles.color = Color.red;
                Handles.DrawWireCube(legcentroid, debugIconScale * Vector3.one);
                EditorDrawing.DrawText(legcentroid, "Legs\nCentroid", Color.red);

                Handles.color = Color.cyan;
                Handles.DrawWireCube(colbottom, debugIconScale * Vector3.one);
                EditorDrawing.DrawText(colbottom, "Collider\nBottom", Color.cyan);
            }
        }
    }
}
