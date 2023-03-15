using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTutorials.ItemsControlDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}

public class ViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// 一些字符串，用来展示基础的 ItemsSource 绑定，奇偶行交替颜色，以及表头
    /// </summary>
    public ObservableCollection<string> Stars { get; } =
        new() { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };

    /// <summary>
    /// 用来展示替换 ItemsPanel 为 Canvas，以及修改 ItemContainerStyle
    /// </summary>
    public ObservableCollection<Shape> Shapes { get; } =
        new()
        {
            new(0, new(50, 50), Brushes.Red),
            new(1, new Point(150, 70), Brushes.Green),
            new(1, new Point(300, 160), Brushes.Blue),
            new(0, new Point(240, 260), Brushes.Yellow),
        };

    /// <summary>
    /// 用于展示在 Resources 中存放多个 DataType 不同的 DataTemplate 的效果
    /// </summary>
    public ObservableCollection<Fruit> Fruits { get; } =
        new()
        {
            new Apple { Amount = 3 },
            new Apple { Amount = 2 },
            new Banana { Amount = 6 },
            new Apple { Amount = 4 },
            new Banana { Amount = 1 },
            new Banana { Amount = 5 },
        };

    /// <summary>
    /// 用于展示 DataTemplateSelector 的使用
    /// </summary>
    public ObservableCollection<Employee> Employees { get; } =
        new()
        {
            new("John", Gender.Male, 27),
            new("Anna", Gender.Female, 31),
            new("Joyce", Gender.Female, 28),
            new("Tony", Gender.Male, 40),
            new("Brian", Gender.Male, 36)
        };

    public event PropertyChangedEventHandler? PropertyChanged;
}

#region Canvas

public record Shape(int Type, Point Pos, Brush Color);

#endregion

#region Fruits

public abstract class Fruit
{
    public int Amount { get; set; }
    public string FruitType => this.GetType().Name;
}

public class Apple : Fruit { }

public class Banana : Fruit { }

#endregion

#region Employees

public record Employee(string Name, Gender Gender, int Age);

public enum Gender
{
    Male,
    Female
};

public class EmployeeTemplateSelector : DataTemplateSelector
{
    public DataTemplate MaleTemplate { get; set; }
    public DataTemplate FemaleTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        var element = container as FrameworkElement;
        var employee = item as Employee;
        return employee!.Gender switch
        {
            Gender.Male => MaleTemplate,
            Gender.Female => FemaleTemplate,
            _ => throw new ArgumentException()
        };
    }
}

#endregion
