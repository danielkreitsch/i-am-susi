namespace RedBlueGames.NotNull
{
    using System.Collections;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Class that contains logic to find NotNull violations when pressing Play from the Editor
    /// </summary>
    [InitializeOnLoad]
    public class FindNotNullsOnLaunch
    {
        static FindNotNullsOnLaunch()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChange;
        }

        private static void OnPlayModeStateChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                NotNullFinder.SearchForAndErrorForNotNullViolations();
            }
        }
    }
}