﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace Bricks
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Termek> termekek = new();
        string fileName;
        string folderName;
        public MainWindow()
        {
            InitializeComponent();
            dgTermekLista.ItemsSource = termekek;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var dialog = new OpenFolderDialog()
            {
                Title = "Select Folder",
                Multiselect = false
            };

            folderName = "";
            if (dialog.ShowDialog() == true)
            {
                folderName = dialog.FolderName;
            }
            MessageBox.Show(folderName);
            DirectoryInfo d = new DirectoryInfo(folderName);
            FileInfo[] Files = d.GetFiles("*.bsx"); //Getting Text files

            foreach (FileInfo file in Files)
            {
                lbFajlok.Items.Add(file.Name);
            }
            //Beolvas();


        }

        private void Beolvas()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "bsx files (*.bsx)|*.bsx|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                fileName = openFileDialog1.FileName;
                Betolt(fileName, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal error");
            }
        }

        private void Betolt(string fileName, bool mode = false)
        {
            if (File.ReadAllLines(fileName).ToList() == null)
            {
                return;
            }
            List<string> sorok = File.ReadAllLines(fileName).ToList();

                for (int i = 0; i < sorok.Count; i++)
                {
                    if (sorok[i].Trim() == "<Item>")
                    {
                        List<string> termekAdatok = new();
                        for (int j = 1; j < 13; j++)
                        {
                            int tol = sorok[i + j].IndexOf(">");
                            int ig = sorok[i + j].LastIndexOf("<");
                            int length = sorok[i + j].Length;

                            termekAdatok.Add(sorok[i + j].Substring(tol + 1, ig - tol - 1));

                        }
                        Termek ujTermerk = new Termek(termekAdatok.ToArray());

                        if (txtSzuroNev.Text != "" || txtSzuroId.Text != "" || cbKategoria.SelectedIndex != -1)
                        {
                            if (ujTermerk.Name.StartsWith(txtSzuroNev.Text) && ujTermerk.Id.StartsWith(txtSzuroId.Text))
                            {
                                if (cbKategoria.SelectedIndex != -1)
                                {
                                    if (!mode && (ujTermerk.CategoryName == cbKategoria.SelectedItem.ToString() || cbKategoria.SelectedIndex == -1))
                                    {
                                        termekek.Add(ujTermerk);
                                    }
                                    if (mode)
                                    {
                                        termekek.Add(ujTermerk);
                                    }

                                }
                            else
                            {
                                termekek.Add(ujTermerk);
                            }
                            }
                        }
                        else
                        {
                            termekek.Add(ujTermerk);
                        }
                    }

                }
                lblszam.Content = "Elemek száma: " + termekek.Count;


                var kategoriak = termekek.DistinctBy(n => n.CategoryName);
                cbKategoria.ItemsSource = kategoriak.Select(n => n.CategoryName);
        }

        private void btnSzuro_Click(object sender, RoutedEventArgs e)
        {
            termekek = new();
            dgTermekLista.ItemsSource = termekek;
            Betolt(fileName);
        }
        private void Reset()
        {
            if (termekek.Count != 0)
            {
                var kategoriak = termekek.DistinctBy(n => n.CategoryName);
                cbKategoria.ItemsSource = kategoriak.Select(n => n.CategoryName);
                cbKategoria.SelectedIndex = -1;
                txtSzuroId.Text = "";
                txtSzuroNev.Text = "";
                termekek.Clear();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reset();
            if (lbFajlok.SelectedIndex!=-1)
            {
                Betolt(folderName + "/" + lbFajlok.SelectedItem.ToString());
            }
            
        }

        private void lbFajlok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reset();
            Betolt(folderName+"/"+lbFajlok.SelectedItem.ToString());
        }
    }
}