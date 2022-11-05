using Game.Avatar.SpiderImpl;
using UnityEditor;
using UnityEngine;

namespace Game.Avatar.Editor {
    sealed class SpiderMasterWindow : EditorWindow {

        Spider spider;
        int count;
        IKStepper[] ikSteppers;
        IKChain[] ikChains;

        [MenuItem("Window/Spider Master Window")]
        static void Init() {
            // Get existing open window or if none, make a new one:
            var window = (SpiderMasterWindow)EditorWindow.GetWindow(typeof(SpiderMasterWindow));
            window.Show();
        }

        void FindArrays() {
            ikChains = spider.GetComponentsInChildren<IKChain>();
            count = ikChains.Length;
            ikSteppers = new IKStepper[count];
            for (int i = 0; i < count; i++) {
                ikSteppers[i] = ikChains[i].GetComponent<IKStepper>();
            }
        }
        void OnGUI() {
            EditorGUILayout.LabelField("Spider Master Window", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (spider == null) {
                spider = FindObjectOfType<Spider>();
            }

            spider = (Spider)EditorGUILayout.ObjectField("Pick Spider", spider, typeof(Spider), true);
            EditorGUILayout.Space();

            if (spider != null) {
                if (ikChains == null && ikSteppers == null) {
                    FindArrays();
                }

                //Show IKSteppers
                GUI.enabled = false;
                float fieldWidthTemp = EditorGUIUtility.fieldWidth;
                float labelWidthTemp = EditorGUIUtility.labelWidth;

                EditorGUILayout.BeginVertical();
                {
                    //Row1
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUIUtility.labelWidth = 100;
                        EditorGUILayout.LabelField("Leg", EditorStyles.boldLabel);
                        EditorGUILayout.LabelField("IK Chain", EditorStyles.boldLabel);
                        EditorGUILayout.LabelField("IK Stepper", EditorStyles.boldLabel);
                        EditorGUILayout.LabelField("Joints", EditorStyles.boldLabel);
                    }
                    EditorGUILayout.EndHorizontal();


                    EditorGUIUtility.labelWidth = 55;
                    EditorGUIUtility.fieldWidth = 100;

                    //Row2+
                    for (int i = 0; i < count; i++) {
                        EditorGUILayout.BeginHorizontal();
                        {
                            var chain = ikChains[i];
                            var ikStepper = ikSteppers[i];
                            EditorGUILayout.LabelField(chain.name);
                            EditorGUILayout.ObjectField(chain, typeof(IKChain), false);
                            EditorGUILayout.ObjectField(ikStepper, typeof(IKStepper), false);

                            EditorGUILayout.PrefixLabel(":");
                            EditorGUILayout.Space();


                            for (int j = 0; j < chain.joints.Length; j++) {
                                var joint = chain.joints[j];
                                EditorGUILayout.ObjectField(joint, typeof(JointHinge), false);
                                EditorGUILayout.PrefixLabel("[" + joint.weight.ToString() + "]  ->");
                            }
                            EditorGUILayout.ObjectField(chain.endEffector, typeof(Transform), false);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUIUtility.labelWidth = labelWidthTemp;
                EditorGUIUtility.fieldWidth = fieldWidthTemp;
                GUI.enabled = true;
            }
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            {
                if (EditorDrawing.DrawButton("Find IKChains, IKSteppers and JointHinges")) {
                    FindArrays();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}