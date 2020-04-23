using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnSauna.Frontend.Phone.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Toggle : Grid
    {
        bool selection1Active = true;
        bool selection2Active;
        private double valueX, valueY;
        private bool IsTurnX, IsTurnY;

        public ICommand Option1Command
        {
            get => GetValue(Option1CommandProperty) as ICommand;
            set => SetValue(Option1CommandProperty, value);
        }

        public static readonly BindableProperty Option1CommandProperty = BindableProperty.Create(
            propertyName: nameof(Option1Command),
            returnType: typeof(ICommand),
            declaringType: typeof(Toggle));

        public ICommand Option2Command
        {
            get => GetValue(Option2CommandProperty) as ICommand;
            set => SetValue(Option2CommandProperty, value);
        }

        public static readonly BindableProperty Option2CommandProperty = BindableProperty.Create(
            propertyName: nameof(Option2Command),
            returnType: typeof(ICommand),
            declaringType: typeof(Toggle));

        public Toggle()
        {
            InitializeComponent();
        }

        public void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var x = e.TotalX; // TotalX Left/Right
            var y = e.TotalY; // TotalY Up/Down

            // StatusType
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Check that the movement is x or y
                    if ((x >= 5 || x <= -5) && !IsTurnX && !IsTurnY)
                    {
                        IsTurnX = true;
                    }

                    if ((y >= 5 || y <= -5) && !IsTurnY && !IsTurnX)
                    {
                        IsTurnY = true;
                    }

                    // X (Horizontal)
                    if (IsTurnX && !IsTurnY)
                    {
                        if (x <= valueX)
                        {
                            // Left
                            if (selection2Active)
                            {
                                //make selection1 move to selection 2

                                selection1Active = true;
                                selection2Active = false;
                                text2.TextColor = Color.Black;
                                text1.TextColor = Color.White;
                                runningFrame.TranslateTo(runningFrame.X, 0, 120);
                            }
                        }

                        if (x >= valueX)
                        {
                            // Right
                            if (selection1Active)
                            {
                                //make selection2 move to selection 1
                                selection1Active = false;
                                selection2Active = true;
                                text2.TextColor = Color.White;
                                text1.TextColor = Color.Black;
                                runningFrame.TranslateTo(runningFrame.X + 150, 0, 120);
                            }
                        }
                    }


                    break;
                case GestureStatus.Completed:
                    valueX = x;
                    valueY = y;

                    IsTurnX = false;
                    IsTurnY = false;

                    break;
            }
        }

        void OnText1Tapped(object sender, EventArgs e)
        {
            if (selection2Active)
            {
                //make selection1 move to selection 2

                selection1Active = true;
                selection2Active = false;
                text2.TextColor = Color.Black;
                text1.TextColor = Color.White;
                runningFrame.TranslateTo(runningFrame.X, 0, 120);
            }

            if (Option1Command != null && Option1Command.CanExecute(null))
            {
                Option1Command.Execute(null);
            }
        }

        void OnText2Tapped(object sender, EventArgs e)
        {
            if (selection1Active)
            {
                //make selection2 move to selection 1
                selection1Active = false;
                selection2Active = true;
                text2.TextColor = Color.White;
                text1.TextColor = Color.Black;
                runningFrame.TranslateTo(runningFrame.X + 150, 0, 120);
            }

            if (Option2Command != null && Option2Command.CanExecute(null))
            {
                Option2Command.Execute(null);
            }
        }
    }
}