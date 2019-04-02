using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoWindow : EditorWindow
{
    private readonly Color[] _colors =
    {
        Color.blue,
        Color.green,
        Color.yellow
    };

    private readonly int kMargin = 50;
    private readonly int kPadding = 10;
    private readonly int kBoxSize = 100;
    
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
            text = "UI Elements",
            style =
            {
                fontSize = 20,
                unityFontStyleAndWeight = FontStyle.Bold,
                width = 140
            }
        });
        
        root.Add(container);

        var grid = new VisualElement
        {
            style =
            {
                display = DisplayStyle.Flex,
                flexDirection = FlexDirection.Row,
                justifyContent = Justify.SpaceBetween
            }
        };
        
        for (var i = 0; i < _colors.Length; i++)
        {
            grid.Add(
                BoxColumn(i)
            );
        }
        
        root.Add(grid);
    }

    private VisualElement BoxColumn(int index)
    {
        bool flipped = index % 2 == 0;
        var direction = flipped ?
             FlexDirection.Column : FlexDirection.ColumnReverse;
        
        var row =  new VisualElement
        {
            style =
            {
                display = DisplayStyle.Flex,
                flexDirection = direction,
                left = kMargin,
                top = flipped ? 0 : kMargin,
                bottom = flipped ? kMargin : 0,
                height = kBoxSize * _colors.Length,
                width = kBoxSize * _colors.Length
            }
        };
        
        for (var i = 0; i < _colors.Length; i++)
        {
            var color = _colors[i];
            
            row.Add(
                BoxElement(color)
            );
        }

        return row;
    }

    private VisualElement BoxElement(Color color)
    {
        return new VisualElement
        {
            style =
            {
                width = kBoxSize,
                height = kBoxSize,
                backgroundColor = color
            }
        };
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
