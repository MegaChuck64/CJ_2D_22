using System.Windows;
using Syncfusion.SfSkinManager;
using CJ_2D_22.Managers;
using System;
using Syncfusion.Windows.Tools.Controls;
using System.Windows.Controls;
using Syncfusion.Windows.Shared;

namespace CJ_2D_22;
public partial class MainWindow : Window
{
    //private DrawingArea drawingArea;
    private MainGraphicsWindow mainGraphicsWindow;
    public MainWindow()
    {
        //drawingArea = new DrawingArea(this);
        mainGraphicsWindow = new MainGraphicsWindow(this);

        InitializeComponent();
       // DockingManager.LoadDockState();
        LogManager.OnLogAdded += ErrorManager_OnErrorAdded;

        SettingsManager.Initialize();

        PopulateThemeDropdown();

        PopulateViewDropdown();

        UpdateTheme();
        GameGrid.Children.Add(mainGraphicsWindow);
        //GameGrid.Children.Add(drawingArea);
    }

    private void ErrorManager_OnErrorAdded(object? sender, LogAddedEventArgs e)
    {
        LogGrid.Items.Add(
            new ErrorDisplayItem 
            { 
                Time = e.Time, 
                Message = e.Message, 
                Sender = sender as string ?? string.Empty 
            });
    }

    private void PopulateThemeDropdown()
    {
        foreach (var thm in SettingsManager.Themes)
        {
            var itm = new CheckListBoxItem
            {
                Content = thm,
                IsChecked = thm == SettingsManager.ChosenTheme
            };

            ThemeDropdown.Items.Add(itm);
        }
    }


    private void PopulateViewDropdown()
    {
        foreach (ContentControl pg in DockingManager.Children)
        {
            var newMenuItem = new MenuItemAdv() { Header = DockingManager.GetHeader(pg) };
            newMenuItem.Click += NewMenuItem_Click;
            ViewMenu.Items.Add(newMenuItem);
        }
    }

    private void NewMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var menuItem = sender as MenuItemAdv;
        var header = menuItem.Header as string;

        foreach (ContentControl pg in DockingManager.Children)
        {
            if (DockingManager.GetHeader(pg) as string == header)
            {
                DockingManager.ExecuteRestore(pg, DockState.Dock);
            }
        }
        
    }

    private void ThemeDropdown_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
        if (e.Checked)
        {
            var newTheme = (e.Item as CheckListBoxItem).Content.ToString();

            if (SettingsManager.ChosenTheme != newTheme)
            {
                ThemeDropdown.ItemChecked -= ThemeDropdown_ItemChecked; //remove this event to prevent recursive

                SettingsManager.ChangeChosenTheme(newTheme);
                
                UpdateTheme();
                
                //these two lines would cause the recursive function. They trigger ItemChange event
                ThemeDropdown.SelectedItems.Clear(); 
                ThemeDropdown.SelectedItems.Add(e.Item as CheckListBoxItem);
                
                ThemeDropdown.ItemChecked += ThemeDropdown_ItemChecked;

            }
        }
        else
        {
            ThemeDropdown.ItemChecked -= ThemeDropdown_ItemChecked; //remove this event to prevent recursive
            ThemeDropdown.SelectedItems.Add(e.Item as CheckListBoxItem);
            ThemeDropdown.ItemChecked += ThemeDropdown_ItemChecked;
        }

    }

    private void UpdateTheme()
    {
        var theme = SettingsManager.ChosenTheme;
        if (!string.IsNullOrWhiteSpace(theme))
            SfSkinManager.SetTheme(this, new Theme(theme));
    }

    private void ColorPicker_ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var col = (System.Windows.Media.Color)e.NewValue;
        mainGraphicsWindow.BrushColor = new Microsoft.Xna.Framework.Color(col.R, col.G, col.B, col.A);
    }
}

public class ErrorDisplayItem
{
    public DateTime Time { get; set; }
    public string? Message { get; set; }
    public string? Sender { get; set; }
}

