using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RideX
{
    /// <summary>
    /// GameCI'nin cagiracagi build metodu.
    /// Sahneyi kodla olusturur, iOS Xcode projesini uretir.
    /// </summary>
    public static class BuildScript
    {
        private const string ScenePath = "Assets/Scenes/Main.unity";

        public static void BuildiOS()
        {
            try
            {
                Directory.CreateDirectory("Assets/Scenes");

                var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(scene, ScenePath);
                AssetDatabase.Refresh();

                EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };

                PlayerSettings.companyName = "RideX";
                PlayerSettings.productName = "RideXSpike";
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "com.ridex.spike");
                PlayerSettings.bundleVersion = "0.1.0";
                PlayerSettings.iOS.targetOSVersionString = "15.0";
                PlayerSettings.SetScriptingBackend(NamedBuildTarget.iOS, ScriptingImplementation.IL2CPP);

                // Imzasiz derleme icin takim/profil bos
                PlayerSettings.iOS.appleEnableAutomaticSigning = false;
                PlayerSettings.iOS.appleDeveloperTeamID = string.Empty;

                Debug.Log("[RideX] Build basliyor: iOS / com.ridex.spike");

                var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
                {
                    scenes = new[] { ScenePath },
                    locationPathName = "build/iOS",
                    target = BuildTarget.iOS,
                    options = BuildOptions.None
                });

                Debug.Log($"[RideX] Sonuc: {report.summary.result} hata={report.summary.totalErrors} boyut={report.summary.totalSize}");

                EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[RideX] Build istisnasi: {e}");
                EditorApplication.Exit(1);
            }
        }
    }
}
