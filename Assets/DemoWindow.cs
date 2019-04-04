using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoWindow : EditorWindow
{
    private static readonly Color[] Colors;

    private const int KMargin = 50;
    private const int KBoxSize = 100;
    private const string StyleSheetPath = "Assets/styles.uss";
    
    #region Show Window
    [MenuItem("Demo/FlexBox Example")]
    public static void ShowWindow()
    {
        var window = GetWindow<DemoWindow>();
        window.minSize = new Vector2(350, 200);
        window.titleContent = new GUIContent("Demo Window");
    }
    #endregion

    static DemoWindow()
    {
        Colors = new[] {
            Color.blue,
            Color.green,
            Color.yellow
        };
    }

    private void OnEnable()
    {
        var root = rootVisualElement;
        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath));
        root.Add(new Header());
        root.Add(new Grid(Colors));
    }

    #region Components
    private class Header : VisualElement
    {
        public Header()
        {
            AddToClassList("header");
            Add(new Label { text = "UI Elements" });
        }
    }

    private class Grid : VisualElement
    {
        public Grid(Color[] colors)
        {
            AddToClassList("grid");
            
            for (var i = 0; i < colors.Length; i++)
            {
                Add(
                    BoxColumn(i)
                );
            }
        }
    }

    private static VisualElement BoxColumn(int index)
    {
        var flipped = index % 2 == 0;
        var direction = flipped ?
             FlexDirection.Column : FlexDirection.ColumnReverse;
        
        var row =  new VisualElement
        {
            style =
            {
                display = DisplayStyle.Flex,
                flexDirection = direction,
                top = flipped ? 0 : KMargin,
                bottom = flipped ? KMargin : 0,
                height = KBoxSize * Colors.Length,
                width = KBoxSize * Colors.Length
            }
        };
        
        row.AddToClassList("row");
        
        foreach (var color in Colors)
        {
            row.Add(
                BoxElement(color)
            );
        }

        return row;
    }

    private static VisualElement BoxElement(Color color)
    {
        var box = new VisualElement
        {
            style =
            {
                backgroundColor = color
            }
        };
        
        box.AddToClassList("box");
        box.AddManipulator(new BoxMouseEventLogger());
        
        return box;
    }
    #endregion

    private class BoxMouseEventLogger : Manipulator
    {
        private StyleColor OriginalColor { get; set; }
        protected override void RegisterCallbacksOnTarget()
        {
            OriginalColor = target.style.backgroundColor;
            
            target.RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
            target.RegisterCallback<MouseLeaveEvent>(OnMouseLeaveEvent);
            target.RegisterCallback<MouseUpEvent>(OnMouseUpEvent);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseDownEvent>(OnMouseDownEvent);
            target.UnregisterCallback<MouseLeaveEvent>(OnMouseLeaveEvent);
            target.UnregisterCallback<MouseUpEvent>(OnMouseUpEvent);
        }
        
        void OnMouseDownEvent(MouseEventBase<MouseDownEvent> evt)
        {
            target.style.backgroundColor = new StyleColor(Color.red);
        }

        void OnMouseLeaveEvent(MouseEventBase<MouseLeaveEvent> evt)
        {
            target.style.backgroundColor = OriginalColor;
        }
        
        void OnMouseUpEvent(MouseEventBase<MouseUpEvent> evt)
        {
            target.style.backgroundColor = OriginalColor;
        }
    }
}
