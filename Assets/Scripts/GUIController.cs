using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GUIController : MonoBehaviour
{

    private static GUIController instance;
    public static GUIController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GUIController");
                go.AddComponent<GUIController>();
            }

            return instance;
        }
    }

    [SerializeField]
    private GUIScreen[] currentScreens = new GUIScreen[0];

    private List<GUIScreen> screens;
    private Stack<GUIScreen> screenStack = new Stack<GUIScreen>();

    void Awake()
    {
        screens = currentScreens.ToList();
        instance = this;
    }

    void Start()
    {

        foreach (var curScreen in screens)
        {
            curScreen.gameObject.SetActive(false);
        }
        
    }

    public T FoundScreen<T>() where T: GUIScreen
    {
        foreach (var curScreen in screens)
        {
            if (curScreen.GetType() == typeof(T))
                return curScreen as T;
        }
        return null;
    }

    public GUIScreen FoundScreen(Type typeScreen)
    {
        foreach (var curScreen in screens)
        {
            if (curScreen.GetType() == typeScreen)
                return curScreen;
        }
        return null;
    }

    public void ShowScreen<T>(bool hideAll = false) where T : GUIScreen
    {
        GUIScreen foundScreen = FoundScreen<T>();
        if (foundScreen != null)
        {
            ShowScreen(foundScreen, hideAll);
        }
        
    }

    public void ShowScreen(GUIScreen screen, bool hideAll = false)
    {
        if (hideAll)
        {
            foreach (var curScreen in screens)
            {
                if (curScreen.GetType() != screen.GetType())
                    curScreen.Hide();
            }
            screenStack.Clear();
        }

        screen.Show();
    }

    public void PushScreen<T>(bool isPopup) where T: GUIScreen
    {
        GUIScreen foundScreen = FoundScreen<T>();
        if (foundScreen != null)
        { 
            PushScreen(foundScreen, isPopup);
        }
    }

    public void PushScreen(GUIScreen screen, bool isPopup)
    {
        if (!isPopup)
        {
            foreach (var curScreen in screenStack)
            { 
                curScreen.Hide();
            }
        }
        screen.Show();
        screenStack.Push(screen);

        int i = 0;

    }

    public void PopScreen()
    {
        if (screenStack.Count == 0)
            return;

        GUIScreen curScreen = screenStack.Pop();
        curScreen.Hide();

        if (screenStack.Count > 0)
        {
            GUIScreen newScreen = screenStack.Peek();
            newScreen.Show();
        }
    }

    public void HideAll()
    {
        foreach (var curScreen in screens)
        {
            curScreen.Hide();
        }
        screenStack.Clear();
    }

    public void HideScreen<T>() where T : GUIScreen
    {
        GUIScreen foundScreen = FoundScreen<T>();
        if (foundScreen != null)
        {
            foundScreen.Hide();
        }
    }

}
