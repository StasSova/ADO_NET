using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace _13_Exam_BookShop.Views.Component;

/// <summary>
/// Interaction logic for C_Book.xaml
/// </summary>
public partial class C_Book : UserControl
{
    public C_Book()
    {
        InitializeComponent();
        DataContext = this;
        //Image = @"https://images.unsplash.com/photo-1707808512103-23f911fab68a?w=500&amp;auto=format&amp;fit=crop&amp;q=60&amp;ixlib=rb-4.0.3&amp;ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxNHx8fGVufDB8fHx8fA%3D%3D";
        //SellingPrice = 10
    }

    // Define dependency properties for book properties

    public static readonly DependencyProperty IdProperty =
    DependencyProperty.Register("Id", typeof(int), typeof(C_Book), new PropertyMetadata(0));

    public int Id
    {
        get { return (int)GetValue(IdProperty); }
        set { SetValue(IdProperty, value); }
    }

    public static  DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(C_Book), new FrameworkPropertyMetadata("test", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
    
    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register("Image", typeof(string), typeof(C_Book), new PropertyMetadata(null));
    public string Image
    {
        get { return (string)GetValue(ImageProperty); }
        set { SetValue(ImageProperty, value); }
    }
    
    public static readonly DependencyProperty AuthorProperty =
        DependencyProperty.Register("Author", typeof(string), typeof(C_Book), new PropertyMetadata(null));
    public string Author
    {
        get { return (string)GetValue(AuthorProperty); }
        set { SetValue(AuthorProperty, value); }
    }

    public static readonly DependencyProperty GenresProperty =
                DependencyProperty.Register("Genres", typeof(IEnumerable<string>), typeof(C_Book), new PropertyMetadata(null));
    public IEnumerable<string> Genres
    {
        get { return (IEnumerable<string>)GetValue(GenresProperty); }
        set { SetValue(GenresProperty, value); }
    }

    public static readonly DependencyProperty SellingPriceProperty =
        DependencyProperty.Register("SellingPrice", typeof(decimal), typeof(C_Book), new PropertyMetadata(0m));
    public decimal SellingPrice
    {
        get { return (decimal)GetValue(SellingPriceProperty); }
        set { SetValue(SellingPriceProperty, value); }
    }
    
    public static readonly DependencyProperty DiscountProperty =
        DependencyProperty.Register("Discount", typeof(decimal), typeof(C_Book), new PropertyMetadata(0m));
    public decimal Discount
    {
        get { return (decimal)GetValue(DiscountProperty); }
        set { SetValue(DiscountProperty, value); }
    }

    // Зависимые свойства для команд "Лайк" и "Добавить в корзину"
    public static readonly DependencyProperty OnLikeCommandProperty = DependencyProperty.Register(
        "OnLikeCommand", typeof(ICommand), typeof(C_Book), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnCartCommandProperty = DependencyProperty.Register(
        "OnCartCommand", typeof(ICommand), typeof(C_Book), new PropertyMetadata(default(ICommand)));

    public ICommand OnLikeCommand
    {
        get => (ICommand)GetValue(OnLikeCommandProperty);
        set => SetValue(OnLikeCommandProperty, value);
    }

    public ICommand OnCartCommand
    {
        get => (ICommand)GetValue(OnCartCommandProperty);
        set => SetValue(OnCartCommandProperty, value);
    }


    private void OnCartCommand_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is C_Book book && OnCartCommand != null)
        {
            OnCartCommand.Execute(book.Id);
        }
    }

    private void OnLikeCommand_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is C_Book book && OnLikeCommand != null)
        {
            OnLikeCommand.Execute(book.Id);
        }
    }
}
