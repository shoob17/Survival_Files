using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shoob.AutoSave
{
    [InitializeOnLoad]
    public static class AutoSave
    {
        private static double _lastSaveTime;
        // How many minutes between saves
        private const float SAVE_INTERVAL_MINUTES = 1f;

        static AutoSave()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            try
            {
                var timeSinceStartup = EditorApplication.timeSinceStartup;

                // Only save if enough time has passed
                if (_lastSaveTime + (SAVE_INTERVAL_MINUTES * 60f) > timeSinceStartup) return;

                // Skip if any of these conditions are true
                if (Application.isPlaying || BuildPipeline.isBuildingPlayer || EditorApplication.isCompiling) return;
                if (!UnityEditorInternal.InternalEditorUtility.isApplicationActive) return;

                // Get the active scene
                var scene = SceneManager.GetActiveScene();
                if (string.IsNullOrEmpty(scene.path)) return;

                // Save over the existing scene rather than making a copy
                EditorSceneManager.SaveScene(scene);

                Debug.Log($"[BDAS] Auto-saved scene '{scene.name}' at {DateTime.Now:T}");

                _lastSaveTime = timeSinceStartup;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
    }
}
