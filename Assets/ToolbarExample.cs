using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using PopupWindow = UnityEngine.UIElements.PopupWindow;

public class ToolbarExample : EditorWindow
{
    private bool PopupSearchFieldOn { get; set; }
    private TextField TextInput { get; set; }

    private const string StyleSheetPath = "Assets/toolbar.uss";
    
    #region MenuItem

    [MenuItem("Demo/Toolbar Example")]
    public static void ToolbarExampleMenuOpen()
    {
        var window = GetWindow<ToolbarExample>();
        window.minSize = new Vector2(700, 200);
        window.titleContent = new GUIContent("Toolbar Example");
    }

    #endregion

    private void OnEnable()
    {
        var root = rootVisualElement;
        rootVisualElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath));

        var toolbar = new Toolbar();
        root.Add(toolbar);

        var button = new ToolbarButton {text = "Click Me!"};
        toolbar.Add(button);

        var spacer = new ToolbarSpacer();
        toolbar.Add(spacer);

        var toggle = new ToolbarToggle {text = "Toggle Me!"};
        toolbar.Add(toggle);

        var flexSpacer = new ToolbarSpacer {name = "flexSpacer", flex = true};
        toolbar.Add(flexSpacer);

        var toolbarMenu = new ToolbarMenu {text = "Menu Me!"};
        toolbarMenu.menu.AppendAction("Default is never shown", a => { }, a => DropdownMenuAction.Status.None);
        toolbarMenu.menu.AppendAction("Normal Menu", a => { }, a => DropdownMenuAction.Status.Normal);
        toolbarMenu.menu.AppendAction("Hidden is never shown", a => { }, a => DropdownMenuAction.Status.Hidden);
        toolbarMenu.menu.AppendAction("Checked menu", a => { }, a => DropdownMenuAction.Status.Checked);
        toolbarMenu.menu.AppendAction("Disabled menu", a => { }, a => DropdownMenuAction.Status.Disabled);
        toolbarMenu.menu.AppendAction("Disabled and checked menu", a => { }, a => DropdownMenuAction.Status.Disabled | DropdownMenuAction.Status.Checked);
        toolbar.Add(toolbarMenu);
        
        var anotherFlexSpacer = new ToolbarSpacer { name = "flexSpacer2", flex = true };
        toolbar.Add(anotherFlexSpacer);

        var popup = new ToolbarMenu
        {
            text = "Pop-UP!",
            variant = ToolbarMenu.Variant.Popup
        };
        popup.menu.AppendAction("Popup", a => { }, a => DropdownMenuAction.Status.Normal);
        toolbar.Add(popup);
        
        var popupSearchField = new ToolbarPopupSearchField();
        popupSearchField.RegisterValueChangedCallback(OnSearchTextChanged);
        popupSearchField.menu.AppendAction(
            "Search Field that POPS UP",
            a => PopupSearchFieldOn = !PopupSearchFieldOn,
            a => PopupSearchFieldOn ?
                DropdownMenuAction.Status.Checked :
                DropdownMenuAction.Status.Normal
        );
        toolbar.Add(popupSearchField);

        var popupWindowContainer = new VisualElement();
        popupWindowContainer.AddToClassList("dat-task");
        var popupWindow = new PopupWindow();
        popupWindow.text = "POP ME UP SCOTTY";
        
        popupWindowContainer.Add(popupWindow);
        root.Add(popupWindowContainer);

        TextInput = new TextField
        {
            name = "input",
            viewDataKey = "input",
            isDelayed = true
        };
        popupWindow.Add(TextInput);
        TextInput.RegisterCallback<ChangeEvent<string>>(AddTask);
    }

    private void AddTask(ChangeEvent<string> evt)
    {
        if (!string.IsNullOrEmpty(TextInput.text))
        {
            // add the damn TASK BOIIIIIIIIII
        }
    }

    private void OnSearchTextChanged(ChangeEvent<string> evt)
    {
        Debug.Log(evt.newValue);
    }
}
