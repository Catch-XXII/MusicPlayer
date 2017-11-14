using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        private static string path = @"C:\MyPlayList";
        static int currentsong = 0;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            play.Enabled = false;
            pictureBox1.Image = Image.FromFile("Default.jpg");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentsong = listBox1.SelectedIndex;
            string pictureName = listBox1.SelectedItem.ToString().Split('-')[0].Trim() + ".jpg";
            string path1 = path + "\\" + listBox1.SelectedItem;
            wplayer.URL = path1;
            wplayer.controls.play();
            label12.Text = "Playing " + listBox1.SelectedItem;
            if (File.Exists(pictureName))
                pictureBox1.Image = Image.FromFile(pictureName);
            else
            {
                pictureBox1.Image = Image.FromFile("Default.jpg");
            }
        }

        #region EnvironmentButtons

        private void search_Click_1(object sender, EventArgs e)
        {
            String[] allmp3 = System.IO.Directory.GetFiles(path, "*.mp3", System.IO.SearchOption.AllDirectories);
            label12.Text = Convert.ToString(allmp3.Length) + " songs are in your directory";
        }

        private void create_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    MessageBox.Show(path + " That path already exists.");
                    return;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
                MessageBox.Show(path + " The directory was created successfully.");
            }
            catch (Exception)
            {
                MessageBox.Show("The process failed");
                throw;
            }
        }

        private void show_Click_1(object sender, EventArgs e)
        {
            play.Enabled = true;
            if (!Directory.Exists(path))
            {
                MessageBox.Show("There is no directory.\nPlease create one");
                return;
            }

            listBox1.Items.Clear();
            string[] lines = Directory.GetFiles(path);
            foreach (string line in lines)
            {
                string[] divided = line.Split('\u005c');
                string l1 = divided[0];
                string l2 = divided[1];
                string l3 = divided[2];
                listBox1.Items.Add(l3);
            }
        }

        private void delete_Click_1(object sender, EventArgs e)
        {
            if (!Directory.Exists(path))
            {
                MessageBox.Show("No directory found");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you Sure? \nIf you click 'Yes', All files will be deleted", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes && Directory.Exists(path))
                {
                    MessageBox.Show("Your library was deleted");
                    Directory.Delete(path, true);
                }
                else
                {
                    return;
                }
            }
        }

        private void close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region ControlButtons

        private void play_Click_1(object sender, EventArgs e)
        {
            label12.Text = "Playing " + listBox1.SelectedItem;
            wplayer.controls.stop();
            wplayer.URL = path + "\\" + listBox1.SelectedItem.ToString();
            wplayer.controls.play();
        }

        private void pause_Click_1(object sender, EventArgs e)
        {
            label12.Text = "Paused...";
            wplayer.controls.pause();
        }

        private void stop_Click_1(object sender, EventArgs e)
        {
            label12.Text = "Stopped...";
            wplayer.controls.stop();
        }

        private void next_Click_1(object sender, EventArgs e)
        {
            if (currentsong + 1 < listBox1.Items.Count)
            {
                currentsong = currentsong + 1;
                listBox1.SelectedIndex = currentsong;
                label12.Text = "Playing " + listBox1.Items[currentsong].ToString();
                wplayer.controls.stop();
                wplayer.URL = path + "\\" + listBox1.Items[currentsong].ToString();
                wplayer.controls.play();

            }
        }

        private void previous_Click_1(object sender, EventArgs e)
        {

            if (currentsong - 1 < listBox1.Items.Count)
            {
                currentsong = currentsong - 1;
                listBox1.SelectedIndex = currentsong;
                label12.Text = "Playing " + listBox1.Items[currentsong].ToString();
                wplayer.controls.stop();
                wplayer.URL = path + "\\" + listBox1.Items[currentsong].ToString();
                wplayer.controls.play();
            }
        }

        #endregion
    }
}
