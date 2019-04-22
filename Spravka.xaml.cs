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
    public partial class Spravka : ContentPage
    {
        public Spravka()
        {
            InitializeComponent();
            DateTime.Text = System.DateTime.Now.ToString("dd.MM.yyyy");
        }
        public void TrailNumber_completed(object sender, EventArgs e)
        {
            EntryLadenWagon.Focus();
        }
        public void LadenWagon_completed(object sender, EventArgs e)
        {
            EntryEmptyWagon.Focus();
        }
        public void EmptyWagon_completed(object sender, EventArgs e)
        {
            EntryTrailWeight.Focus();
        }
        public void TrailWeight_completed(object sender, EventArgs e)
        {
            EntryLastWagonNumber.Focus();
        }
        public void Calculate_click(object sender, EventArgs e)
        {
            int Laden = Convert.ToInt32(EntryLadenWagon.Text);
            int LadenAxis = Laden * 4;
            int Empty = Convert.ToInt32(EntryEmptyWagon.Text);
            int EmptyAxis = Empty * 4;
            if (EntryTrailNumber.Text == String.Empty || EntryTrailWeight.Text == String.Empty || !(Laden + Empty > 0))
            {
                DisplayAlert("Внимание!!!", $"Введены не все необходимые данные", "Ок");
                return;
            }
            int Number = Convert.ToInt32(EntryTrailNumber.Text);
            int Uklon = GetUklon(Number);
            int Weight = Convert.ToInt32(EntryTrailWeight.Text);
            string LastWagon = EntryLastWagonNumber.Text;

            if (Uklon == 0) DisplayAlert("Внимание!!!", $"Не найден уклон для позда с номером {Number}. Скажите мне чтобы поправил!", "Ок");
            int HandsBreak = (int)Math.Round(Weight * Uklon / 1000.0f);
            if (HandsBreak % 2 > 0) HandsBreak--;
            int Kof = Laden > 0 ? 33 : 55;
            int Fact = (int)(LadenAxis * 7 + EmptyAxis * 3.5f);
            int Needed = GetNeeded(Weight, Fact, ref Kof);
            SpravkaResult Result = new SpravkaResult(Number, Weight, Needed, HandsBreak, EmptyAxis, LadenAxis, Kof, LastWagon);
            Navigation.PushAsync(Result);
        }
        public void Reset_click(object sender, EventArgs e)
        {
            EntryLadenWagon.Text = "";
            EntryEmptyWagon.Text = "";
            EntryTrailNumber.Text = "";
            EntryTrailWeight.Text = "";
            EntryLastWagonNumber.Text = "";
        }
        private int GetUklon(int number)
        {
            if (number > 9500) return 6;
            else if (number > 3639) return 7;
            else if (number > 3609) return 4;
            else if (number > 3499) return 4;
            else if (number > 1649) return 8;
            else return (0);
        }
        private int GetNeeded(int weight, int fact, ref int kof)
        {
            int counter = 0;
            while (fact < weight * kof / 100)
            {
                kof--;
                counter++;
            }
            if (counter > 3) DisplayAlert("Внимание!!!", $"Не хватает нажатия - проверь данные {fact} /{weight} / {weight * kof / 100} / {kof} / {counter}", "Ок");
            return weight * kof / 100;
        }
    }
}