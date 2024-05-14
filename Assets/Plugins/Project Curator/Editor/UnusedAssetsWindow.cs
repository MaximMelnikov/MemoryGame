using System.IO;
using UnityEditor;
using UnityEngine;

namespace Ogxd.ProjectCurator
{
    public class UnusedAssetsWindow : EditorWindow, IHasCustomMenu
    {

        [MenuItem("Window/UnusedAssetsWindow")]
        private static void Init()
        {
            GetWindow<UnusedAssetsWindow>("UnusedAssetsWindow");
        }

        public UnusedAssetsWindow()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            Repaint();
        }

        private Vector2 scroll;

        private bool dependenciesOpen = true;
        private bool referencesOpen = true;

        private static GUIStyle titleStyle;
        private static GUIStyle TitleStyle => titleStyle ?? (titleStyle = new GUIStyle(EditorStyles.label) { fontSize = 13 });

        private static GUIStyle itemStyle;
        private static GUIStyle ItemStyle => itemStyle ?? (itemStyle = new GUIStyle(EditorStyles.label) { margin = new RectOffset(32, 0, 0, 0) });
        private void OnGUI()
        {
            if (ProjectCurator.pathToAssetInfo.Count == 0)
            {
                bool rebuildClicked = HelpBoxWithButton(new GUIContent("You must rebuild database to obtain information on this asset", EditorGUIUtility.IconContent("console.warnicon").image), new GUIContent("Rebuild Database"));
                if (rebuildClicked)
                {
                    ProjectCurator.RebuildDatabase();
                }
                return;
            }

            Rect rect = new Rect(0, 0, 200, 20);//GUILayoutUtility.GetLastRect();

            var assetInfos = ProjectCuratorData.AssetInfos;

            scroll = GUILayout.BeginScrollView(scroll);

            foreach (var item in assetInfos)
            {
                if (item.dependencies.Count == 0 && item.referencers.Count == 0)
                {
                    string extension = System.IO.Path.GetExtension(item.path);
                    if (extension == ".cs" ||
                        extension == ".aar" ||
                        extension == ".jar" ||
                        extension == ".dll" ||
                        extension == ".mdb" ||
                        extension == ".bin" ||
                        extension == ".so"
                        )
                    {
                        continue;
                    }

                    var content = new GUIContent(item.IsIncludedInBuild ? ProjectIcons.LinkBlue : ProjectIcons.LinkBlack, item.IncludedStatus.ToString());
                    GUI.Label(new Rect(position.width - 20, rect.y + 1, 16, 16), content);

                    if (GUILayout.Button(Path.GetFileName(item.path), ItemStyle))
                    {
                        UnityEditor.Selection.activeObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(item.path);
                    }
                    rect = GUILayoutUtility.GetLastRect();
                    GUI.DrawTexture(new Rect(rect.x - 16, rect.y, rect.height, rect.height), AssetDatabase.GetCachedIcon(item.path));
                    AssetInfo depInfo = ProjectCurator.GetAsset(item.path);
                    content = new GUIContent(depInfo.IsIncludedInBuild ? ProjectIcons.LinkBlue : ProjectIcons.LinkBlack, depInfo.IncludedStatus.ToString());
                    GUI.Label(new Rect(rect.width + rect.x - 20, rect.y + 1, 16, 16), content);
                }
            }

            GUILayout.Space(5);

            GUILayout.EndScrollView();

            /*if (!selectedAssetInfo.IsIncludedInBuild) {
                bool deleteClicked = HelpBoxWithButton(new GUIContent("This asset is not referenced and never used. Would you like to delete it ?", EditorGUIUtility.IconContent("console.warnicon").image), new GUIContent("Delete Asset"));
                if (deleteClicked) {
                    File.Delete(selectedPath);
                    AssetDatabase.Refresh();
                    ProjectCurator.RemoveAssetFromDatabase(selectedPath);
                }
            }*/
        }

        void IHasCustomMenu.AddItemsToMenu(GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Rebuild Database"), false, ProjectCurator.RebuildDatabase);
            menu.AddItem(new GUIContent("Clear Database"), false, ProjectCurator.ClearDatabase);
            menu.AddItem(new GUIContent("Project Overlay"), ProjectWindowOverlay.Enabled, () => { ProjectWindowOverlay.Enabled = !ProjectWindowOverlay.Enabled; });
        }

        public bool HelpBoxWithButton(GUIContent messageContent, GUIContent buttonContent)
        {
            float buttonWidth = buttonContent.text.Length * 8;
            const float buttonSpacing = 5f;
            const float buttonHeight = 18f;

            // Reserve size of wrapped text
            Rect contentRect = GUILayoutUtility.GetRect(messageContent, EditorStyles.helpBox);
            // Reserve size of button
            GUILayoutUtility.GetRect(1, buttonHeight + buttonSpacing);

            // Render background box with text at full height
            contentRect.height += buttonHeight + buttonSpacing;
            GUI.Label(contentRect, messageContent, EditorStyles.helpBox);

            // Button (align lower right)
            Rect buttonRect = new Rect(contentRect.xMax - buttonWidth - 4f, contentRect.yMax - buttonHeight - 4f, buttonWidth, buttonHeight);
            return GUI.Button(buttonRect, buttonContent);
        }
    }
}