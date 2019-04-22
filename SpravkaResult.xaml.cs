using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kaliy3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpravkaResult : ContentPage
    {
        int[][] ConfigValue = new int[][]
        {
            new int[]{11,12,13,14,15,16},
            new int[]{21,22,23,24,25,26},
            new int[]{31,32,33,34,35,36},
            new int[]{41,42,43,44,45,46},
            new int[]{51,52,53,54,55,56}
        };
        int axis = 0;
        public SpravkaResult(int number, int weight, int needed, int hands, int empty, int laden, int kof, string last)
        {
            InitializeComponent();
            LabelNumber.Text = number.ToString();
            LabelWeight.Text = weight.ToString();
            LabelNeeded.Text = needed.ToString();
            LabelHand.Text = hands.ToString();
            LabelEmpty.Text = empty.ToString();
            LabelEmptyAxis.Text = (empty * 3.5).ToString();
            LabelLaden.Text = laden.ToString();
            LabelLadenAxis.Text = (laden * 7).ToString();
            axis = empty + laden;
            LabelTotal.Text = (axis).ToString();
            LabelTotalAxis.Text = (laden*7 + empty*3.5).ToString();
            LabelKof.Text = $"100/{kof}";
            LabelLast.Text = last;
            LabelValue.Text = GetValueTrail(BtnTrail.Text, axis);
        }

        private string GetValueTrail(string text, int axis)
        {
            int index1;
            int index2;
            switch (text)
            {
                case "2ТЭ10У":
                    index1 = 0;
                    break;
                case "2ТЭ10М":
                    index1 = 1;
                    break;
                case "ЧМЭ3":
                    index1 = 2;
                    break;
                case "М62":
                    index1 = 3;
                    break;
                case "2М62":
                    index1 = 4;
                    break;
                default: index1 = 0;
                    break;
            }
            if (axis > 300) index2 = 0;
            else if (axis > 250) index2 = 1;
            else if (axis > 200) index2 = 2;
            else if (axis > 150) index2 = 3;
            else if (axis > 100) index2 = 4;
            else index2 = 5;

            return (ConfigValue[index1][index2]).ToString();
        }

        private async void SetTrail_click(object o, EventArgs e)
        {
            string Result = await DisplayActionSheet("Выбор тепловоза", "Отмена", null, "2ТЭ10У", "2ТЭ10М", "ЧМЭ3", "М62", "2М62");
            BtnTrail.Text = Result;
            LabelValue.Text = GetValueTrail(Result, axis);
        }
    }
}