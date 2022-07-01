using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{


    [SerializeField] private View startView;
    [SerializeField] private View[] views;
    View currentView;
    private readonly Stack<View> history = new Stack<View>();
    
    public static T GetView<T>() where T : View
    {
        for (int i = 0; i < Instance.views.Length; i++)
        {
            if (Instance.views[i] is T tview)
            {
                return tview;
            }
        }
        return null;
    }
    public static void Show<T>(bool remember = true) where T : View
    {
        for (int i = 0; i < Instance.views.Length; i++)
        {
            if (Instance.views[i] is T)
            {
                if (Instance.currentView != null)
                {
                    if (remember)
                    {
                        Instance.history.Push(Instance.currentView);
                    }
                    Instance.currentView.Hide();
                }
                Instance.views[i].Show();
                Instance.currentView = Instance.views[i];
            }

        }

    }
    public static void ShowBoth<T, D>(bool remember = true) where T : View where D : View
    {
        for (int i = 0; i < Instance.views.Length; i++)
        {

            if (Instance.views[i] is T)
            {
                if (Instance.currentView != null)
                {
                    if (remember)
                    {
                        Instance.history.Push(Instance.currentView);
                    }
                    Instance.currentView.Hide();
                }
                Instance.views[i].Show();
                Instance.currentView = Instance.views[i];
            }
        }
        for (int i = 0; i < Instance.views.Length; i++)
        {
            if (Instance.views[i] is D)
            {
                Instance.views[i].Show();
            }
        }
    }
    //Call this when we want to show two screens at a time
    public static void ShowBoth(View viewFront, View viewBack, bool remember = true)
    {
        if (Instance.currentView != null)
        {
            if (remember)
            {
                Instance.history.Push(Instance.currentView);
            }
            Instance.currentView.Hide();
        }
        viewBack.Show();
        viewFront.Show();

        Instance.currentView = viewFront;
    }
    //Call this when we want to show one screen at a time
    public static void Show(View view, bool remember = true)
    {

        if (Instance.currentView != null)
        {
            if (remember)
            {
                Instance.history.Push(Instance.currentView);
            }
            Instance.currentView.Hide();
        }
        view.Show();
        Instance.currentView = view;
    }
    public void HideAll()
    {
        for (int i = 0; i < views.Length; i++)
        {

            views[i].Hide();
        }
    }
    private void Start()
    {
        for (int i = 0; i < views.Length; i++)
        {
            views[i].Initialize();
            views[i].Hide();
        }

        Show(startView, true);

    }
    //It can be used further if we will have more screens to work with
    //Call this on click of back button 
    public static void ShowLast()
    {
        if (Instance.history.Count != 0)
        {
            Show(Instance.history.Pop(), false);
        }
    }
}

