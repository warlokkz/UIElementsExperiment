using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoWindow : EditorWindow
{
    private void OnEnable()
    {
        var root = rootVisualElement;

        var container = new VisualElement
        {
            style =
            {
                marginTop = 6,
                marginBottom = 6,
                flexDirection = FlexDirection.Row,
                backgroundColor = new Color(0.3f, 0.3f, 0.3f),
            }
        };

        container.Add(new Label
        {
            text = "Hello, World!",
            style =
            {
                fontSize = 20,
                unityFontStyleAndWeight = FontStyle.Bold,
                width = 140
            }
        });
        
        root.Add(container);
    }
    
    #region Show Window
    [MenuItem("Demo/Show Window")]
    public static void ShowWindow()
    {
        var window = GetWindow<DemoWindow>();
        window.minSize = new Vector2(350, 200);
        window.titleContent = new GUIContent("Demo Window");
    }
    #endregion
}
