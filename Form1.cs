using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Citis
{
	public partial class Form1 : Form
	{
		const string fileName = "$data.txt";
        string nl = Environment.NewLine;
        private List<string> allCities = new List<string>();
        private List<string> usedCities = new List<string>();
		private string LastCity = "";
		private char LastLetter;
		Random rnd = new Random();

		public Form1()
		{
			InitializeComponent();
			Init();
		}

        private void Init()
		{
			allCities = File.ReadAllLines(fileName).ToList();

			LastCity = allCities[rnd.Next(allCities.Count)];
			LastLetter = LastCity.Last();

            usedCities.Add(LastCity);
            allCities.Remove(LastCity);

			if ("ъьйёы".Contains(LastLetter))
				LastLetter = LastCity[LastCity.Length - 2];

            textBox1.AppendText("Компьютер говори: " + LastCity + nl);
		}

        private void CompMove()
        {
            List<string> useable = new List<string>();

            foreach (string e in allCities)
                if (e.ToLower().First() == LastLetter)
                    useable.Add(e);

            if (useable.Count == 0)
            {
                textBox1.AppendText("Вы виграли!" + nl);
                return;
            }

            LastCity = useable[rnd.Next(useable.Count)];
            LastLetter = LastCity.Last();

            usedCities.Add(LastCity);
            allCities.Remove(LastCity);

            if ("ъьйёы".Contains(LastLetter))
                LastLetter = LastCity[LastCity.Length - 2];

            textBox1.AppendText("Компьютер говори: " + LastCity + nl);

            useable = new List<string>();

            foreach (string e in allCities)
                if (e.ToLower().First() == LastLetter)
                    useable.Add(e);

            if (useable.Count == 0)
            {
                textBox1.AppendText("Вы проиграли!" + nl);
                return;
            }
        }

		private void PlayerMove(string word)
		{
            if (!allCities.Any(c => word == c))
            {
                MessageBox.Show("Слово должно быть городом!");
                return;
            }

            if (usedCities.Any( str => str == word))
            {
                MessageBox.Show("Город уже был использован!");
                return;
            }

            if (LastLetter != char.ToLower(word.First()))
			{
				MessageBox.Show("Новый город должен начинаться с буквы " + LastLetter + "!");
                return;
			}

			LastCity = word;
			LastLetter = LastCity.Last();

			if ("ъьйёы".Contains(LastLetter))
				LastLetter = LastCity[LastCity.Length - 2];

            usedCities.Add(LastCity);
            allCities.Remove(LastCity);

            textBox1.AppendText("Игрок говорит: " + LastCity + nl);

            CompMove();

        }

        //Click button "Send"
		private void button2_Click(object sender, EventArgs e)
		{
            PlayerMove(textBox2.Text);
        }

        //Click button "Help"
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> useable = new List<string>();

            textBox1.AppendText("Доступные города: ");

            foreach (string l in allCities)
                if (l.ToLower().First() == LastLetter)
                    textBox1.AppendText(l + " ");
            textBox1.AppendText(nl);
        }
    }
}
