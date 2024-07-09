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
        #region Properties

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

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            // Initialize borders around grids.
            foreach (RowDefinition rd in CharacterGrid.RowDefinitions)
            {
                foreach (ColumnDefinition cd in CharacterGrid.ColumnDefinitions)
                {
                    Border border = new Border();
                    Grid.SetRow(border, CharacterGrid.RowDefinitions.IndexOf(rd));
                    Grid.SetColumn(border, CharacterGrid.ColumnDefinitions.IndexOf(cd));
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1.5);
                    CharacterGrid.Children.Add(border);
                }
            }
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
                Image cloneLeft = CreateImageClone(image, CharacterGrid, Grid.GetRow(image), Grid.GetColumn(image)); cloneLeft.Name = "cloneL"; CharacterGrid.Children.Add(cloneLeft);
                Image cloneRight = CreateImageClone(image, CharacterGrid, Grid.GetRow(image), Grid.GetColumn(image)); cloneRight.Name = "cloneR"; CharacterGrid.Children.Add(cloneRight);


                // Giving value to the properties for databinding. 
                MyFrom = new Thickness(image.Margin.Left, image.Margin.Top, image.Margin.Right, image.Margin.Bottom);
                MyToL = new Thickness(image.Margin.Left - 20, image.Margin.Top, image.Margin.Right, image.Margin.Bottom);
                MyToR = new Thickness(image.Margin.Left + 20, image.Margin.Top, image.Margin.Right, image.Margin.Bottom);
                ImgNameL = cloneLeft.Name;
                ImgNameR = cloneRight.Name;

                cloneLeft.DataContext = new { MyFrom = myFrom, MyToL = myToL };
                cloneRight.DataContext = new { MyFrom = myFrom, MyToR = myToR };

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

        private void Character_Choose(object sender, MouseEventArgs e)
        {
            var image = sender as Image;

            if (image != null)
            {
                // The image is made unhittable in order to stop the idle animation. Also prevents pressing it multiple times. 
                // All other character images are made HitTestVisible to let the idle animation play on the other images still. 
                image.IsHitTestVisible = false;
                image.Opacity = 0;
                foreach (var charImage in CharacterGrid.Children.OfType<Image>().Where(img => img != image && img.Name != ImgNameL && img.Name != ImgNameR).ToList())
                {
                    charImage.IsHitTestVisible = true;
                    charImage.Opacity = 1;
                }

                // First, the current chosen character is removed from the screen (if it exists). 
                foreach (var canv in CharacterGrid.Children.OfType<Canvas>().Where(canvas => canvas.Name == "ChosenCharacterCanvas").ToList())
                {
                    CharacterGrid.Children.Remove(canv);
                }
                foreach (var bigImg in ChosenCharacterGrid.Children.OfType<Image>().Where(img => img.Name == "ChosenImage").ToList())
                {
                    ChosenCharacterGrid.Children.Remove(bigImg);
                }

                // Then, a canvas is created for the new chosen character. 
                Canvas canvas = new Canvas
                {
                    Width = image.Width,
                    Height = image.Height,
                    Name = "ChosenCharacterCanvas"
                };
                Grid.SetRow(canvas, Grid.GetRow(image));
                Grid.SetColumn(canvas, Grid.GetColumn(image));
                CharacterGrid.Children.Add(canvas);

                // Now, the image is cloned for animation. Also it is added as a child to the canvas. 
                Image canvImage = CreateImageClone(image); canvImage.Name = "canvasImage"; canvas.Children.Add(canvImage);

                // Another image is cloned, used for the latter part of the animation. 
                Image bigImage = CreateImageClone(image, ChosenCharacterGrid, 1, 1); bigImage.Name = "ChosenImage";
                bigImage.Width = 1; //115
                bigImage.Height = 300;
                bigImage.Opacity = 0;
                ChosenCharacterGrid.Children.Add(bigImage);

                // Finally the storyboard is setup.
                Storyboard sb = new Storyboard();

                // Animations are added.
                double duration = 1;
                // Animation for the small images to disappear upwards. 
                DoubleAnimationUsingKeyFrames yMovement = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(duration)
                };

                yMovement.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    Value = 0,
                    KeyTime = TimeSpan.FromSeconds(0)
                });
                yMovement.KeyFrames.Add(new SplineDoubleKeyFrame
                {
                    Value = 20,
                    KeyTime = TimeSpan.FromSeconds(duration / 3),
                    KeySpline = new KeySpline(0.5, 0, 0.5, 1)
                });
                yMovement.KeyFrames.Add(new SplineDoubleKeyFrame
                {
                    Value = -200,
                    KeyTime = TimeSpan.FromSeconds(duration),
                    KeySpline = new KeySpline(0.5, 0, 0.5, 1)
                });
                Storyboard.SetTarget(yMovement, canvImage);
                Storyboard.SetTargetProperty(yMovement, new PropertyPath(Canvas.TopProperty));
                sb.Children.Add(yMovement);

                DoubleAnimationUsingKeyFrames enlargeChosenChar = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(duration + 1)
                };

                enlargeChosenChar.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    Value = 1,
                    KeyTime = TimeSpan.FromSeconds(0)
                });
                enlargeChosenChar.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    Value = 1,
                    KeyTime = TimeSpan.FromSeconds(duration)
                });
                enlargeChosenChar.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    Value = 115,
                    KeyTime = TimeSpan.FromSeconds(duration + 1)
                });
                Storyboard.SetTarget(enlargeChosenChar, bigImage);
                Storyboard.SetTargetProperty(enlargeChosenChar, new PropertyPath(Image.WidthProperty));
                sb.Children.Add(enlargeChosenChar);

                DoubleAnimationUsingKeyFrames visibilityChosenChar = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(duration + 1)
                };

                visibilityChosenChar.KeyFrames.Add(new LinearDoubleKeyFrame
                {
                    Value = 0,
                    KeyTime = TimeSpan.Zero
                });
                visibilityChosenChar.KeyFrames.Add(new DiscreteDoubleKeyFrame
                {
                    Value = 1,
                    KeyTime = TimeSpan.FromSeconds(duration)
                });
                Storyboard.SetTarget(visibilityChosenChar, bigImage);
                Storyboard.SetTargetProperty(visibilityChosenChar, new PropertyPath(Image.OpacityProperty));
                sb.Children.Add(visibilityChosenChar);

                sb.Begin();
            }
        }

        /// <summary>
        /// Creates a clone of an image, and sets the IsHitTestVisible property to false. Also sets the row and column to the same as the image.
        /// </summary>
        private Image CreateImageClone(Image img, Grid targetGrid, int row, int column)
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
            Grid.SetRow(clone, row);
            Grid.SetColumn(clone, column);
            return clone;
        }

        /// <summary>
        /// Creates a clone of an image, and sets the IsHitTestVisible property to false. Does not set the row and column (used when adding image to canvas).
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