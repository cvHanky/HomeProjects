using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CharacterChooser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Databound properties
        private Thickness myFrom;

        public Thickness MyFrom
        {
            get { return myFrom; }
            set { myFrom = value; OnPropertyChanged(nameof(MyFrom)); }
        }

        //MyToL and MyToR is databound "To"-properties for the Right and Left side of the animation. 
        private Thickness myToL;

        public Thickness MyToL
        {
            get { return myToL; }
            set { myToL = value; OnPropertyChanged(nameof(MyToL)); }
        }

        private Thickness myToR;

        public Thickness MyToR
        {
            get { return myToR; }
            set { myToR = value; OnPropertyChanged(nameof(MyToR)); }
        }

        private string imgNameL;

        public string ImgNameL
        {
            get { return imgNameL; }
            set { imgNameL = value; OnPropertyChanged(nameof(ImgNameL)); }
        }

        private string imgNameR;

        public string ImgNameR
        {
            get { return imgNameR; }
            set { imgNameR = value; OnPropertyChanged(nameof(ImgNameR)); }
        }

        //private Storyboard sbClone;

        //public Storyboard SbClone
        //{
        //    get { return sbClone; }
        //    set { sbClone = value; OnPropertyChanged(nameof(SbClone)); }
        //}


        public MainWindow()
        {
            InitializeComponent();
        }



        private void Character_MouseEnter(object sender, MouseEventArgs e)
        {
            var image = sender as Image;

            if (image != null)
            {

                Storyboard sbL = (Storyboard)this.Resources["CharHoverLeft"];
                Storyboard sbR = (Storyboard)this.Resources["CharHoverRight"];

                // Clone the storyboard to prevent conflicts. We also make clones of the image, in order to animate when hovering. 
                Storyboard sbCloneL = sbL.Clone();
                Storyboard sbCloneR = sbR.Clone();
                Image cloneLeft = CreateImageClone(image); cloneLeft.Name = "cloneL"; CharacterGrid.Children.Add(cloneLeft);
                Image cloneRight = CreateImageClone(image); cloneRight.Name = "cloneR"; CharacterGrid.Children.Add(cloneRight);


                // Giving value to the properties for databinding. 
                MyFrom = new Thickness(image.Margin.Left, image.Margin.Top, image.Margin.Right, image.Margin.Bottom);
                MyToL = new Thickness(image.Margin.Left - 10, image.Margin.Top, image.Margin.Right, image.Margin.Bottom);
                MyToR = new Thickness(image.Margin.Left + 10, image.Margin.Top, image.Margin.Right, image.Margin.Bottom);
                ImgNameL = cloneLeft.Name;
                ImgNameR = cloneRight.Name;

                cloneLeft.DataContext = new { MyFrom = myFrom, MyToL = myToL };
                cloneRight.DataContext = new {MyFrom = myFrom, MyToR = myToR };

                var animations = sbCloneL.Children.OfType<ThicknessAnimationUsingKeyFrames>().ToList();
                foreach (ThicknessAnimationUsingKeyFrames animation in animations)
                {
                    animation.KeyFrames[0].Value = MyFrom;
                    animation.KeyFrames[1].Value = MyToL;
                }

                animations = sbCloneR.Children.OfType<ThicknessAnimationUsingKeyFrames>().ToList();
                foreach (ThicknessAnimationUsingKeyFrames animation in animations)
                {
                    animation.KeyFrames[0].Value = MyFrom;
                    animation.KeyFrames[1].Value = MyToR;
                }

                // Now we need to give TargetName's to the two Storyboards. Cannot be done using databinding,
                // since the TargetName property is not a DependencyProperty
                Storyboard.SetTarget(sbCloneL, cloneLeft);
                Storyboard.SetTarget(sbCloneR, cloneRight);

                sbCloneL.Begin();
                sbCloneR.Begin();
            }
        }

        private void Character_MouseLeave(object sender, MouseEventArgs e)
        {
            // Since the storyboard clones are instantiated in the other event handler, they cannot be stopped from here.
            // Instead, the cloned images can be removed by targeting all images with names corresponding to the clones.
            // This is done using LINQ. 

            var clonedImages = CharacterGrid.Children.OfType<Image>().Where(img => img.Name == ImgNameL || img.Name == ImgNameR).ToList();
            foreach (var clonedImage in clonedImages)
            {
                CharacterGrid.Children.Remove(clonedImage);
            }
        }

        /// <summary>
        /// Creates a clone of an image, and sets the IsHitTestVisible property to false.
        /// </summary>
        private Image CreateImageClone(Image img)
        {
            Image clone = new Image
            {
                Width = img.Width,
                Height = img.Height,
                Source = img.Source,
                Margin = img.Margin,
                HorizontalAlignment = img.HorizontalAlignment,
                VerticalAlignment = img.VerticalAlignment,
                IsHitTestVisible = false
            };
            return clone;
        }

        #region Notification interface implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}