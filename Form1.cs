using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace Citis
{
	public partial class Form1 : Form
	{
		const string fileName = "$data.txt";
		public string[] allCities = null;
		public string[] usedCities = null;
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
			allCities = File.ReadAllLines(fileName);
			LastCity = allCities[rnd.Next(allCities.Length)];
			LastLetter = LastCity.Last();
			if ("ъьйёы".Contains(LastLetter))
				LastLetter = LastCity[LastCity.Length - 2];
			label1.Text = LastCity;
		}

		//ДЗ
		//Функция со списком доступных городов, вывод хода игры, ходы компьютера
		//Встроить в чат, если будет время

		private bool PlayerRequest(string word)
		{
			if (LastCity.Last() != char.ToLower(word.First()))
			{
				MessageBox.Show("New word have to start from letter " + LastCity.Last());
				return false;
			}

			if (!allCities.Any(c => word == c))
			{
				MessageBox.Show("New word have to be city!");
				return false;
			}

			LastCity = word;
			LastLetter = LastCity.Last();
			if ("ъьйёы".Contains(LastLetter))
				LastLetter = LastCity[LastCity.Length - 2];
			label1.Text = LastCity;
			return true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBox1.Lines = allCities;
		}
        /// <summary>
        /// Player wrote the word.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void button2_Click(object sender, EventArgs e)
		{
			if (PlayerRequest(textBox2.Text))
				label1.Text = "Good! Last is " + LastCity;
			else
				label1.Text = "No!";
		}
	}
}
