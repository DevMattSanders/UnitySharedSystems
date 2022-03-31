using System.Linq;
using DevMattSanders._CoreSystems;
using UnityEditor;
using UnityEngine;

namespace Dev_Matt_Sanders._CoreSystems
{
    [InitializeOnLoad]
    public class HierarchyHighlightManager
    {
        //==============================================================================
        //
        //                                    CONSTANTS
        //
        //==============================================================================



        public static readonly Color DefaultColorHierarchySelected = new Color(0.243f, 0.4901f, 0.9058f, 1f);







        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        static HierarchyHighlightManager()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= HierarchyHighlight_OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyHighlight_OnGUI;
        }







        //==============================================================================
        //
        //                                    EVENTS
        //
        //==============================================================================



        private static void HierarchyHighlight_OnGUI(int inSelectionID, Rect inSelectionRect)
        {
            GameObject goLabel = EditorUtility.InstanceIDToObject(inSelectionID) as GameObject;

            if (goLabel != null)
            {
                HierarchyHighlighter label = goLabel.GetComponent<HierarchyHighlighter>();

                if(label != null && Event.current.type == EventType.Repaint)
                {
                    #region Determine Styling

                    bool objectIsSelected = Selection.instanceIDs.Contains(inSelectionID);

                    Color bkCol = label.Background_Color;
                    Color textCol = label.Text_Color;
                    FontStyle textStyle = label.TextStyle;

                    if(!label.isActiveAndEnabled)
                    {
                        if(label.Custom_Inactive_Colors)
                        {
                            bkCol = label.Background_Color_Inactive;
                            textCol = label.Text_Color_Inactive;
                            textStyle = label.TextStyle_Inactive;
                        }
                        else
                        {
                            if(bkCol != HierarchyHighlighter.DEFAULT_BACKGROUND_COLOR)
                                bkCol.a = bkCol.a * 0.5f; //Reduce opacity by half

                            textCol.a = textCol.a * 0.5f;
                        }
                    }

                    #endregion


                    Rect offset = new Rect(inSelectionRect.position + new Vector2(2f, 0f), inSelectionRect.size);


                    #region Draw Background

                    //Only draw background if background color is not completely transparent
                    if (bkCol.a > 0f)
                    {
                        Rect backgroundOffset = new Rect(inSelectionRect.position, inSelectionRect.size);

                        //If the background has transparency, draw a solid color first
                        if (label.Background_Color.a < 1f || objectIsSelected)
                        {
                            EditorGUI.DrawRect(backgroundOffset, HierarchyHighlighter.DEFAULT_BACKGROUND_COLOR);
                        }

                        //Draw background
                        EditorGUI.DrawRect(backgroundOffset,
                            objectIsSelected ? Color.Lerp(GUI.skin.settings.selectionColor, bkCol, 0.3f) : bkCol);
                    }

                    #endregion


                    EditorGUI.LabelField(offset, goLabel.name, new GUIStyle()
                    {
                        normal = new GUIStyleState() { textColor = textCol },
                        fontStyle = textStyle
                    });

                    EditorApplication.RepaintHierarchyWindow();
                }
            }
        }
    }
}